using OMG.Inputs;
using UnityEngine;

namespace OMG.UI.Solid
{
    public class SolidUICaster : MonoBehaviour
    {
        [SerializeField] UIInputSO input;

        [Space(15f)]
        [SerializeField] Camera castCamera = null;
        [SerializeField] LayerMask castLayer = 0;

        private SolidUI currentUI = null;

        private void Awake()
        {
            input.OnLeftClickEvent += HandleLeftClick;
            InputManager.ChangeInputMap(InputMapType.UI);
        }

        private void Update()
        {
            CastUIResult result = CastUI();
            
            if(result.ui?.Active == false)
                return;

            if(currentUI == result.ui)
                return;

            FocusUI(result.ui, result.point);
        }

        private void FocusUI(SolidUI ui, Vector3 point)
        {
            (currentUI as IPointerExit)?.PointerExitEvent(point);
            currentUI = ui;
            (currentUI as IPointerEnter)?.PointerEnterEvent(point);
        }

        private CastUIResult CastUI()
        {
            CastUIResult result = new CastUIResult();

            Ray ray = castCamera.ScreenPointToRay(input.MousePosition);
            float maxDistance = castCamera.farClipPlane;

            bool castResult = Physics.Raycast(ray, out RaycastHit hit, maxDistance, castLayer);
            if(castResult)
                castResult &= hit.transform.TryGetComponent<SolidUI>(out result.ui);
            result.point = castResult ? hit.point : ray.direction * maxDistance;

            return result;
        }

        private void HandleLeftClick(bool isClick)
        {
            if(currentUI == null)
                return;

            if(isClick)
                (currentUI as IPointerDown)?.PointerDownEvent(input.MousePosition);
            else
                (currentUI as IPointerUp)?.PointerUpEvent(input.MousePosition);
        }
    }
}
