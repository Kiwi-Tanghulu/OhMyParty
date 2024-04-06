using OMG.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : FSMState
{
    [SerializeField] private PlayInputSO input;

    public override void EnterState()
    {
        input.OnMoveEvent += TransIdleState;
        input.OnMoveEvent += SetMoveDir;
    }

    public override void UpdateState()
    {
        movement.Move();
    }

    public override void ExitState()
    {
        input.OnMoveEvent -= TransIdleState;
        input.OnMoveEvent -= SetMoveDir;
    }

    private void SetMoveDir(Vector2 input)
    {
        Vector3 moveDir = new Vector3(input.x, 0f, input.y);

        movement.SetMoveDir(moveDir);
    }

    private void TransIdleState(Vector2 input)
    {
        if (input == Vector2.zero)
            actioningPlayer.ChangeState(typeof(IdleState));
    }
}
