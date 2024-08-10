using OMG.Extensions;
using OMG.NetworkEvents;
using UnityEngine;

namespace OMG.Minigames
{
    public class DummyMinigame : Minigame
    {
        [SerializeField] float dummyPlayTime = 3f;

        protected override void OnGameStart(NoneParams ignore)
        {
            base.OnGameStart(ignore);

            if(IsHost == false)
                return;

            StartCoroutine(this.DelayCoroutine(dummyPlayTime, () => {
                Debug.Log("Finish Dummy");
                FinishGame();
            }));
        }
     
        protected override void OnGameFinish(NoneParams ignore)
        {
            if(IsHost == false)
                MinigameManager.Instance.FinishMinigame();

            base.OnGameFinish(ignore);
        }
    }
}
