using UnityEngine;
namespace BeforeTimeOfTheTree
{
    public class PlayerPrimaryAttack : PlayerState
    {
        private int comboCounter;
        private float lastTimeAttacked;
        private float comboWindow = 2;

        public PlayerPrimaryAttack(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
                comboCounter = 0;

            player.anim.SetInteger("ComboCounter",comboCounter);
            //Debug.Log(comboCounter);
        }

        public override void Exit()
        {
            base.Exit();
            comboCounter++;
            lastTimeAttacked = Time.time;
            //Debug.Log(lastTimeAttacked);
        }

        public override void Update()
        {
            base.Update();
            if(triggerCalled) 
                player.stateMachine.ChangeState(player.idleState);
        }
    }
}
