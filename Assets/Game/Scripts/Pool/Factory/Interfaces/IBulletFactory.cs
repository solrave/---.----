namespace Game
{
    public interface IBulletFactory
    {
        Bullet Get(TeamType type);
    }
}