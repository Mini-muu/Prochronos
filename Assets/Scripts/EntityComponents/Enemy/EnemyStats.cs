public class EnemyStats : CharacterStats
{
    private Enemy enemy;

    protected override void Start()
    {
        base.Start();

        enemy = GetComponent<Enemy>();
    }

    public override bool TryTakeDamage(int _damage)
    {
        base.TryTakeDamage(_damage);
        enemy.DamageEffect();
        return true;
    }

    public override void DoDamage(CharacterStats _targetStats)
    {
        base.DoDamage(_targetStats);
    }

    protected override void Die()
    {
        base.Die();
        enemy.Die();
    }
}
