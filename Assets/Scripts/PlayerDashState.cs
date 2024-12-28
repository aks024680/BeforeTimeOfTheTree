using BeforeTimeOfTheTree;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(BeforeTimeOfTheTree.Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.dashSpeed * player.dashDir,0);
        if(!player.IsGroundDetected && player.IsWallDetected)
            player.stateMachine.ChangeState(player.wallSlideState);
        //Debug.Log("I am Dashing....");
        if (stateTimer < 0) 
            player.stateMachine.ChangeState(player.idleState);
    }
}
