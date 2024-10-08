using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        PlayerInputManager.instance.parry.performed += ParryPerformed;
        PlayerInputManager.instance.normalAttack.performed += NormalAttackPerformed;
        PlayerInputManager.instance.strongAttack.performed += StrongAttackPerformed;
        PlayerInputManager.instance.jump.performed += JumpPerformed;
    }

    public override void Exit()
    {
        base.Exit();
        PlayerInputManager.instance.parry.performed -= ParryPerformed;
        PlayerInputManager.instance.normalAttack.performed -= NormalAttackPerformed;
        PlayerInputManager.instance.strongAttack.performed -= StrongAttackPerformed;
        PlayerInputManager.instance.jump.performed -= JumpPerformed;
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.AirState);
        }
        else
        {
            player.canDoubleJump = PlayerManager.instance.unlockedActions.Contains(PlayerAction.DoubleJump);
        }
    }

    private void ParryPerformed(InputAction.CallbackContext ctx)
    {
        stateMachine.ChangeState(player.ParryState);
    }

    private void NormalAttackPerformed(InputAction.CallbackContext ctx)
    {
        stateMachine.ChangeState(player.NormalAttrackState);
    }

    private void StrongAttackPerformed(InputAction.CallbackContext ctx)
    {
        stateMachine.ChangeState(player.ChargedAttackState);
    }

    private void JumpPerformed(InputAction.CallbackContext ctx)
    {
        if (!player.IsGroundDetected()) return;

        stateMachine.ChangeState(player.JumpState);
    }
}
