using UnityEngine;

namespace OMG.UI.Minigames
{
    public class DeathmatchPlayerSlot : PlayerSlot
    {
        private GameObject deadCheck = null;

        protected override void Awake()
        {
            base.Awake();
            deadCheck = transform.Find("DeadCheck").gameObject;
        }

        public override void Init(RenderTexture playerIcon)
        {
            base.Init(playerIcon);
            SetDead(false);
        }

        public void SetDead(bool dead)
        {
            deadCheck.SetActive(dead);
        }
    }
}
