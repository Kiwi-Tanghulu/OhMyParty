using OMG.Items;
using OMG.Minigames.MazeAdventure;
using UnityEngine;

public class MazeAdventureItem : NetworkItem
{
    [SerializeField] private ItemType itemType;
    public ItemType ItemType => itemType;
    protected Transform playerTrm = null;
    public virtual void Init(Transform playerTrm) 
    {
        this.playerTrm = playerTrm;  
    }
    public override void OnActive()
    {
        
    }
}
