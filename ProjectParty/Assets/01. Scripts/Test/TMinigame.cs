using UnityEngine;
using OMG.Minigames;

namespace OMG.Test
{
    public class TMinigame : MonoBehaviour
    {
        [SerializeField] Minigame minigame;

        public void StartMinigame()
        {
            MinigameManager.Instance.StartMinigame(minigame);
        }
    }
}
