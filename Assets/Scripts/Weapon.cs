using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon: Physics {
	public float reach;
	
	private void Start() {
		reach += ((RectTransform)transform).rect.height / 2;
	}
}
