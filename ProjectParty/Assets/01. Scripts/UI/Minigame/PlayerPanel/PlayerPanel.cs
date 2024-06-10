using OMG.Extensions;
using OMG.Minigames;
using UnityEngine;

namespace OMG.UI.Minigames
{
    public class PlayerPanel : MonoBehaviour
    {
        protected PlayerSlot[] playerSlots = null;
        
        protected virtual void Awake()
        {
            playerSlots = transform.GetComponentsInChildren<PlayerSlot>();
        }

        public virtual void Init()
        {
            // 플레이어 이미지 삽입
            playerSlots.ForEach((i, index) => {
                bool actived = MinigameManager.Instance.CurrentMinigame.PlayerDatas.Count > index;

                i.Display(actived);
                i.Init(null);
            });
        }
    }
}
