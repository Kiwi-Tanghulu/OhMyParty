using UnityEngine;
using OMG.Minigames;

namespace OMG.Test
{
    public class TMinigame : MonoBehaviour
    {
        [SerializeField] MinigameSO minigameData;

        public void StartMinigame()
        {
            MinigameManager.Instance.StartMinigame(minigameData);
        }
    }
}
