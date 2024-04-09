using System.Collections;
using OMG.Extensions;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.RockFestival
{
    public class ScoreArea : MonoBehaviour
    {
        [SerializeField] float updateDelay = 1f;

        private NetworkList<PlayerData> players = null;

        private bool active = false;

        private int prevPoint = 0;
        private int pointBuffer = 0;

        private ulong ownerID = 0;

        public void Init(ulong ownerID)
        {
            this.ownerID = ownerID;
            players = MinigameManager.Instance.CurrentMinigame.JoinedPlayers;
        }

        public void SetActive(bool active)
        {
            this.active = active;
            if(active)
                StartCoroutine(UpdateCoroutine());
            else
            {
                StopAllCoroutines();
                UpdateScore();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if(other.rigidbody.CompareTag("Rock") == false)
                return;

            pointBuffer++;
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.rigidbody.CompareTag("Rock") == false)
                return;

            pointBuffer--;
        }

        private IEnumerator UpdateCoroutine()
        {
            YieldInstruction delay = new WaitForSeconds(updateDelay);

            while(active)
            {
                UpdateScore();
                yield return delay;
            }
        }

        private void UpdateScore()
        {
            if (prevPoint != pointBuffer)
            {
                prevPoint = pointBuffer;
                players.ChangeData(i => i.clientID == ownerID, data => {
                    data.score = pointBuffer;
                    return data;
                });
            }
        }
    }
}
