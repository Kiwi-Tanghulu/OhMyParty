using UnityEngine;

namespace OMG.ColorControlling
{
    public class PlayerColorController : ColorController
    {
        [SerializeField] int index = 0;

        public void SetIndex(int index)
        {
            this.index = index;
        }

        protected override Color GetColor()
        {
            return PlayerManager.Instance.GetPlayerColor(index);
        }
    }
}