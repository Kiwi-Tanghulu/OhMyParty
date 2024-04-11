using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using OMG.Interacting;
using Unity.Netcode;
using OMG.Players;

public class TSofa : NetworkBehaviour, IInteractable, IFocusable
{
    

    

    public GameObject CurrentObject => gameObject;

    private void Awake()
    {
        
    }

    public bool Interact(Component performer, bool actived, Vector3 point = default)
    {
        if(actived)
        {
            
        }

        return true;
    }

    public void OnFocusBegin(Vector3 point)
    {
        
    }

    public void OnFocusEnd()
    {
        
    }
}
