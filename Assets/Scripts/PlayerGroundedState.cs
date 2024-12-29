using BeforeTimeOfTheTree;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(BeforeTimeOfTheTree.Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Mouse0)) 
            player.stateMachine.ChangeState(player.primaryAttack);
        if(!player.IsGroundDetected)
            player.stateMachine.ChangeState(player.airState);
        if(Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected)
            player.stateMachine.ChangeState(player.jumpState);
    }
}
