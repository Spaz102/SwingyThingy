using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon: MonoBehaviour {
	public GameObject body;
	public Vector3 momentum;
	private float reach;
	private float reachSquared;

	public float mass = 10f;
	public float bodyMass = 5f;
	//public float dragAmount;
	//private float friction = 0.95f;
	//private float drag = 0.01f;

	private void Start() {
		reach = (((RectTransform)this.transform).rect.height + ((RectTransform)body.transform).rect.height) / 2;
		//reachSquared = Mathf.Pow(reach, 2);
	}

	void Update() {
		Vector3 oldPosition = this.transform.position;
		momentum.y -= 0.2f;
		this.transform.position += momentum;
		
		Vector3 toTarget = (body.transform.position - this.transform.position).normalized;
		float distSquared = Mathf.Pow(body.transform.position.x - this.transform.position.x, 2) + Mathf.Pow(body.transform.position.y - this.transform.position.y, 2);
		float dist = Mathf.Sqrt(distSquared) - reach;
		
		float forceNeeded = (dist * (bodyMass * mass)) / (bodyMass + mass);
		
		float thisImpulse = forceNeeded / this.mass;
		float bodyImpulse = forceNeeded / bodyMass;

		this.transform.position += toTarget * thisImpulse;
		body.transform.position -= toTarget * bodyImpulse;

		//toTarget *= friction;
		//dragAmount = Mathf.Min(1f, drag / toTarget.magnitude);
		//toTarget *= (1f - dragAmount);
		this.transform.rotation = Quaternion.Euler(0, 0, 90f + Mathf.Atan2(body.transform.position.y - this.transform.position.y, body.transform.position.x - this.transform.position.x) * Mathf.Rad2Deg);
		momentum = this.transform.position - oldPosition;
	}
}
