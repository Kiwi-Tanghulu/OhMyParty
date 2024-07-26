#if UNITY_EDITOR
using OMG.Inputs;
using UnityEngine;

public class TJunsung : MonoBehaviour
{
    public Rigidbody rb;

    private void Start()
    {
        InputManager.ChangeInputMap(InputMapType.Play);
        rb.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.name);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            rb.gameObject.SetActive(true);
        }
    }
}
#endif