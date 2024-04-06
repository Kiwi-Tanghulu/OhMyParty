using OMG.Player;
using UnityEngine;

public abstract class FSMState : MonoBehaviour
{
    protected ActioningPlayer actioningPlayer;
    protected PlayerMovement movement;
    protected PlayerAnimation anim;

    public virtual void InitState(ActioningPlayer actioningPlayer)
    {
        this.actioningPlayer = actioningPlayer;
        movement = actioningPlayer.GetComponent<PlayerMovement>();
        anim = actioningPlayer.transform.Find("Visual").GetComponent<PlayerAnimation>();
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}
