using System.Collections;
using OMG.Extensions;
using OMG.UI.Minigames;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.RockFestival
{
    public class ScoreArea : MonoBehaviour
    {
        [SerializeField] ScorePlayerSlot scoreSlot = null;
        [SerializeField] float updateDelay = 1f;
        private TMP_Text scoreText = null;

        private Minigame minigame = null;

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
            minigame = MinigameManager.Instance.CurrentMinigame;
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
            
            if(other.TryGetComponent<Rock>(out Rock rock))
                UpdatePointBuffer(rock.Point);
        }

        private void OnTriggerExit(Collider other)
        {
            if(active == false)
                return;

            if (other.CompareTag("Point") == false)
                return;

            if(other.TryGetComponent<Rock>(out Rock rock))
                UpdatePointBuffer(-rock.Point);
        }

        private void UpdatePointBuffer(int value)
        {
            pointBuffer += value;
            scoreText.text = pointBuffer.ToString();
            scoreSlot.SetScore(pointBuffer);
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
            if (minigame.State != MinigameState.Playing)
                return;

            if (prevPoint != pointBuffer)
            {
                prevPoint = pointBuffer;
                minigame.PlayerDatas.ChangeData(i => i.clientID == ownerID, data => {
                    data.score = pointBuffer;
                    return data;
                });
            }
        }
    }
}
