using OMG;
using OMG.Extensions;
using OMG.Minigames;
using OMG.NetworkEvents;
using OMG.UI.Minigames.OhMySword;
using Unity.Netcode;
using UnityEngine;

public class CatchTailPlayer : MonoBehaviour
{
    [SerializeField] NetworkEvent<UlongParams, ulong> onTargetSelectedEvent = new NetworkEvent<UlongParams, ulong>("TargetSelected");
    private NetworkObject owner = null;
    private Minigame minigame = null;

    private OhMySwordPlayerPanel playerPanel = null;
    private ulong targetPlayerID = 0;

    private void Awake()
    {
        minigame = MinigameManager.Instance.CurrentMinigame;
        playerPanel = minigame.MinigamePanel.PlayerPanel as OhMySwordPlayerPanel;
    }

    public void Init(NetworkObject owner)
    {
        onTargetSelectedEvent?.AddListener(HandleTargetPlayerSelected);
        onTargetSelectedEvent?.Register(owner);

        this.owner = owner;
    }

    public void SetTargetPlayerID(ulong targetID)
    {
        targetPlayerID = targetID;
        onTargetSelectedEvent?.Broadcast(targetID, false);
    }

    public bool IsCorrectTarget(ulong inputID) => inputID == targetPlayerID;

    private void HandleTargetPlayerSelected(ulong targetID)
    {
        targetPlayerID = targetID;
        
        int playerIndex = minigame.PlayerDatas.FindIndex(i => i.clientID == owner.OwnerClientId);
        int targetIndex = minigame.PlayerDatas.FindIndex(i => i.clientID == targetID);
        Color color = PlayerManager.Instance.GetPlayerColor(targetIndex);

        if(owner.IsOwner)
            playerPanel.SetTargetTailColor(color);
        // playerPanel.SetTargetTailColor(playerIndex, color);
    }
}