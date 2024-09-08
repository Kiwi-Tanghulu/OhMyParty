using UnityEngine;

namespace OMG.ColorControlling
{
    public class PlayerColorController : ColorController
    {
        [SerializeField] int index = 0;

        protected override Color GetColor()
        {
            return PlayerManager.Instance.GetPlayerColor(index);
        }
    }
}