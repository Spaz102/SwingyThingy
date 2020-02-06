using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All physics objects
/// Warning: These do not automatically update
/// This is because their physics outcomes are not independent of one another, and must be calculated together
/// Thus all physics objects are handled by a controller of some sort
/// </summary>

public class Physics: MonoBehaviour {
	public float mass; // If mass is 0; treat as infinity

	public float friction = 0.98f;
	protected Vector3 gravity = new Vector3(0, -0.2f, 0);

	public bool hasMomentum; // Even if not, still tracks last position
	protected Vector3 lastPosition;
	protected Vector3 momentum;

	// TODO: Manual center of mass?

	void Start() {
		lastPosition = gameObject.transform.position;
	}

	public void UpdatePosition() {
		if (hasMomentum) {
			momentum += gravity;
			gameObject.transform.position += momentum;
		}
	}

	public void UpdateMomentum() {
		if (hasMomentum) {
			momentum = (gameObject.transform.position - lastPosition) * friction;
		}
		lastPosition = gameObject.transform.position;
	}
}
