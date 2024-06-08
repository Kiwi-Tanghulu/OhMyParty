using OMG.Extensions;
using OMG.Inputs;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class PowderItem : SafetyZoneItem
    {
        [SerializeField] PlayInputSO input = null;
        [SerializeField] float inversionDuration = 3f;

        public override void OnCollision(Collision other)
        {
            if(false) // 부딪힌 것이 본인 클라라면
                return;
        }
    }
}
