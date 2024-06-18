using OMG.Player;
using UnityEngine;

public class PlayerFallChecker : MonoBehaviour
{
    [SerializeField] private Transform teleportTrm;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<PlayerMovement>(out PlayerMovement movement))
            {
                movement.Teleport(teleportTrm.position, teleportTrm.rotation);
            }
        }
    }
}
