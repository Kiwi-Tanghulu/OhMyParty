using OMG.NetworkEvents;
using Unity.Netcode;
using UnityEngine;

public class CatchTailPlayer : MonoBehaviour
{
    [SerializeField] NetworkEvent<UlongParams, ulong> onTargetSelectedEvent = new NetworkEvent<UlongParams, ulong>("TargetSelected");
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

    private void HandleTargetPlayerSelected(ulong targetID)
    {
        Debug.Log($"Client {GetComponent<NetworkObject>().OwnerClientId}'s Target is {targetID}");
    }
}