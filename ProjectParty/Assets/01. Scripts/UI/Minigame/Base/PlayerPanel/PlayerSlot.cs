using UnityEngine;
using UnityEngine.UI;

namespace OMG.UI.Minigames
{
    public class PlayerSlot : MonoBehaviour
    {
        [SerializeField] private RawImage playerImage = null;
        private ulong clientID = 0;
        public ulong ClientID => clientID;

        protected virtual void Awake()
        {
            //playerImage = transform.Find("PlayerImage").GetComponent<RawImage>();
        }

        public virtual void Init(RenderTexture playerIcon)
        {
            playerImage.texture = playerIcon;
        }

        public void SetClientID(ulong clientID)
        {
            this.clientID = clientID;
        }

        public virtual void Display(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
