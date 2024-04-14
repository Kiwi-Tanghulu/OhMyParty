using System.Collections;
using UnityEngine;

namespace OMG.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class BGMPlayer : AudioPlayer
    {
        [SerializeField] private string[] bgmList;
        [SerializeField] private float replayDelay;
        [SerializeField] private bool playOnAwake;
        private AudioClip currentBgm;

        private void Start()
        {
            if (playOnAwake)
                StartBGM();
        }

        public void StartBGM()
        {
            StopAllCoroutines();
            StartCoroutine(PlayBGM());
        }

        public void SetBGMList(string[] bgmList)
        {
            this.bgmList = bgmList;
        }

        private IEnumerator PlayBGM()
        {
            while (true)
            {
                string bgmName = bgmList[UnityEngine.Random.Range(0, bgmList.Length)];
                currentBgm = audioLibrary[bgmName];
                PlayAudio(bgmName);

                yield return new WaitForSeconds(currentBgm.length + replayDelay);
            }
        }
    }
}
