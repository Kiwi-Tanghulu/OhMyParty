using UnityEngine;

public class PlayerOutLine : MonoBehaviour
{
    private Outline outLine = null;


    private void Awake()
    {
        outLine = gameObject.AddComponent<Outline>();
        outLine.OutlineMode = Outline.Mode.OutlineHidden;
    }

    public void SettingOutLine(int colorIndex)
    {
        outLine.OutlineMode = Outline.Mode.OutlineAll;
        outLine.OutlineColor = PlayerManager.Instance.GetPlayerColor(colorIndex);
        Debug.Log("플레이어 색 : " + colorIndex);
        outLine.OutlineWidth = 2.5f;
    }
}
