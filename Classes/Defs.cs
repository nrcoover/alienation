public static class Defs
{
    public enum PowerUpType { Health, Shield }
    public enum BulletType { Player, Enemy, EnemyBomb }
    public enum ExplosionType { Explosion, Boom }

    public const string GROUP_HOMING_MISSLE = "homingMissile";
    public const string GROUP_PLAYER = "player";
    public const string GROUP_ENEMY_SHIP = "enemyShip";
    public const string GROUP_SAUCER = "saucer";
    public const string GROUP_BULLET = "bullet";

    public const int MISSILE_DAMAGE = 10;
    public const int COLLISION_DAMAGE = 40;
}