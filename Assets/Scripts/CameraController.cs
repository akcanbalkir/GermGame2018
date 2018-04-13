using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// Guaranteed to run after all objects have been processed in update.
	// (we know absolutely that the player has moved for that frame).
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}
