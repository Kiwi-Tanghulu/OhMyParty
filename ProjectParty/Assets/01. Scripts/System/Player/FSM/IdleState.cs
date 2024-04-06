using OMG.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : FSMState
{
    [SerializeField] private PlayInputSO input;

    public override void EnterState()
    {
        input.OnMoveEvent += TransMoveState;

        movement.SetMoveDir(Vector3.zero);
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {
        input.OnMoveEvent -= TransMoveState;
    }

    private void TransMoveState(Vector2 input)
    {
        if (input != Vector2.zero)
            actioningPlayer.ChangeState(typeof(MoveState));
    }
}
