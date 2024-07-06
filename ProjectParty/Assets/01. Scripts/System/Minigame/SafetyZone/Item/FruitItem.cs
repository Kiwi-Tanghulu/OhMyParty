using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class FruitItem : SafetyZoneItem
    {
        [SerializeField] float power = 10f;
        public override void OnCollisionPlayer(SafetyZonePlayerController player, Collision other)
        {
            player.FruitItemHitEffectServerRPC();
            player.Health.OnDamaged(power, transform, other.contacts[0].point, HitEffectType.None,other.contacts[0].normal);
        }
    }
}