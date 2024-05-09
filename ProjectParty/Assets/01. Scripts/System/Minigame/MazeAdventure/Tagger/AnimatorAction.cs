using OMG.FSM;
using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorAction : FSMAction
{
    [SerializeField] private string animBoolName;
    private int animBoolHash;
    private Animator animator;

    public override void Init(FSMBrain brain)
    {
        base.Init(brain);
        animator = brain.GetComponent<Animator>();
        if(animator == null )
            animator = brain.transform.Find("Visual").GetComponent<Animator>();
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
