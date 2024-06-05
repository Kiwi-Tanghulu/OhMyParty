using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class FishItem : SafetyZoneItem
    {
        public override void OnCollision()
        {
            Debug.Log("철썩");
        }
    }
}
