using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class FishItem : SafetyZoneItem
    {
        public override void OnCollision(Transform other)
        {
            // other에서 무언가를 가져와서 효과 적용하기
            Debug.Log("철썩");
            Destroy(gameObject);
        }
    }
}
