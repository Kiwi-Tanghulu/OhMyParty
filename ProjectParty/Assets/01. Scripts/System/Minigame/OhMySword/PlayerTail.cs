using UnityEngine;

public class PlayerTail : MonoBehaviour
{
    private Renderer tailRenderer = null;
    private Material tailMaterial = null;

    private void Awake()
    {
        tailRenderer = GetComponent<Renderer>();
        tailMaterial = tailRenderer.material;
    }

    public void SetTailColor(ulong clientID)
    {
        Color tailColor = PlayerManager.Instance.GetPlayerColor((int)clientID);
        tailMaterial.SetColor("_BaseColor", tailColor);
    }
}