using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class FishItem : SafetyZoneItem
    {
        [SerializeField] float power = 10f;
        [SerializeField] float slowPower = -10f;
        [SerializeField] float slowDuration = 10f;

        public override void OnCollisionPlayer(SafetyZonePlayerController player, Collision other)
        {
            player.Health.OnDamaged(power, transform, other.contacts[0].point, HitEffectType.None, other.contacts[0].normal);
            player.Slow(slowDuration, slowPower);
            // 속도 감소
        }
    }
}
