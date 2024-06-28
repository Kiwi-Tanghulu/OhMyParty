using OMG.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;

namespace OMG.Lobbies
{
    public class MinigameSpotTVUI : UIObject
    {
        private CountText roundCountText;

        [SerializeField] private PlayerGraph graphPrefab;
        private Transform playerGraphContainer;
        private List<PlayerGraph> graphList;
        private TextMeshProUGUI noticeText;

        public override void Init()
        {
            base.Init();
            
            roundCountText = transform.Find("MinigameCountText").GetComponent<CountText>();
            roundCountText.Init();

            playerGraphContainer = transform.Find("PlayerGraphContainer");
            graphList = new List<PlayerGraph>();

            noticeText = transform.Find("NoticeText").GetComponent<TextMeshProUGUI>();

            Lobby.Current.GetLobbyComponent<LobbyMinigameComponent>().OnMinigameFinishedEvent += MinigameSpotTVUI_OnMinigameFinishedEvent;

            CreatePlayerGraph();
        }

        private void MinigameSpotTVUI_OnMinigameFinishedEvent(Minigames.Minigame arg1, bool arg2)
        {
            noticeText.gameObject.SetActive(false);
            playerGraphContainer.gameObject.SetActive(true);
        }

        #region count text
        public void SetRoundCountText(int round)
        {
            roundCountText.SetCount(round);
        }

        public void SetRoundCountValue(int cycleCount)
        {
            roundCountText.SetCountValue(0, cycleCount);
        }

        public void PlayRoundTextChangeAnim(float delay, Action onStart, Action onEnd)
        {
            roundCountText.PlayAnim(delay, onStart, onEnd);
        }
        #endregion

        #region player graph
        private void CreatePlayerGraph()
        {
            foreach(PlayerData data in Lobby.Current.PlayerDatas)
            {
                PlayerGraph graph = Instantiate(graphPrefab, playerGraphContainer);
                graph.Init(data.ClientID, 0f);
                graphList.Add(graph);
            }
        }

        public void SetPlayerGraphScore(List<PlayerData> dataList)
        {
            int loop = graphList.Count > dataList.Count ? graphList.Count : dataList.Count;
            for(int i = 0; i < loop; i++) 
            {
                SetPlayerGraphScore(dataList[i]);
            }
        }

        public void SetPlayerGraphScore(PlayerData data)
        {
            PlayerGraph graph = graphList.Find(x => x.OwenrID == data.ClientID);
            graph.SetValue(data.Score);
        }
        #endregion
    }
}