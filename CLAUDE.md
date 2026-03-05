# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project

Unity 2D space shooter game. Open and run via the Unity Editor — there are no CLI build or test commands.

## Development Philosophy (from `Assets/Game/Scripts/AI/UNITY_COMPOSITION_GUIDE.md`)

The project follows **"Unity Way"** — composition over inheritance.

### Core rules
- **One component = one responsibility.** If a component does two things, split it.
- **Composition via `[SerializeField]`**, not `GetComponent` at runtime.
- **Event-driven communication** between components (`event Action`). Never call a component's methods directly from another unrelated component.
- **Subscribe in `OnEnable`, unsubscribe in `OnDisable`** — always, no exceptions.
- **No `GetComponent` in `Update`/`FixedUpdate`** — cache references in `Awake` or wire via Inspector.
- Prefer interfaces (`IDamageTaker`, `IHealable`) for cross-object interaction.

### Naming conventions
- Components end with `Component` (`MoveComponent`, `HealthComponent`)
- Controllers end with `Controller` (`PlayerMoveController`, `FireController`)
- Observers end with `Observer` (`BulletDestroyObserver`, `EnemyDeathObserver`)
- Interfaces start with `I`
- Events start with `On` (`OnFire`, `OnDespawn`, `OnDead`)
- Private fields start with `_`

### Code order inside a class
Events → SerializeField settings → SerializeField dependencies → private fields → public properties → Unity callbacks (Awake → OnEnable → Start → Update → FixedUpdate → OnDisable → OnDestroy) → public methods → private methods

### Condition system (`AndCondition`)
Components that can be blocked (move only if alive, fire only if cooldown expired) should expose `AddCondition(Func<bool>)` and check it before acting. Facades (Player, Enemy) wire conditions in `Awake`:
```
_moveComponent.AddCondition(() => _healthComponent.IsAlive);
_fireComponent.AddCondition(() => _fireRate.IsExpired);
```

### Inheritance — only when justified (≈5% of cases)
- Template Method: shared algorithm, 1–2 variant steps
- Specialisation that adds data to a base component
- Default: use composition

---

## Code Architecture

### Folder structure

- `Assets/Game/Scripts/` — **active codebase**. All new work goes here.
- `Assets/Game/_Legacy/` — old commented-out code, kept for reference. Do not add new code here.

```
Scripts/
├── Components/          # Reusable single-responsibility components
│   ├── MoveComponent        — Rigidbody2D movement + AddCondition + Lerp smoothing
│   ├── FireComponent        — Spawns bullet via BulletFactory + AddCondition; raises OnFire
│   ├── HealthComponent      — HP tracking; IsAlive, ResetToMax(); raises OnHealthChanged/OnDead
│   ├── CooldownComponent    — Countdown timer; OnExpired fires once per Reset()
│   ├── TiltComponent        — DOTween rotation on move
│   ├── WaveMotionComponent  — DOTween idle float animation
│   └── CollisionComponent   — Wraps OnTriggerEnter2D; raises OnHit
├── Player/
│   ├── Player               — Facade: wires conditions, delegates to components
│   ├── PlayerMoveController — Reads Input axes → Player.SetDirection()
│   ├── FireController       — Reads Space key → Player.Fire()
│   ├── FireVFXController    — Plays ParticleSystem on FireComponent.OnFire
│   ├── PlayerDeathObserver  — Deactivates gameObject on HealthComponent.OnDead
│   └── PlayerSettings       — [Serializable] data: Damage, Health
├── Enemy/
│   ├── Enemy                — Facade: wires conditions, SetDestination, SetTarget, ResetState, Despawn
│   ├── EnemyMoveController  — AI: moves toward destination, rotates to face destination/target, exposes IsReached/IsFacingTarget
│   ├── EnemyFireController  — AI: calls Enemy.Fire() when IsReached
│   ├── EnemyDeathObserver   — Calls Enemy.Despawn() on HealthComponent.OnDead
│   └── EnemySpawner         — Pools enemies via PrefabPool, spawns on CooldownComponent.OnExpired
├── Bullet/
│   ├── Bullet               — Entity: MoveComponent + damage + Despawn lifecycle
│   ├── BulletDestroyObserver— Despawns bullet on CollisionComponent.OnHit or CooldownComponent.OnExpired
│   └── BulletDamageObserver — On hit: TryGetComponent<HealthComponent> → TakeDamage
├── Modules/
│   ├── BulletFactory        — Singleton; rents from PrefabPool, manages bullet lifecycle
│   ├── PrefabPool           — Generic pool keyed by prefab.name
│   └── AndCondition         — List<Func<bool>>; IsTrue() returns false if any condition fails
└── AI/
    └── UNITY_COMPOSITION_GUIDE.md
```

### Player flow

```
PlayerMoveController (Input) → Player.SetDirection()
FireController       (Input) → Player.Fire()

Player.Awake():
  _moveComponent.AddCondition(IsAlive)
  _fireComponent.AddCondition(IsAlive)
  _fireComponent.AddCondition(IsExpired)

Player.Fire()        → FireComponent.Fire() [conditions checked inside]
FireComponent.OnFire → Player resets CooldownComponent

Player.FixedUpdate() → MoveComponent.Move() [conditions checked inside]
                     → TiltComponent.TiltToDirection()
```

### Enemy flow

```
EnemySpawner (CooldownComponent.OnExpired) → pool.Rent<Enemy>()
  → enemy.SetDestination(), enemy.SetTarget(), enemy.ResetState(), subscribe OnDespawn

EnemyMoveController (Update):
  Moving   → direction to destination, rotate to face destination
  Reached  → direction zero, rotate to face player (target)
  Exposes IsReached, IsFacingTarget

EnemyFireController (Update) → when IsReached → Enemy.Fire()

Enemy.Awake():
  _moveComponent.AddCondition(IsAlive)
  _fireComponent.AddCondition(IsAlive)
  _fireComponent.AddCondition(IsExpired)
  _fireComponent.AddCondition(IsFacingTarget)

Enemy dies → HealthComponent.OnDead → EnemyDeathObserver → Enemy.Despawn()
          → EnemySpawner.Despawn() → pool.Return()
```

### Bullet lifecycle

```
FireComponent.Fire() → BulletFactory.SpawnBullet(prefab, pos, rot, dir)
  → PrefabPool.Rent<Bullet>() → set position/direction/lifetime → subscribe OnDespawn

Bullet hit → CollisionComponent.OnHit:
  → BulletDamageObserver  → target.HealthComponent.TakeDamage()
  → BulletDestroyObserver → Bullet.Despawn()

Bullet lifetime expired → CooldownComponent.OnExpired:
  → BulletDestroyObserver → Bullet.Despawn()

Bullet.OnDespawn → BulletFactory.Despawn() → PrefabPool.Return()
```

### Object pool

`PrefabPool` — plain C# class, dictionary keyed by `prefab.name`. `Rent<T>` dequeues or instantiates; `Return` deactivates and enqueues. Used by `BulletFactory` and `EnemySpawner`.

### Layer setup (Unity Physics)

Player/Enemy bullet separation is handled via layers and Layer Collision Matrix:
- PlayerBullet layer ↔ Enemy layer
- EnemyBullet layer ↔ Player layer
- Each FireComponent references the appropriate bullet prefab with the correct layer baked in

### Dependencies

- **DOTween** — used in `TiltComponent` and `WaveMotionComponent`
- All game code lives in the `Game` namespace
