using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WH_Doors : MonoBehaviour {

	Camera cam;
	Animator anim;

	public bool showCrosshair = true;
	public Texture2D crosshair_Default;
	public Texture2D crosshair_Yellow;

	bool inRange = false;

	[Range(0.5f, 10f)]
	public float interactionDistance = 3f;


	void Start(){
		cam = FindObjectOfType<Camera> ();
	}

	void Update(){

		Ray ray = cam.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0));
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, interactionDistance)) {
			if (hit.transform.GetComponent<Door_ID> ()) {
				//TODO Door animation
				Door_ID id = hit.transform.GetComponent<Door_ID> ();
				inRange = true;
				anim = id.GetComponent<Animator> ();

				if (Input.GetKeyDown (KeyCode.E)) {
					
					anim.SetBool ("open", !anim.GetBool ("open"));

				}
			} else if (hit.transform.GetComponent<Traffic>()) {
				//TODO Large gates opening
				inRange = true;

				if (Input.GetKeyDown (KeyCode.E)) {
					hit.transform.GetComponent<Traffic> ().ToggleGarageGates ();
				}
				//Other components go below
			}else {
				inRange = false;
			}
		} else {
			inRange = false;
		}
	}

	void OnGUI(){
		
		Rect rect = new Rect (Screen.width / 2 - crosshair_Default.width /2 , Screen.height / 2 - crosshair_Default.height /2, crosshair_Default.width, crosshair_Default.height);

		if (showCrosshair) {
			if (inRange) {
				GUI.DrawTexture (rect, crosshair_Yellow);
			} else {
				GUI.DrawTexture (rect, crosshair_Default);
			}
		}

	}
}
