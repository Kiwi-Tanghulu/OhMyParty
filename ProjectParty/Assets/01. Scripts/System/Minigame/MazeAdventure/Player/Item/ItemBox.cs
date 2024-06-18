using DG.Tweening;
using System.Collections;
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
        public bool Alive = true;

        [SerializeField] private float dissolveTime;
        private Material material;

        private void Awake()
        {
            material = GetComponent<Renderer>().material;
        }
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsHost)
            {
                SetRadomItemType();
            }

            DissolveVisual();
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

        private void DissolveVisual()
        {
            StartCoroutine(Dissolve());
        }

        private IEnumerator Dissolve()
        {
            float time = 0;
            material.SetFloat("_Split_Value", 0f);

            while(time <= dissolveTime)
            {
                time += Time.deltaTime;
                material.SetFloat("_Split_Value", 1.5f * (time/dissolveTime));
                yield return null;
            }

            material.SetFloat("_Split_Value", 1.5f);
        }
    }
}
