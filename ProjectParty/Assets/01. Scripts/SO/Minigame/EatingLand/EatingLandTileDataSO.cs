using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Minigame/EatingLand/TileData")]
public class EatingLandTileDataSO : ScriptableObject
{
    [SerializeField] private List<Color> tileColors;
    [SerializeField] private List<Color> effectColors;
    public List<Color> TileColors => tileColors;
    public List<Color> EffectColors => effectColors;
}
