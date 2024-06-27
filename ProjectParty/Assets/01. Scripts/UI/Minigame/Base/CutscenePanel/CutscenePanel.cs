using System;
using System.Collections.Generic;
using OMG.Minigames;
using OMG.NetworkEvents;
using TMPro;
using UnityEngine;

namespace OMG.UI.Minigames
{
    public class CutscenePanel : MonoBehaviour
    {
        [SerializeField] TMP_Text skipText = null;
        private HashSet<ulong> skipPlayers = new HashSet<ulong>();
        private int playerCount = 0;

        private NetworkEvent<IntParams> OnCutsceneInitEvent = new NetworkEvent<IntParams>("CutsceneInitEvent");

        private void Awake()
        {
            OnCutsceneInitEvent.AddListener(HandleInit);
        }

        public void Init(Minigame minigame)
        {
            OnCutsceneInitEvent?.Register(minigame.NetworkObject);

            if(minigame.IsHost)
                OnCutsceneInitEvent?.Broadcast(minigame.PlayerDatas.Count);
        }

        public void Display(bool active)
        {
            gameObject.SetActive(active);
        }

        public void SetSkip(ulong clientID)
        {
            skipPlayers.Add(clientID);
            SetText();
        }

        private void SetText() => skipText.text = $"{skipPlayers.Count}/{playerCount}";

        private void HandleInit(IntParams count)
        {
            playerCount = count;
            SetText();
        }
    }
}
