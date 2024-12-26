﻿using UnityEngine;
namespace BeforeTimeOfTheTree
{
    public class PlayerMoveState : PlayerState
    {
        public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
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
            player.SetVelocity(xInput * player.walkSpeed , rb.linearVelocity.y);
            
            player.currentSpeed = Mathf.MoveTowards(player.currentSpeed,player.runSpeed,player.addSpeed * Time.deltaTime);
            player.anim.SetFloat("xVelocity", player.currentSpeed);
            if (xInput == 0)
                player.stateMachine.ChangeState(player.idleState);
        }
    }
}