using UnityEngine;
using UnityEngine.UI;

namespace OMG.UI.Minigames.Deathmatches
{
    public class PlayerSlot : MonoBehaviour
    {
        private RawImage playerImage = null;
        private GameObject deadCheck = null;

        private void Awake()
        {
            playerImage = transform.Find("PlayerImage").GetComponent<RawImage>();
            deadCheck = transform.Find("DeadCheck").gameObject;
        }

        public void Init(RenderTexture playerIcon)
        {
            playerImage.texture = playerIcon;
            SetDead(false);
        }

        public void Display(bool active)
        {
            gameObject.SetActive(active);
        }

        public void SetDead(bool dead)
        {
            deadCheck.SetActive(dead);
        }
    }
}
