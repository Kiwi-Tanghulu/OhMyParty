using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField] private PlayerVisualSOListSO playerVisualList;
    public PlayerVisualSOListSO PlayerVisualList => playerVisualList;

    [Space]
    [SerializeField] private List<Color> playerColorList;

    [Space]
    private List<RenderTexture> playerRenderTextureList;
    private List<Camera> playerRenderCameraList;
    [SerializeField] private PlayerVisual playerVisualPrefab;
    [SerializeField] private Vector3 createPlayerVisualPos;
    [SerializeField] private Vector3 playerVisualPosOffset;
    [SerializeField] private Vector3 cameraOffset;
    public List<RenderTexture> PlayerRenderTextureList => playerRenderTextureList;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        playerRenderTextureList = new List<RenderTexture>();
        playerRenderCameraList = new List<Camera>();
    }

    public Color GetPlayerColor(int index)
    {
        if (index < 0 || index >= playerColorList.Count)
        {
            Debug.LogError("color index found fail");
            return default;
        }
        return playerColorList[index];
    }

    public RenderTexture CreatePlayerRenderTexture(PlayerController player)
    {
        Vector3 playerVisualPos =
            createPlayerVisualPos + (playerVisualPosOffset * playerRenderTextureList.Count);

        Transform playerVisualRenderTrm = new GameObject($"PlayerRender{playerRenderTextureList.Count}").transform;
        playerVisualRenderTrm.position = playerVisualPos;
        playerVisualRenderTrm.SetParent(transform);
        

        PlayerVisual playerVisual = Instantiate(
            playerVisualPrefab,
            playerVisualPos,
            Quaternion.Euler(0f, 180f, 0f),
            playerVisualRenderTrm);
        playerVisual.SetVisual(player.Visual.VisualType);

        RenderTexture rt = new RenderTexture(256, 256, 16);
        rt.Create();
        playerRenderTextureList.Add(rt);

        Transform camTrm= new GameObject("Cam").transform;
        camTrm.SetParent(playerVisualRenderTrm);
        camTrm.localPosition = cameraOffset;

        Camera cam = camTrm.gameObject.AddComponent<Camera>();
        cam.targetTexture = rt;
        playerRenderCameraList.Add(cam);

        return rt;
    }
}
