using System.Collections;
using OMG.Extensions;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.RockFestival
{
    public class ScoreArea : MonoBehaviour
    {
        [SerializeField] float updateDelay = 1f;
        private TMP_Text scoreText = null;

        private NetworkList<PlayerData> players = null;

        private bool active = false;

        private int prevPoint = 0;
        private int pointBuffer = 0;

        private ulong ownerID = 0;

        private void Awake()
        {
            scoreText = transform.Find("ScoreText").GetComponent<TMP_Text>();
            scoreText.text = "-";
        }

        public void Init(ulong ownerID, bool active)
        {
            if(active == false)
                return;

            this.ownerID = ownerID;
            players = MinigameManager.Instance.CurrentMinigame.PlayerDatas;
        }

        public void SetActive(bool active, bool isHost)
        {
            this.active = active;
            if(isHost == false)
                return;

            if(active)
                StartCoroutine(UpdateCoroutine());
            else
            {
                StopAllCoroutines();
                UpdateScore();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(active == false)
                return;

            if(other.CompareTag("Point") == false)
                return;

            UpdatePointBuffer(1);
        }

        private void OnTriggerExit(Collider other)
        {
            if(active == false)
                return;

            if (other.CompareTag("Point") == false)
                return;

            UpdatePointBuffer(-1);
        }

        private void UpdatePointBuffer(int value)
        {
            pointBuffer += value;
            scoreText.text = pointBuffer.ToString();
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
