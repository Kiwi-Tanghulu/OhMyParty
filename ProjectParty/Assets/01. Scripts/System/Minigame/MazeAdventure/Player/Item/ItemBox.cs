using DG.Tweening;
using Unity.Netcode;
using UnityEngine;


namespace OMG.Minigames.MazeAdventure
{
    public class ItemBox : NetworkBehaviour
    {
        [SerializeField] private float floatDistance = 0.5f;
        [SerializeField] private float floatDuration = 2f;
        private NetworkVariable<ItemType> itemType = new NetworkVariable<ItemType>();
        public ItemType ItemType => itemType.Value;
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsHost)
            {
                SetRadomItemType();
            }

            OnItemTypeSetting(itemType.Value);
        }

        private void SetRadomItemType()
        {
            ItemType randomType = (ItemType)Random.Range(1, System.Enum.GetValues(typeof(ItemType)).Length);
            itemType.Value = randomType;
        }

        private void OnItemTypeSetting(ItemType value)
        {
            FlotingMove();
        }
        private void FlotingMove()
        {
            Vector3 upPosition = transform.position + new Vector3(0, floatDistance, 0);

            transform.DOMove(upPosition, floatDuration)
                     .SetEase(Ease.InOutSine)
                     .SetLoops(-1, LoopType.Yoyo);
        }
    }
}
