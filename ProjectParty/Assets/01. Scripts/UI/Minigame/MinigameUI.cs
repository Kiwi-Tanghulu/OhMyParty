using UnityEngine;

namespace OMG.UI.Minigames
{
    public class MinigameUI : MonoBehaviour
    {
        [SerializeField] ResultPanel resultPanel = null;
        public ResultPanel ResultPanel => resultPanel;

        [SerializeField] PlayerPanel playerPanel = null;
        public PlayerPanel PlayerPanel => playerPanel;
    }
}
