using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.Race
{
    public class Podium : MonoBehaviour
    {
        [SerializeField] private PlayerVisual[] visuals;

        private Race race;

        private void Awake()
        {
            race = MinigameManager.Instance.CurrentMinigame as Race;
        }

        private void OnEnable()
        {
            BindingVisual();
        }

        private void BindingVisual()
        {
            for(int i = 0; i < race.Rank.Count; i++)
            {
                if (race.Players[race.Rank[i]].TryGet(out NetworkObject networkObject))
                {
                    visuals[i].SetSkin(networkObject.GetComponent<PlayerController>().Visual.VisualType);
                }
            }
        }
    }
}