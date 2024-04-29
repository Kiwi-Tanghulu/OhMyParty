using OMG.Interiors;
using OMG.Tweens;
using UnityEngine;

namespace OMG.UI.Interiors
{
    public class PropModifyPanel : MonoBehaviour
    {
        [SerializeField] TweenOptOption tweenOption = null;

        [Space(15f)]
        [SerializeField] InteriorSystem interiorSystem = null;
        [SerializeField] InteriorPresetComponent presetComponent = null;
        [SerializeField] InteriorGridComponent gridComponent = null;

        [SerializeField] InteriorProp currentProp = null;

        private void Awake()
        {
            tweenOption.Init(transform);
        }

        public void Init(InteriorProp prop)
        {
            currentProp = prop;
        }

        public void Display(bool active)
        {
            tweenOption?.GetOption(active)?.PlayTween();
        }

        public void OnRotateClick()
        {
            int prev = currentProp.PlacementData.Rotate;
            int rotate = currentProp.PlacementData.Rotate;
            rotate = (rotate + 4 + 1) % 4;
            presetComponent.ModifyPlacement(currentProp.PlacementData.GridIndex, rotate);
            


            Vector3 pivot = gridComponent.GetGridPosition(currentProp.PlacementData.GridIndex);
            currentProp.transform.RotateAround(pivot, Vector3.up, (prev - rotate) * 90f);

            currentProp.PlacementData.Rotate = rotate;
        }

        public void OnMoveClick()
        {

        }

        public void OnSellClick()
        {

        }

        public void OnExitClick()
        {

        }
    }
}
