using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControls : MonoBehaviour {
	
	
	// Movement and rotation speeds
	public float movementSpeed = 5.0f;
	public float rotationSpeed_Body = 100f;
	public float rotationSpeed_Head = 100f;
	public float resetSpeed = 0.01f;
	
	// Mouse Input, used for moving the camera to the point cursor is at
	public float pos_x;
	public float pos_y;
	public float pos_z;
	
	// Current camera rotation
	public float rot_x;
	public float rot_y;
	
	public float shoot;
	
	// Vectors for resetting rotations in case camera rotation exceeds limits
	public Vector3 resetZ;
	
	// Vector to rotate camera
	public Vector3 rotation_vector;
	
	public Camera		head;

	public Rigidbody body;

	public float v_x;
	public float v_y;
	public float v_z;

	public float v_max;
	public float v_min;
	public float v_snap;

	public float dampen;

	public Material hightlight;
	public Material prevMat;
	public RaycastHit hit;

	public GameObject prev;

	public Vector3 pos;
	public Quaternion rot;
	public bool lerp;
	public bool locked;


	[Range(1f, 5f)]
	public float speed = 1f;

	void Start () {

	}
	void Update()
	{
		Physics.Raycast (transform.position, transform.forward, out hit, 10f);
		if (hit.collider != null) {
			if (hit.collider.gameObject.tag.Equals ("Interactable")) {
				if (prev == null) {
					prev = hit.collider.gameObject;
					prevMat = prev.GetComponent<MeshRenderer> ().material;
					hit.collider.gameObject.GetComponent<MeshRenderer> ().material = hightlight;
				} else {
					if (prev != hit.collider.gameObject) {
						prev.GetComponent<MeshRenderer> ().material = prevMat;
						prev = hit.collider.gameObject;
						hit.collider.gameObject.GetComponent<MeshRenderer> ().material = hightlight;
					}
				}
			}
			else
			{
				prev.GetComponent<MeshRenderer> ().material = prevMat;
				prev = null;
				prevMat = null;
			}
		} 
		else {
			if(prev != null)
			{
				prev.GetComponent<MeshRenderer> ().material = prevMat;
				prev = null;
				prevMat = null;
			}
		}


	
	}
	void FixedUpdate(){
		MouseInputs ();
		KeyboardInputs ();

		if (pos != Vector3.zero) {
			transform.LookAt(prev.transform);
			if(lerp)
			{
				transform.position = Vector3.Lerp(transform.position, prev.transform.position, Time.deltaTime);
			}
		}
	}
	
	// Camera rotation
	void MouseInputs(){
		// Get mouse input
		pos_x = -1 * Input.GetAxis ("Mouse Y");
		pos_y = 	 Input.GetAxis ("Mouse X");
		pos_z = 0f;
		
		shoot = Input.GetAxis ("Fire1");
		// Round mouse input
		pos_x =   1 * Mathf.Round (pos_x * 100f) / 100f;
		pos_y =   1 * Mathf.Round (pos_y * 100f) / 100f;
		
		// Create rotation vector
		rotation_vector = new Vector3 (pos_x, pos_y, pos_z);
		
		// Get local angles to limit rotation
		rot_x = head.transform.localEulerAngles.x;
		rot_y = head.transform.localEulerAngles.y;

		head.transform.Rotate (rotation_vector);
		
		// Reset z rotation
		pos_z = head.transform.eulerAngles.z;
		resetZ = new Vector3 (0, 0, -pos_z);
		head.transform.Rotate (resetZ);
	}

	void KeyboardInputs()
	{
		if (Input.GetKey (KeyCode.Space)) {
			if (v_y < v_max) {
				//v_y += Time.deltaTime;
			}
			if(locked)
			{
				locked = false;
			}
			body.AddForce (transform.up);
		}

		if (!locked) {
			if (Input.GetKey (KeyCode.W)) {
				v_z = body.velocity.z;
				v_x = body.velocity.x;
				v_y = body.velocity.y;
				if (((v_x < v_max && v_x >= 0) || (v_x > -v_max && v_x <= 0)) && ((v_z >= 0 && v_z < v_max) || (v_z <= 0 && v_z > -v_max)) && ((v_y < v_max && v_y >= 0) || (v_y > -v_max && v_y <= 0))) {
					body.AddForce (transform.forward * speed);
				}

				v_x = Mathf.Round (v_x);
				v_y = Mathf.Round (v_y);
				v_z = Mathf.Round (v_z);
			} 
			if (Input.GetKey (KeyCode.S)) {
				v_z = body.velocity.z;
				v_x = body.velocity.x;
				v_y = body.velocity.y;
				if (((v_x < v_max && v_x >= 0) || (v_x > -v_max && v_x <= 0)) && ((v_z >= 0 && v_z < v_max) || (v_z <= 0 && v_z > -v_max)) && ((v_y < v_max && v_y >= 0) || (v_y > -v_max && v_y <= 0))) {
					body.AddForce (-transform.forward * speed);
				}

				v_x = Mathf.Round (v_x);
				v_y = Mathf.Round (v_y);
				v_z = Mathf.Round (v_z);
			} 
			if (Input.GetKey (KeyCode.A)) {
				v_z = body.velocity.z;
				v_x = body.velocity.x;
				v_y = body.velocity.y;
				if (((v_x < v_max && v_x >= 0) || (v_x > -v_max && v_x <= 0)) && ((v_z >= 0 && v_z < v_max) || (v_z <= 0 && v_z > -v_max)) && ((v_y < v_max && v_y >= 0) || (v_y > -v_max && v_y <= 0))) {
					body.AddForce (-transform.right * speed);
				}

				v_x = Mathf.Round (v_x);
				v_y = Mathf.Round (v_y);
				v_z = Mathf.Round (v_z);
			} 
			if (Input.GetKey (KeyCode.D)) {
				v_z = body.velocity.z;
				v_x = body.velocity.x;
				v_y = body.velocity.y;
				if (((v_x < v_max && v_x >= 0) || (v_x > -v_max && v_x <= 0)) && ((v_z >= 0 && v_z < v_max) || (v_z <= 0 && v_z > -v_max)) && ((v_y < v_max && v_y >= 0) || (v_y > -v_max && v_y <= 0))) {
					body.AddForce (transform.right * speed);
				}

				v_x = Mathf.Round (v_x);
				v_y = Mathf.Round (v_y);
				v_z = Mathf.Round (v_z);
			}
		}
	
		if (Input.GetKey (KeyCode.F) && prev != null)
		{
			pos = prev.transform.position;
			lerp = true;
			locked = true;
			//rot = Quaternion.LookRotation(pos);
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag.Equals ("Interactable")) {
			lerp = false;
			pos = Vector3.zero;
		}
	}
}
