using System;
using System.Collections.Generic;
using OMG.Extensions;
using OMG.NetworkEvents;
using OMG.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace OMG.Minigames
{
    public abstract class TeamMinigameCycle : MinigameCycle
    {
        [Serializable]
        public class TeamData
        {
            public List<ulong> members;
            public int score;

            public TeamData(int memberCount)
            {
                members = new List<ulong>(memberCount);
                score = 0;
            }
        }

        [SerializeField] OptOption<int> teamQuota = new OptOption<int>(2, 2);
        [SerializeField] OptOption<int> scoreOption = new OptOption<int>(0, 500);

        private OptOption<TeamData> teamTable = null;
        private Dictionary<ulong, bool> teamInfo = null;
        public Dictionary<ulong, bool> TeamInfo => teamInfo;

        private NetworkEvent<TeamParams> onTeamDecidedEvent = new NetworkEvent<TeamParams>("teamDecided");

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            onTeamDecidedEvent.AddListener(HandleTeamDecided);
            onTeamDecidedEvent.Register(NetworkObject);

            teamInfo = new Dictionary<ulong, bool>(minigame.PlayerDatas.Count);
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            onTeamDecidedEvent.Unregister();
        }

        public void DecideTeam()
        {
            if(IsHost == false)
                return;

            teamTable = new OptOption<TeamData>(new TeamData(teamQuota[true]), new TeamData(teamQuota[false]));
            teamInfo.Clear();

            List<ulong> minigamePlayers = new List<ulong>(minigame.PlayerDatas.Count);
            int minigamePlayersCount = minigamePlayers.Count;
            for(int i = 0; i < minigamePlayersCount; ++i)
            {
                minigamePlayers[i] = minigame.PlayerDatas[i].clientID;
                teamInfo.Add(minigamePlayers[i], true);
            }

            minigamePlayers = minigamePlayers.Shuffle();
            bool teamFlag;
            int index;
            for (int i = 0; i < minigamePlayersCount; ++i)
            {
                if(i % 2 == 0 && i / 2 < teamQuota[true])
                    teamFlag = true;
                else if(i / 2 < teamQuota[false])
                    teamFlag = false;
                else
                    teamFlag = true;

                index = Random.Range(0, minigamePlayers.Count);
                SetTeam(teamFlag, minigamePlayers[index]);
                minigamePlayers.RemoveAt(index);
            }
        }

        private void SetTeam(bool teamFlag, ulong clientID)
        {
            teamInfo[clientID] = teamFlag;
            onTeamDecidedEvent?.Broadcast(new TeamParams(teamFlag, clientID));

            int remainIndex = teamTable[teamFlag].members.IndexOf(clientID);
            if(remainIndex != -1)
                return;
                
            remainIndex = teamTable[!teamFlag].members.IndexOf(clientID);
            if(remainIndex != -1)
                teamTable[!teamFlag].members.RemoveAt(remainIndex);

            teamTable[teamFlag].members.Add(clientID);
        }

        public void SetTeamScore(bool teamFlag, int score)
        {
            if(IsHost == false)
                return;

            if(teamTable == null)
                return;

            teamTable[teamFlag].score = score;
        }

        public void ApplyScore()
        {
            if(IsHost == false)
                return;

            if(teamTable[true].score == teamTable[false].score)
            {
                int score = (scoreOption[true] + scoreOption[false]) / 2;
                minigame.PlayerDatas.ChangeAllData(i => {
                    i.score = score;
                    return i;
                });
            }
            else
            {
                bool winningTeam = teamTable[true].score > teamTable[false].score;

                teamTable[true].members.ForEach(id => {
                    minigame.PlayerDatas.ChangeData(i => i.clientID == id, player => {
                        player.score = scoreOption[winningTeam == true];
                        return player;
                    });
                });
                
                teamTable[false].members.ForEach(id => {
                    minigame.PlayerDatas.ChangeData(i => i.clientID == id, player => {
                        player.score = scoreOption[winningTeam == false];
                        return player;
                    });
                });
            }
        }

        private void HandleTeamDecided(TeamParams teamParams)
        {
            if(teamInfo.ContainsKey(teamParams.ClientID))
                teamInfo[teamParams.ClientID] = teamParams.TeamFlag;
            else
                teamInfo.Add(teamParams.ClientID,teamParams.TeamFlag);
        }
    }
}
