using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using OMG.Interacting;
using Unity.Netcode;
using OMG.Players;

public class TSofa : MonoBehaviour, IInteractable
{
    public bool Interact(Component performer, bool actived, Vector3 point = default)
    {
        ActioningPlayer actioningPlayer = performer.GetComponent<ActioningPlayer>();

        return true;
    }
}
