using TMPro;
using UnityEngine;

namespace OMG.UI.Minigames
{
    public class TimerPanel : MonoBehaviour
    {
        [SerializeField] TMP_Text timeText = null;

        public void SetText(float time)
        {
            time = Mathf.Max(0f, time);
            timeText.text = Mathf.FloorToInt(time).ToString();
        }
    }
}
