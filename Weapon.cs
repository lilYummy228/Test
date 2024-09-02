class Weapon
{
    private int _damage;
    private int _bullets;

    public Weapon(int damage, int bullets)
    {
        if (damage < 0 || bullets < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        else
        {
            _damage = damage;
            _bullets = bullets;
        }
    }

    public void Fire(Player player)
    {
        if (_bullets-- > 0)
            player.TakeDamage(_damage);
    }
}

class Player
{
    private int _health;

    public Player(int health)
    {
        if (health < 0)
            throw new ArgumentOutOfRangeException(nameof(health));
        else
            _health = health;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));
        else
            _health -= damage;

        if (_health < 0)
            _health = 0;
    }
}

class Bot
{
    private readonly Weapon _weapon;

    public Bot(Weapon weapon)
    {
        if (weapon == null)
            throw new ArgumentNullException(nameof(weapon));

        _weapon = weapon;
    }

    public void OnSeePlayer(Player player)
    {
        if (player == null || _weapon == null)
            throw new ArgumentNullException();
        else
            _weapon.Fire(player);
    }
}