using OMG.Lobbies;
using OMG.Player;
using OMG.Utility;
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
    [SerializeField] private OptOption<Color> teamColorOption;

    [Space]
    private Dictionary<ulong, RenderTargetPlayerVisual> renderTargetPlayerDic;
    [SerializeField] private RenderTargetPlayerVisual cameraRenderPlayerVisual;
    [SerializeField] private Vector3 createPlayerVisualPos;
    [SerializeField] private Vector3 playerVisualPosOffset;
    public Dictionary<ulong, RenderTargetPlayerVisual> RenderTargetPlayerDic => renderTargetPlayerDic;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        renderTargetPlayerDic = new Dictionary<ulong, RenderTargetPlayerVisual>();
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

    public Color GetTeamColor(bool teamFlag)
    {
        return teamColorOption[teamFlag];
    }

    //create player render target
    public RenderTargetPlayerVisual CreatePlayerRenderTarget(PlayerController player)
    {
        Vector3 playerVisualPos =
            createPlayerVisualPos + (playerVisualPosOffset * renderTargetPlayerDic.Count);
        
        RenderTargetPlayerVisual playerVisual = Instantiate(
            cameraRenderPlayerVisual,
            playerVisualPos,
            Quaternion.identity,
            transform);

        //playerVisual.SetSkin(player.Visual.VisualType);
        //player.OnSpawnedEvent.AddListener(playerVisual.Init);
        playerVisual.Init(player);
        //playerVisual.SetOwenrID(player.OwnerClientId);

        renderTargetPlayerDic.Add(player.OwnerClientId, playerVisual);

        player.GetCharacterComponent<PlayerVisual>().OnSkinChangedEvent += playerVisual.SetSkin;

        return playerVisual;
    }

    public RenderTargetPlayerVisual GetPlayerRenderTargetVisual(ulong id)
    {
        return renderTargetPlayerDic[id];
    }
}
