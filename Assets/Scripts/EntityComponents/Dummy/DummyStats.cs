public class DummyStats : CharacterStats
{
    private Dummy dummy;

    protected override void Start()
    {
        base.Start();

        dummy = GetComponent<Dummy>();
    }

    public override bool TryTakeDamage(int _damage)
    {
        return base.TryTakeDamage(_damage);
    }

    public override void DoDamage(CharacterStats _targetStats)
    {
        base.DoDamage(_targetStats);
    }

    protected override void Die()
    {
        base.Die();
        dummy.Die();
    }
}
