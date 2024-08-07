using OMG.Player;
using OMG.Ragdoll;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollSignalHandler : MonoBehaviour
{
	[SerializeField] private List<TimelineRagdollController> _ragdollControllers;
	[SerializeField] private float _force = 5f;

	private int _currentIdx = 0;

	private void Awake()
	{
		_currentIdx = 0;
	}

	public void HandleSignal()
	{
		Debug.Log("Ragdoll Signal");

		TimelineRagdollController controller = _ragdollControllers[_currentIdx];

		controller.SetActive(true);
		controller.GetComponent<Animator>().enabled = false;
		controller.transform.parent.GetComponent<Animator>().enabled = false;

		controller.AddForce(_force, -controller.transform.forward);

		_currentIdx++;
	}
#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Transform t = _ragdollControllers[0].transform;
		Gizmos.DrawRay(t.position, -t.forward);
		Gizmos.color = Color.white;
	}
#endif
}
