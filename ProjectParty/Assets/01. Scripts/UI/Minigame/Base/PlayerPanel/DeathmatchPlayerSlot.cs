using UnityEngine;

namespace OMG.UI.Minigames
{
    public class DeathmatchPlayerSlot : PlayerSlot
    {
        [SerializeField] GameObject deadCheck = null;
        [SerializeField] GameObject deadCheckPrefab = null;
        [SerializeField] Transform deadCheckContainer = null;

        protected override void Awake()
        {
            base.Awake();
            //deadCheck = transform.Find("DeadCheck").gameObject;
        }

        public override void Init(RenderTexture playerIcon)
        {
            base.Init(playerIcon);
            SetDead(false);
            SetDead(0);
        }

        public void SetDead(int index)
        {

            foreach(Transform deadCheck in deadCheckContainer)
                Destroy(deadCheck.gameObject);

            for(int i = 0; i < index; ++i)
                Instantiate(deadCheckPrefab, deadCheckContainer);
        }

        public void SetDead(bool isDead)
        {
            deadCheck.SetActive(isDead);
        }
    }
}
