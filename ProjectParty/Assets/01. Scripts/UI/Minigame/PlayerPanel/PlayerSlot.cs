using UnityEngine;
using UnityEngine.UI;

namespace OMG.UI.Minigames
{
    public class PlayerSlot : MonoBehaviour
    {
        [SerializeField] private RawImage playerImage = null;

        protected virtual void Awake()
        {
            //playerImage = transform.Find("PlayerImage").GetComponent<RawImage>();
        }

        public virtual void Init(RenderTexture playerIcon)
        {
            playerImage.texture = playerIcon;
        }

        public virtual void Display(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
