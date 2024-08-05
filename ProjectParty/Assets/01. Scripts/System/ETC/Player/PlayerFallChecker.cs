using OMG.Player;
using UnityEngine;

public class PlayerFallChecker : MonoBehaviour
{
    [SerializeField] private Transform teleportTrm;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<PlayerController>(out PlayerController player))
            {
                player.Respawn(teleportTrm.position, teleportTrm.eulerAngles);
                //.Teleport(teleportTrm.position, teleportTrm.rotation);
            }
        }
    }
}
