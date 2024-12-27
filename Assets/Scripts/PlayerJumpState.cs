﻿using BeforeTimeOfTheTree;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(BeforeTimeOfTheTree.Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(rb.linearVelocity.y < 0)
            player.stateMachine.ChangeState(player.airState);
    }
}
