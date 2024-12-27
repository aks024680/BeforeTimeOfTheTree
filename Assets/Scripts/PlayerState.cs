﻿using UnityEngine;
namespace BeforeTimeOfTheTree
{
    public class PlayerState
    {
        protected float xInput;
        protected PlayerStateMachine stateMachine;
        protected Player player;
        private string animBoolName;
        protected float stateTimer;

        public Rigidbody2D rb;
        public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        {
            this.player = _player;
            this.stateMachine = _stateMachine;
            this.animBoolName = _animBoolName;
        }
        public virtual void Enter()
        {
            player.anim.SetBool(animBoolName, true);
            rb = player.rb;
            //Debug.Log("I enter " + animBoolName);
        }
        public virtual void Update()
        {
            stateTimer -= Time.deltaTime;
            xInput = Input.GetAxisRaw("Horizontal");
            player.anim.SetFloat("yVelocity", rb.linearVelocity.y);
            //Debug.Log("I am in " + animBoolName);

        }
        public virtual void Exit()
        {
            player.anim.SetBool(animBoolName, false);
            //Debug.Log("I exit " + animBoolName);

        }
    }
}