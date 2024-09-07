using UnityEngine;

namespace OMG.Minigames.CookingClass
{
    public class DispenserVisual : CharacterComponent
    {
        [SerializeField] Transform ovenTransform = null;
        [SerializeField] float rotateSpeed = 5f;

        public override void UpdateCompo()
        {
            base.UpdateCompo();
            ovenTransform?.Rotate(Vector3.up * (rotateSpeed * Time.deltaTime));
        }
    }
}
