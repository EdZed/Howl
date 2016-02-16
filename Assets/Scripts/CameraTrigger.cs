using UnityEngine;
using System.Collections;

public class CameraTrigger : MonoBehaviour {
	
	public float cameraResize;
	public Transform cameraAnchor;
	public delegate void CameraOverrideEnter (float resize, Transform target = null);
	public static event CameraOverrideEnter OnCameraOverrideEnter;
	public delegate void CameraOverrideExit ();
	public static event CameraOverrideExit OnCameraOverrideExit;
	
	void OnTriggerEnter2D (Collider2D hit) {
		if (hit.CompareTag ("Player")) {
			if (OnCameraOverrideEnter != null) OnCameraOverrideEnter (cameraResize, cameraAnchor);
		}
	}
	void OnTriggerExit2D (Collider2D hit) {
		if (hit.CompareTag ("Player")) {
			if (OnCameraOverrideEnter != null) OnCameraOverrideExit ();
		}
	}
}
