using OMG.Extensions;
using OMG.Minigames;
using UnityEngine;

namespace OMG.UI.Minigames.Deathmatches
{
    public class PlayerPanel : MonoBehaviour
    {
        private PlayerSlot[] playerSlots = null;

        private void Awake()
        {
            playerSlots = transform.GetComponentsInChildren<PlayerSlot>();
        }

        private void Start()
        {
            // 플레이어 이미지 삽입
            playerSlots.ForEach((i, index) => {
                bool actived = MinigameManager.Instance.CurrentMinigame.PlayerDatas.Count > index;
                Debug.Log(actived);
                i.Display(actived);
                i.Init(null);
            });
        }

        public void SetDead(int index)
        {
            playerSlots[index].SetDead(true);
        }
    }
}
