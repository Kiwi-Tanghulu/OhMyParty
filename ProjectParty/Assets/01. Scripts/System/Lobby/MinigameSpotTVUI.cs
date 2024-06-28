using OMG.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
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

        public override void Init()
        {
            base.Init();
            
            roundCountText = transform.Find("MinigameCountText").GetComponent<CountText>();
            roundCountText.Init();

            playerGraphContainer = transform.Find("PlayerGraphContainer");
            graphList = new List<PlayerGraph>();

            CreatePlayerGraph();
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