using OMG.Extensions;
using UnityEngine;

namespace OMG.Minigames
{
    public class DummyMinigame : Minigame
    {
        [SerializeField] float dummyPlayTime = 3f;

        public override void StartGame()
        {
            StartCoroutine(this.DelayCoroutine(dummyPlayTime, () => {
                Debug.Log("Finish Dummy");
                FinishGame();
            }));
        }
     
        public override void FinishGame()
        {
            MinigameManager.Instance.FinishMinigame();
        }
    }
}
