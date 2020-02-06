using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A "Body" is any self-propelled physics object that can hold weapons
/// For now it chases a 'controller' point in space. Later, it will pull actual controlls from a controller
/// 
/// TODO:
/// - 'Grip'
/// - Modes of movement (Towards cursor, ai patterns, complex ai, etc)
/// 
/// The physics class handles the mass, momentum, collision, etc
/// </summary>

public class Body: Physics {
	public GameObject controller; // The point that this body will move towards // TODO: Additional controls for clicking, etc
	public float force = 5f; // How hard this engine can move towards its target
	
	public List<GameObject> rawWeapons = new List<GameObject>();
	private List<Weapon> weapons = new List<Weapon>();
	private void Start() {
		foreach (GameObject weapon in rawWeapons) {
			AddWeapon(weapon);
		}
		rawWeapons.Clear();
		this.hasMomentum = false; // Unnecessary? (Would have to manually set in editor/template)
	}

	void Update() { // Apply weapon momentum, apply body impulse, re-attach weapons
		foreach (Weapon weapon in weapons) {
			weapon.UpdatePosition();
		}

		Vector3 toController = controller.transform.position - this.transform.position;
		toController = toController.normalized * Mathf.Min(toController.magnitude, force); // Use less than maximum force, if that would overshoot
		
		this.transform.position += toController;

		for (int x = 0; x < Mathf.Min(3, weapons.Count); x++) { // With more than one weapon, final positions can only be approximated
			foreach (Weapon weapon in weapons) {
				Vector3 toBody = this.gameObject.transform.position - weapon.gameObject.transform.position;
				Vector3 towardsBody = toBody.normalized;

				float dist = toBody.magnitude - weapon.reach;
				float forceNeeded = (dist * (mass * weapon.mass)) / (mass + weapon.mass);

				float bodyImpulse = forceNeeded / this.mass;
				this.transform.position -= towardsBody * bodyImpulse;

				float weaponImpulse = dist - bodyImpulse;
				weapon.gameObject.transform.position += towardsBody * weaponImpulse;
				weapon.gameObject.transform.rotation = Quaternion.Euler(0, 0, 90f + Mathf.Atan2(this.transform.position.y - weapon.gameObject.transform.position.y, this.transform.position.x - weapon.gameObject.transform.position.x) * Mathf.Rad2Deg);
			}
		}
		foreach (Weapon weapon in weapons) {
			weapon.UpdateMomentum();
		}
	}

	void AddWeapon(GameObject weapon) {
		Weapon weaponScript = weapon.GetComponent<Weapon>();
		weaponScript.reach += ((RectTransform)transform).rect.height / 2;
		weapons.Add(weaponScript);
	}

}
