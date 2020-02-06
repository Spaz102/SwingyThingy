using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor: MonoBehaviour {
	//TODO: Rework as an extension of a Controller class (position and any extra instructions for body to attempt)
	void Update() {
		this.transform.position = Input.mousePosition;
	}
}
