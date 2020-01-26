using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body: MonoBehaviour {
	public GameObject controller;
	public float force = 10f;
	public float mass = 10f;

	void Update() {
		Vector3 toTarget = controller.transform.position - this.transform.position;
		toTarget = toTarget.normalized * Mathf.Min(toTarget.magnitude, force);
		
		this.transform.position = this.transform.position + toTarget;
	}
}
