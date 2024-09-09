using OMG.Player;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerFallChecker : MonoBehaviour
{
    [SerializeField] private Transform teleportTrm;

    private void OnTriggerStay(Collider other)
    {
        FallCheck(other);
    }

    protected virtual void FallCheck(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<PlayerController>(out PlayerController player))
            {
                player.Respawn(teleportTrm.position, teleportTrm.eulerAngles);
            }
        }
    }
}
