using OMG.Tweens;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyTile : MonoBehaviour
    {
        [SerializeField] TweenOptOption tweenOption = null;

        private SafetyTileCollision tileCollision = null;
        private SafetyTileVisual tileVisual = null;
        private SafetyTileBlock block = null;

        private int safetyNumber = 0;

        private void Awake()
        {
            tileCollision = transform.Find("Collision").GetComponent<SafetyTileCollision>();
            tileVisual = transform.Find("Visual").GetComponent<SafetyTileVisual>();
            block = transform.Find("Block").GetComponent<SafetyTileBlock>();

            tweenOption.Init(transform);

            tileCollision.OnPlayerCountChangedEvent += HandlePlayerCountChanged;
        }

        public void SetSafetyNumber(int number)
        {
            safetyNumber = number;
            tileVisual.SetNumberText(safetyNumber);
            ToggleBlock(IsSafetyZone());
        }

        public bool IsSafetyZone()
        {
            return tileCollision.IncludePlayerCount == safetyNumber;
        }

        public void SetActive(bool active)
        {
            tweenOption.GetOption(active).PlayTween();
        }

        public void ToggleBlock(bool active, bool immediately = false)
        {
            block.SetActive(active, immediately);
        }

        public void Init()
        {
            safetyNumber = 100;
            tileVisual.SetNumberText(-1);

            ToggleBlock(false);
            gameObject.SetActive(true);
        }

        public void Reset()
        {
            safetyNumber = 100;
            tileVisual.SetNumberText(-1);

            ToggleBlock(false, true);
            SetActive(true);
        }

        private void HandlePlayerCountChanged()
        {
            ToggleBlock(IsSafetyZone());
        }
    }
}
