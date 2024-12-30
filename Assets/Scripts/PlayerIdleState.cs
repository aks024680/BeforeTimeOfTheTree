using UnityEngine;
namespace BeforeTimeOfTheTree { 
public class PlayerIdleState : PlayerGroundedState
    {
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
            player.ZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
            if(xInput == 0 && player.IsWallDetected)
                return;
            if(xInput != 0 && !player.isBusy)
            player.stateMachine.ChangeState(player.moveState);
    }
}
}
