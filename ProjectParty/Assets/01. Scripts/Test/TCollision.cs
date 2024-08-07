using UnityEngine;

public class TCollision : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
    {
        Debug.Log("asd");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
    }
}
