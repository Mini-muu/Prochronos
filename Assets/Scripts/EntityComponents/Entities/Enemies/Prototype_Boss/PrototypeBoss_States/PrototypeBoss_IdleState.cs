using UnityEngine.InputSystem;

public class PrototypeBoss_IdleState : EnemyState
{

    private PrototypeBoss boss;

    public PrototypeBoss_IdleState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, PrototypeBoss _boss) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        boss = _boss;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Keyboard.current.uKey.IsPressed())//Input.GetKeyDown(KeyCode.U)) 
        {
            stateMachine.ChangeState(boss.TeleportState);
        }
    }
}