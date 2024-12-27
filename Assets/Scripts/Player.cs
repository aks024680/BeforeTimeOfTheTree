using UnityEngine;

namespace BeforeTimeOfTheTree
{
    public class Player : MonoBehaviour
    {
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
        #endregion

        private void Awake()
        {
            stateMachine = new PlayerStateMachine();
            idleState = new PlayerIdleState(this, stateMachine, "Idle");
            moveState = new PlayerMoveState(this, stateMachine, "Move");
            jumpState = new PlayerJumpState(this, stateMachine, "Jump");
            airState = new PlayerAirState(this, stateMachine, "Jump");
            dashState = new PlayerDashState(this, stateMachine, "Dash");
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

        private void CheckForDashIInput()
        {
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

        public void SetVelocity(float _xVelocity, float _yVelocity)
        {
            rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
            FlipController(_xVelocity);
        }
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
        public bool IsGroundDetected => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
            Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        }
    }
}