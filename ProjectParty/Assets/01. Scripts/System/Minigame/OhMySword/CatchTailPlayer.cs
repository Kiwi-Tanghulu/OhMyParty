using OMG.NetworkEvents;
using UnityEngine;

public class CatchTailPlayer : MonoBehaviour
{
    [SerializeField] NetworkEvent<UlongParams> onTargetSelectedEvent = new NetworkEvent<UlongParams>("TargetSelected");
    private int targetPlayerID = 0;

    public void Init(NetworkBehaviour owner)
    {
        onTargetSelectedEvent?.AddListener(HandleTargetPlayerSelected);
        onTargetSelectedEvent?.Register(owner);
    }

    public void SetTargetPlayerID(int targetID)
    {
        targetPlayerID = targetID;
        onTargetSelectedEvent?.Broadcast(targetID);
    }

    public bool IsCorrecTarget(int inputID) => targetPlayerID;

    private void HandleTargetPlayerSelected(UlongParams targetID)
    {
        
    }
}