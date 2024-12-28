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
            rb.linearVelocity = new Vector2(0,0);
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
            if(xInput != 0)
            player.stateMachine.ChangeState(player.moveState);
    }
}
}
