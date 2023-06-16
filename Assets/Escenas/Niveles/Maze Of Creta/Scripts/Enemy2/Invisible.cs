using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : MonoBehaviour {

	Renderer ren;
	void OnEnable () {
		ren = GetComponent<Renderer>();
		ren.enabled = false;

	}

	void OnDisable () {
		ren.enabled = true;
	}
}
