using OMG.Extensions;
using OMG.Lobbies;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

namespace OMG.UI
{
    public class PlayerGraph : UIObject
    {
        [SerializeField] private PlayerReadyCheckBox playerProfile;
        [SerializeField] private RectTransform barRectTrm;
        [SerializeField] private Image groundImage;
        [SerializeField] private TextMeshProUGUI valueText;

        [SerializeField] private float maxGraphSize = 300f;
        [SerializeField] private float maxScore = 3000f;

        public ulong OwenrID { get; private set; }

        public void Init(ulong playerID, float value)
        {
            base.Init();

            OwenrID = playerID;
            SetPlayer(playerID);
            SetValue(value);

            int index = OMG.Lobbies.Lobby.Current.PlayerDatas.IndexOf(i => i.ClientID == playerID);
            Color playerColor = PlayerManager.Instance.GetPlayerColor(index);
            barRectTrm.GetComponent<Image>().color = playerColor;
            groundImage.color = playerColor;    
        }

        public void SetPlayer(ulong playerID)
        {
            playerProfile.SetPlayerImage(PlayerManager.Instance.GetPlayerRenderTargetVisual(playerID).RenderTexture);
            OMG.Lobbies.Lobby.Current.PlayerDatas.Find(out PlayerData data, (data) => data.ClientID == playerID);
            playerProfile.SetNameText(data.Nickname);
        }

        public void SetValue(float value)
        {
            Vector2 size = new Vector2(barRectTrm.rect.width, Mathf.Clamp01(value / maxScore) * maxGraphSize);
            barRectTrm.sizeDelta = size;
            if(valueText != null )
            {
                valueText.text = value.ToString();
            }
        }
    }
}