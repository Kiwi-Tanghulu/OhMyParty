using OMG.Input;
using OMG.Interiors;
using OMG.Tweens;
using UnityEngine;

namespace OMG.UI.Interiors
{
    public class PropModifyPanel : MonoBehaviour
    {
        [SerializeField] UIInputSO input = null;
        [SerializeField] TweenOptOption tweenOption = null;

        [Space(15f)]
        [SerializeField] InteriorSystem interiorSystem = null;
        [SerializeField] InteriorPresetComponent presetComponent = null;
        [SerializeField] InteriorGridComponent gridComponent = null;

        [Space(15f)]
        [SerializeField] float yOffset = -30f;

        private Camera mainCam = null;
        private InteriorProp currentProp = null;

        public bool Active {get;private set;} = false;

        private void Awake()
        {
            mainCam = Camera.main;
            tweenOption.Init(transform);
        }

        private void Start()
        {
            transform.localScale = Vector3.zero;
        }

        public void Init(InteriorProp prop)
        {
            currentProp = prop;

            Vector3 worldPosition = prop.transform.position;
            transform.position = mainCam.WorldToScreenPoint(worldPosition) + Vector3.up * yOffset;
        }

        public void Display(bool active)
        {
            tweenOption?.GetOption(active)?.PlayTween();
            Active = active;
        }

        public void OnRotateClick()
        {
            int prev = currentProp.PlacementData.Rotate;
            int rotate = currentProp.PlacementData.Rotate;
            rotate = (rotate + 4 + 1) % 4;
            presetComponent.ModifyPlacement(currentProp.PlacementData.GridIndex, rotate);

            Vector3 pivot = gridComponent.GetGridPosition(currentProp.PlacementData.GridIndex);
            currentProp.transform.RotateAround(pivot, Vector3.up, (rotate - prev) * 90f);

            currentProp.PlacementData.Rotate = rotate;
        }

        public void OnMoveClick()
        {
            interiorSystem.OnPropPlacedEvent += HandlePropPlaced;

            presetComponent.RemovePlacement(currentProp);
            interiorSystem.SetPropData(currentProp.PropData.PropID);
            Destroy(currentProp.gameObject);
            currentProp = null;

            Display(false);
        }

        public void OnSellClick()
        {
            Debug.Log($"Sell Prop");
            presetComponent.RemovePlacement(currentProp);
            Destroy(currentProp.gameObject);
            Display(false);
        }

        public void OnExitClick()
        {
            currentProp = null;
            Display(false);
        }

        private void HandlePropPlaced(InteriorProp prop)
        {
            interiorSystem.OnPropPlacedEvent -= HandlePropPlaced;
            interiorSystem.ClearPropData();
            Init(prop);
            Display(true);
        }
    }
}
