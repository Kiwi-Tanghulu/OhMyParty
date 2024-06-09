using OMG.Utility.Netcodes;
using OMG.Utility.Transforms;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

namespace OMG.Minigames.RockFestival
{
    public class RockTransform : TransformController
    {
        [SerializeField] LayerMask groundLayer = 0;

        public void FitToGround()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, float.MaxValue, groundLayer))
                SetPositionImmediately(hit.point + (transform.forward * 0.5f) + (Vector3.up * 0.5f));
        }
    }
}
