using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;
using OMG.Extensions;
using LightShaft.Scripts;

namespace OMG
{
    [RequireComponent(typeof(VideoPlayer))]
    public class OMGVideoPlayer : MonoBehaviour
    {
        public YoutubePlayer youtubePlayer;
        private VideoPlayer videoPlayer;

        [SerializeField] private Image playImage;
        [SerializeField] private Image stopImage;
        [SerializeField] private GameObject blindImage;

        private Tween videoPlayTween;
        private Tween videoStopTween;

        public UnityEvent OnPlayEvent;
        public UnityEvent OnStopEvent;

        private void Awake()
        {
            videoPlayer = GetComponent<VideoPlayer>();  

            Sequence videoPlaySeq = DOTween.Sequence();
            videoPlaySeq.Append(playImage.transform.DOScale(1f, 0f));
            videoPlaySeq.Join(playImage.DOFade(1f, 0f));
            videoPlaySeq.Append(playImage.transform.DOScale(0.75f, 0.2f));
            //videoPlaySeq.AppendCallback(() => blindImage.SetActive(false));
            videoPlaySeq.Append(playImage.transform.DOScale(3f, 0.2f));
            videoPlaySeq.Join(playImage.DOFade(0f, 0.2f));
            videoPlaySeq.SetAutoKill(false);
            videoPlayTween = videoPlaySeq;

            Sequence videoStopSeq = DOTween.Sequence();
            videoStopSeq.Append(stopImage.transform.DOScale(3f, 0f));
            videoStopSeq.Join(stopImage.DOFade(0f, 0f));
            videoStopSeq.Append(stopImage.transform.DOScale(0.75f, 0.2f));
            videoStopSeq.AppendCallback(() => blindImage.SetActive(true));
            videoStopSeq.Join(stopImage.DOFade(1f, 0.2f));
            videoStopSeq.Append(stopImage.transform.DOScale(1f, 0.2f));
            videoStopSeq.SetAutoKill(false);
            videoStopTween = videoStopSeq;
        }

        private void OnEnable()
        {
            playImage.transform.DOScale(1f, 0f);
            playImage.DOFade(1f, 0f);
            playImage.gameObject.SetActive(true);

            blindImage.SetActive(true);

            videoPlayer.Stop();
        }

        private void OnDisable()
        {
            videoPlayer.Stop();
        }

        public void Play(string url, float delay = 0f)
        {
            if (!gameObject.activeInHierarchy)
                return;

            StartCoroutine(this.DelayCoroutine(delay, () =>
            {
                playImage.gameObject.SetActive(true);
                stopImage.gameObject.SetActive(false);

                videoPlayTween.Restart();

                youtubePlayer.Play(url);

                OnPlayEvent?.Invoke();
            }));
        }

        public void Stop(float delay = 0f)
        {
            if (!gameObject.activeInHierarchy)
                return;

            StartCoroutine(this.DelayCoroutine(delay, () =>
            {
                playImage.gameObject.SetActive(false);
                stopImage.gameObject.SetActive(true);

                videoPlayer.Stop();

                videoStopTween.Restart();

                OnStopEvent?.Invoke();
            }));
        }
    }
}
