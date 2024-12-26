﻿using UnityEngine;

namespace BeforeTimeOfTheTree
{
    public class Player : MonoBehaviour
    {
        [Header("Move Info")]
        public float walkSpeed = 7f;
        public float runSpeed = 8f;
        public float addSpeed = 6f;
        public float currentSpeed;
        #region Component
        public Animator anim { get; private set; }
        public Rigidbody2D rb { get; private set; }
        #endregion

        #region State
        public PlayerStateMachine stateMachine { get; private set; }
        public PlayerIdleState idleState { get; private set; }
        public PlayerMoveState moveState { get; private set; }
        #endregion

        private void Awake()
        {
            stateMachine = new PlayerStateMachine();
            idleState = new PlayerIdleState(this, stateMachine, "Idle");
            moveState = new PlayerMoveState(this, stateMachine, "Move");
            
        }
        private void Start()
        {
            anim = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
            stateMachine.Initialize(idleState);
        }
        private void Update()
        {
            stateMachine.currentState.Update();
        }
        public void SetVelocity(float _xVelocity,float _yVelocity)
        {
            rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
            
        }
    }
}