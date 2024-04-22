public class PlayerRunState : PlayerState
{

    private PlayerStats playerStats = PlayerManager.instance.playerStats;

    public PlayerRunState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //player.skill.Roll = null;

        stateTimer = 0.01f;
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0, rb.velocity.y);
    }

   public override void Update()
   {
        base.Update();

        if (playerStats.currentStamina < player.runConsumptionPerSec / 60)
        {
            stateMachine.ChangeState(player.MoveState);
            return;
        }

        if (stateTimer <= 0)
        {
            playerStats.DecreaseStaminaBy(player.runConsumptionPerSec / 60);
            stateTimer = 0.01f;
        }

        player.SetVelocity(xInput * player.runSpeed, rb.velocity.y);
        player.allowStaminaRecovery = false;

        if (xInput == 0 || player.IsWallDetected())
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
