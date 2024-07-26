using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OMG.UI.Minigames
{
    public class TimerPanel : MonoBehaviour
    {
        [SerializeField] Image timerImage = null;
        [SerializeField] TMP_Text timeText = null;

        public void SetTimerUI(float ratio, float time)
        {
            time = Mathf.Max(0f, time);
            timeText.text = Mathf.FloorToInt(time).ToString();

            if(timerImage != null)
                timerImage.fillAmount = ratio;
        }
    }
}
