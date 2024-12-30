using UnityEngine;
namespace BeforeTimeOfTheTree
{
    public class PlayerPrimaryAttackState : PlayerState
    {
        private int comboCounter;
        private float lastTimeAttacked;
        private float comboWindow = 2;

        public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
                comboCounter = 0;

            player.anim.SetInteger("ComboCounter",comboCounter);
            stateTimer = .1f;

            #region Choose attack Direction
            float attackDir = player.facingDir;
            if (xInput != 0) 
                attackDir = xInput;
            #endregion

            player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);
            //Debug.Log(comboCounter);
        }

        public override void Exit()
        {
            base.Exit();
            player.StartCoroutine("BusyFor", .15f);
            comboCounter++;
            lastTimeAttacked = Time.time;
            //Debug.Log(lastTimeAttacked);
        }

        public override void Update()
        {
            base.Update();
            if(stateTimer <  0)
                player.ZeroVelocity();

            if(triggerCalled) 
                player.stateMachine.ChangeState(player.idleState);
        }
    }
}
