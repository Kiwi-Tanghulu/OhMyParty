using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class PowderItem : SafetyZoneItem
    {
        [SerializeField] float inversionDuration = 3f;

        public override void OnCollisionPlayer(SafetyZonePlayerController player, Collision other)
        {
            player.InvertInput(inversionDuration);
        }
    }
}
