class Weapon
{
    private int _damage;
    private int _bullets;

    public void Fire(Player player)
    {
        if (_damage < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_damage));
        }
        else if (_bullets > 0)
        {
            player.TakeDamage(_damage);
            _bullets--;
        }
    }
}

class Player
{
    private int _health;

    public void TakeDamage(int damage)
    {
        if (_health <= 0)
        {
            _health = 0;
            return;
        }

        _health -= damage;
    }
}

class Bot
{
    private Weapon _weapon;

    public void OnSeePlayer(Player player) => _weapon.Fire(player);
}