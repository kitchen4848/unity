using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

	[SerializeField]
	private Camera cam;

	private Vector3 rotation = Vector3.zero;
	private Rigidbody rb;
	private Vector3 cameraRotation = Vector3.zero;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		PerformRotation ();
	}

	public void Rotate(Vector3 _rotation){
		rotation = _rotation;
	}

	public void RotateCamera(Vector3 _cameraRotation){
		cameraRotation = _cameraRotation;
	}

	void PerformRotation(){
		rb.MoveRotation (rb.rotation * Quaternion.Euler (rotation));
		if(cam != null){
			cam.transform.Rotate (-cameraRotation);
		}
	}
}
