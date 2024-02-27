using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traffic : MonoBehaviour {

	public Texture2D open;
	public Texture2D closed;

	public GameObject trafficLight;
	public GameObject orangeLight;

	public Animator garageDoor;

	public bool isOpen = false;

	Light pointLight;
	Light spotLight;

	Renderer interior_unit;
	Renderer exterior_unit;
	Renderer r_orangeLamp;

	void Start(){
		pointLight = trafficLight.GetComponentInChildren<Light> ();
		interior_unit = this.GetComponent<Renderer> ();
		exterior_unit = trafficLight.GetComponent<Renderer> ();
		r_orangeLamp = orangeLight.GetComponent<Renderer> ();
		spotLight = orangeLight.GetComponentInChildren<Light> ();
	}

	public void ToggleGarageGates() {
		isOpen = !isOpen;

		if (isOpen) {

			StartCoroutine (OrangeLight ());
			interior_unit.material.SetTexture ("_EmissionMap", open);
			exterior_unit.material.SetTexture ("_EmissionMap", open);
			Color c = Color.red * Mathf.LinearToGammaSpace (7f);
			DynamicGI.SetEmissive (interior_unit, c);
			DynamicGI.SetEmissive (exterior_unit, c);
			pointLight.color = Color.red;

		} else {
			StartCoroutine (OrangeLight ());
			exterior_unit.material.SetTexture ("_EmissionMap", closed);
			interior_unit.material.SetTexture ("_EmissionMap", closed);
			Color c = Color.red * Mathf.LinearToGammaSpace (7f);
			DynamicGI.SetEmissive (interior_unit, c);
			DynamicGI.SetEmissive (exterior_unit, c);
			pointLight.color = Color.green;

		}


		garageDoor.SetBool ("open", isOpen);


	
	}

	IEnumerator OrangeLight() {
		spotLight.enabled = true;
		Color c = Color.yellow * Mathf.LinearToGammaSpace(10f);
		r_orangeLamp.material.SetColor ("_EmissionColor", c);
		DynamicGI.SetEmissive (r_orangeLamp, c);
		yield return new WaitForSeconds (3f);
		c = c * Mathf.LinearToGammaSpace (0.0001f);
		r_orangeLamp.material.SetColor ("_EmissionColor", c);
		DynamicGI.SetEmissive (r_orangeLamp, c);
		spotLight.enabled = false;
	}
}
