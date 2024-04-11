using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using OMG.Interacting;
using Unity.Netcode;
using OMG.Players;

public class TSofa : NetworkBehaviour, IInteractable, IFocusable
{
    public List<Transform> sitPointList;
    private NetworkList<ulong> playerIdList;

    public GameObject CurrentObject => gameObject;

    private void Awake()
    {
        playerIdList = new NetworkList<ulong>(new ulong[4]);
    }

    public bool Interact(Component performer, bool actived, Vector3 point = default)
    {
        if(actived)
        {
            if (performer.TryGetComponent<PlayerController>(out PlayerController player))
            {
                InteractServerRpc(player.OwnerClientId);
            }
        }

        return true;
    }

    [ServerRpc(RequireOwnership = false)]
    private void InteractServerRpc(ulong playerId)
    {
        playerIdList[(int)playerId] = playerId;
        Transform playerTrm = NetworkManager.Singleton.ConnectedClients[playerId].PlayerObject.transform;
        Transform sitPoint = sitPointList[(int)playerId];
        playerTrm.position = sitPoint.position;
        playerTrm.rotation = sitPoint.rotation;
    }

    public void OnFocusBegin(Vector3 point)
    {
        
    }

    public void OnFocusEnd()
    {
        
    }
}
