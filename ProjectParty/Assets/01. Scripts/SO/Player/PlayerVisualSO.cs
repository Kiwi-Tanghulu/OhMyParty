using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Player/PlayerVisualSO")]
public class PlayerVisualSO : ScriptableObject
{
    [SerializeField] private PlayerVisualType visualType;
    [SerializeField] private Sprite profile;
    [SerializeField] private Mesh visualMesh;

    public PlayerVisualType VisualType => visualType;
    public Sprite Profile => profile;
    public Mesh VisualMesh => visualMesh;
}
