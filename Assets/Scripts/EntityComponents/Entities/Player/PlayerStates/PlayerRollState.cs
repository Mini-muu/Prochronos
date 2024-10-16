﻿
public class PlayerRollState : PlayerState
{
    bool executing;

    private PlayerStats playerStats = PlayerManager.instance.playerStats;

    public PlayerRollState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
        action = PlayerAction.Roll;
    }

    public override void Enter()
    {
        base.Enter();

        //player.skill.Roll = null;

        stateTimer = player.rollDuration;

        player.Stats.MakeInvincible(true);
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0, rb.velocity.y);
        player.Stats.MakeInvincible(false);
    }

    public override void Update()
    {
        base.Update();

        if (player.Stats.currentStamina < player.rollConsumption && !executing)
        {
            stateMachine.ChangeState(player.IdleState);
            return;
        }

        if (!executing)
        {
            playerStats.DecreaseStaminaBy(player.rollConsumption);
            executing = true;
        }

        player.SetVelocity(player.rollSpeed * player.RollDir, 0);

        if (stateTimer < 0 && triggerCalled)
        {
            stateMachine.ChangeState(player.IdleState);
            executing = false;
        }

    }
}