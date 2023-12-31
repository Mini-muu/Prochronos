﻿using UnityEngine;

public class PrototypeBoss_MoveState : EnemyState
{
    private PrototypeBoss boss;
    protected Transform playerTransform;

    public PrototypeBoss_MoveState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, PrototypeBoss _boss) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        boss = _boss;
    }

    public override void Enter()
    {
        base.Enter();
        playerTransform = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        boss.SetVelocity(boss.moveSpeed * boss.FacingDir, rb.velocity.y);
    }
}