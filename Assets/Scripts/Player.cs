using System.Collections;
using UnityEngine;

namespace BeforeTimeOfTheTree
{
    public class Player : MonoBehaviour
    {
        
        [Header("Attack details")]
        public Vector2[] attackMovement;

        [Header("Move Info")]
        public float walkSpeed = 5f;
        public float runSpeed = 8f;
        public float addSpeed = 9f;
        public float currentSpeed;

        public float jumpForce;

        [Header("Dash Info")]
        [SerializeField]private float dashCoolDown;
        private float dashUsageTimer;
        public float dashSpeed;
        public float dashDuration;
        public float dashDir { get; private set; }

        [Header("Collision Info")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private Transform wallCheck;
        [SerializeField] private float wallCheckDistance;
        [SerializeField] private LayerMask whatIsGround;

        public int facingDir { get; private set; } = 1;
        private bool facingRight = true;

        public bool isBusy { get; private set; }

        #region Component
        public Animator anim { get; private set; }
        public Rigidbody2D rb { get; private set; }
        #endregion

        #region State
        public PlayerStateMachine stateMachine { get; private set; }
        public PlayerIdleState idleState { get; private set; }
        public PlayerMoveState moveState { get; private set; }
        public PlayerJumpState jumpState { get; private set; }
        public PlayerAirState airState { get; private set; }
        public PlayerDashState dashState { get; private set; }
        public PlayerWallSlideState wallSlideState { get; private set; }
        public PlayerWallJumpState wallJumpState { get; private set; }
        public PlayerPrimaryAttackState primaryAttack { get; private set; }
        #endregion

        private void Awake()
        {
            stateMachine = new PlayerStateMachine();
            idleState = new PlayerIdleState(this, stateMachine, "Idle");
            moveState = new PlayerMoveState(this, stateMachine, "Move");
            jumpState = new PlayerJumpState(this, stateMachine, "Jump");
            airState = new PlayerAirState(this, stateMachine, "Jump");
            dashState = new PlayerDashState(this, stateMachine, "Dash");
            wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
            wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
            primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
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
            CheckForDashIInput();
            
        }
        
        public IEnumerator BusyFor(float _seconds)
        {
            isBusy = true;
            Debug.Log("is busy");
            yield return new WaitForSeconds(_seconds);
            Debug.Log("not busy");
            isBusy = false;
        }

        public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

        private void CheckForDashIInput()
        {
            if (IsWallDetected)
                return;
            dashUsageTimer -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
            {
                dashUsageTimer = dashCoolDown;
                dashDir = Input.GetAxisRaw("Horizontal");

                if(dashDir == 0)
                    dashDir = facingDir;

                stateMachine.ChangeState(dashState);
            }
        }
        #region Velocity
        public void ZeroVelocity() => rb.linearVelocity = new Vector2(0, 0);

        public void SetVelocity(float _xVelocity, float _yVelocity)
        {
            rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
            FlipController(_xVelocity);
        }
        #endregion
        #region Collision
        public bool IsGroundDetected => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        public bool IsWallDetected => Physics2D.Raycast(wallCheck.position,Vector2.right,wallCheckDistance,whatIsGround);

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
            Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        }
        #endregion
        #region Flip
        public void Flip()
        {
            facingDir = facingDir * -1;
            facingRight = !facingRight;
            transform.Rotate(0, 180, 0);
        }
        public void FlipController(float _x)
        {
            if (_x > 0 && !facingRight)
                Flip();
            else if (_x < 0 && facingRight)
                Flip();
        }
        #endregion
    }
}