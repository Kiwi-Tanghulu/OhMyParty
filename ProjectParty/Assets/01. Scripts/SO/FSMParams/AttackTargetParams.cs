using UnityEngine;

[CreateAssetMenu(menuName = "SO/FSMParams/AttackTarget")]
public class AttackTargetParams : FSMParamSO
{
	public float AttackRange = 1f;
    public float AttackCooldown = 0.5f;
    public int Damage = 10;
    public bool IsCooldown = false;

    #if UNITY_EDITOR
    [Space(15f)]
    public bool GIZMO = false;

    public void DrawGizmo(Transform trm)
    {
        if(GIZMO == false)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(trm.position, AttackRange);
    }
    #endif
}
