using UnityEngine;

public class DummyState
{
    protected DummyStateMachine stateMachine;
    protected Dummy dummy;

    private string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled;

    public DummyState(Dummy _dummy, DummyStateMachine _stateMachine, string _animBoolName)
    {
        this.dummy = _dummy;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        dummy.Anim.SetBool(animBoolName, true);
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        dummy.Anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}

