using OMG.Minigames;
using UnityEngine;

namespace OMG.UI.Minigames
{
    public class MinigamePanel : MonoBehaviour
    {
        [SerializeField] ResultPanel resultPanel = null;
        public ResultPanel ResultPanel => resultPanel;

        [SerializeField] PlayerPanel playerPanel = null;
        public PlayerPanel PlayerPanel => playerPanel;

        protected Minigame minigame = null;

        private void Start()
        {
            Display(false);
        }

        public virtual void Init(Minigame minigame)
        {
            this.minigame = minigame;
            playerPanel?.Init(minigame);
        }

        public void Display(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
