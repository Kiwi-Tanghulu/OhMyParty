using OMG.NetworkEvents;
using Unity.Netcode;
using UnityEngine;

public class CatchTailPlayer : MonoBehaviour
{
    [SerializeField] NetworkEvent<UlongParams> onTargetSelectedEvent = new NetworkEvent<UlongParams>("TargetSelected");
    private ulong targetPlayerID = 0;

    public void Init(NetworkObject owner)
    {
        onTargetSelectedEvent?.AddListener(HandleTargetPlayerSelected);
        onTargetSelectedEvent?.Register(owner);
    }

    public void SetTargetPlayerID(ulong targetID)
    {
        targetPlayerID = targetID;
        onTargetSelectedEvent?.Broadcast(targetID);
    }

    public bool IsCorrectTarget(ulong inputID) => inputID == targetPlayerID;

    private void HandleTargetPlayerSelected(UlongParams targetID)
    {
        
    }
}