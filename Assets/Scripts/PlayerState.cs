using UnityEngine;
namespace BeforeTimeOfTheTree
{
    public class PlayerState
    {
        protected float xInput;
        protected float yInput;
        protected PlayerStateMachine stateMachine;
        protected Player player;
        private string animBoolName;
        protected float stateTimer;

        protected bool triggerCalled;

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
            triggerCalled = false;
            //Debug.Log("I enter " + animBoolName);
        }
        public virtual void Update()
        {
            stateTimer -= Time.deltaTime;
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");
            player.anim.SetFloat("yVelocity", rb.linearVelocity.y);
            //Debug.Log("I am in " + animBoolName);

        }
        public virtual void Exit()
        {
            player.anim.SetBool(animBoolName, false);
            //Debug.Log("I exit " + animBoolName);

        }
        public virtual void AnimationFinishTrigger()
        {
            triggerCalled = true;
        }
    }
}