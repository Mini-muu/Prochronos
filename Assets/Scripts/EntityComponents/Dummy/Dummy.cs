

public class Dummy : Entity
{
    #region States

    public DummyStateMachine StateMachine { get; private set; }
    public DummyIdleState IdleState { get; private set; }
    public DummyLightHitState LightHitState { get; private set; }
    public DummyHeavyHitState HeavyHitState { get; private set; }
    public DummyBrokenState BrokenState { get; private set; }
    
    #endregion

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new DummyStateMachine();

        IdleState = new DummyIdleState(this, StateMachine, "Idle");
        LightHitState = new DummyLightHitState(this, StateMachine, "LightHit");
        HeavyHitState = new DummyHeavyHitState(this, StateMachine, "HeavyHit");
        BrokenState = new DummyBrokenState(this, StateMachine, "Broken");

    }


    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.Update();
    }

    public void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public override void Die()
    {
        base.Die();
        StateMachine.ChangeState(BrokenState);
    }

    public override void OnHeavyHit()
    {
        base.OnHeavyHit();
        StateMachine.ChangeState(HeavyHitState);
    }

    public override void OnLightHit()
    {
        base.OnLightHit();
        StateMachine.ChangeState(LightHitState);
    }
}
