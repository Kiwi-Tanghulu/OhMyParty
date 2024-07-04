using OMG.FSM;
using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorAction : FSMAction
{
    [SerializeField] private string animBoolName;
    private int animBoolHash;
    [SerializeField] private Animator animator;

    public override void Init(CharacterFSM brain)
    {
        base.Init(brain);
        animBoolHash = Animator.StringToHash(animBoolName);
    }
    public override void EnterState()
    {
        base.EnterState();
        animator.SetBool(animBoolHash, true);
    }
    public override void ExitState()
    {
        base.ExitState();
        animator.SetBool(animBoolHash, false);
    }
}
