using System.Collections.Generic;
using OMG.Extensions;
using OMG.Minigames;
using UnityEngine;

namespace OMG.UI.Minigames
{
    public class PlayerPanel : MonoBehaviour
    {
        protected List<PlayerSlot> playerSlots = null;

        public PlayerSlot this[int index] => playerSlots[index];
        
        protected virtual void Awake()
        {
            playerSlots = new List<PlayerSlot>();
            transform.GetComponentsInChildren<PlayerSlot>(playerSlots);
        }

        public virtual void Init(Minigame minigame)
        {
            // 플레이어 이미지 삽입
            playerSlots.ForEach((i, index) => {
                bool actived = minigame.PlayerDatas.Count > index;
                i.Display(actived);

                if (!actived)
                    return;

                ulong id = minigame.PlayerDatas[index].clientID;
                RenderTexture playerRenderTexture = PlayerManager.Instance.GetPlayerRenderTargetVisual(id).RenderTexture;

                i.Init(playerRenderTexture);
            });
        }
    }
}
