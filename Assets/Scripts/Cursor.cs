using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor: MonoBehaviour {
	void Start() {

	}

	void Update() {
		this.transform.position = Input.mousePosition;
	}
}
