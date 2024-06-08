using Cinemachine;
using OMG;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Chair : NetworkBehaviour
{
    [SerializeField] private List<Transform> sitPoints;

    private NetworkList<bool> isUsed;

    private void Awake()
    {
        isUsed = new NetworkList<bool>(new bool[sitPoints.Count]);
    }

    public Transform GetUseableSitPoint()
    {
        for(int i = 0; i < isUsed.Count; i++)
        {
            if (!isUsed[i])
            {
                return sitPoints[i];
            }
        }

        return null;
    }

    public void SetUseWhetherChair(Transform sitPoint, bool isUse)
    {
        int index = sitPoints.IndexOf(sitPoint);

        SetUseWhetherChairServerRpc(index, isUse);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetUseWhetherChairServerRpc(int sitPointIndex, bool value)
    {
        isUsed[sitPointIndex] = value;
    }
}
