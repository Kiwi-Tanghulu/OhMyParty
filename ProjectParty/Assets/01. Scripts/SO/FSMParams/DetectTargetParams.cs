using UnityEngine;

[CreateAssetMenu(menuName = "SO/FSMParams/DetectTarget")]
public class DetectTargetParams : FSMParamSO
{
	public float Radius = 10f;
    public LayerMask TargetLayer = 0;
    public Transform Target = null;

    #if UNITY_EDITOR
    [Space(15f)]
    public bool GIZMO = false;

    public void DrawGizmo(Transform trm)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(trm.position, Radius);
    }
    #endif
}
