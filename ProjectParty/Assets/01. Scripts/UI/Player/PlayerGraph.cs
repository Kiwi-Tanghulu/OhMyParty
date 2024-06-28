using OMG.Extensions;
using OMG.Lobbies;
using TMPro;
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

        public ulong OwenrID { get; private set; }

        public void Init(ulong playerID, float value)
        {
            base.Init();

            OwenrID = playerID;
            SetPlayer(playerID);
            SetValue(value);

            Color playerColor = PlayerManager.Instance.GetPlayerColor((int)OwenrID);
            barRectTrm.GetComponent<Image>().color = playerColor;
            groundImage.color = playerColor;    
        }

        public void SetPlayer(ulong playerID)
        {
            playerProfile.SetPlayerImage(PlayerManager.Instance.GetPlayerRenderTargetVisual(playerID).RenderTexture);
            Lobby.Current.PlayerDatas.Find(out PlayerData data, (data) => data.ClientID == playerID);
            playerProfile.SetNameText(data.Nickname);
        }

        public void SetValue(float value)
        {
            Vector2 size = new Vector2(barRectTrm.rect.width, value);
            barRectTrm.sizeDelta = size;
            if(valueText != null )
            {
                valueText.text = value.ToString();
            }
        }
    }
}