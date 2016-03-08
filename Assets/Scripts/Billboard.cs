using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

	public GameObject player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.LookAt (player.transform.position);
	}
}
