using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField] private PlayerVisualInfoListSO playerVisualList;
    public PlayerVisualInfoListSO playerVisualInfo => playerVisualList;
    [SerializeField] private List<Color> playerColorList;
    

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
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
}
