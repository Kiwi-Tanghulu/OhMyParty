using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class FishItem : SafetyZoneItem
    {
        [SerializeField] float power = 10f;

        public override void OnCollisionPlayer(SafetyZonePlayerController player, Collision other)
        {
            player.Health.OnDamaged(power, transform, other.contacts[0].point, other.contacts[0].normal);
            // 속도 감소
        }
    }
}
