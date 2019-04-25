using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour {
	[Header("Set in Inspector")]
	public GameObject prefabProjectile;
	public float      velocityMult = 8f;

	[Header("Set Dynamically")]
	public GameObject launchPoint;
	public Vector3    launchPos;
	public GameObject projectile;
	public bool       aiming;

	private Rigidbody projectileRigidbody;

	void Awake(){
		Transform launchPointTrans = transform.Find ("LaunchPoint");
		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive (false);
		launchPos = launchPointTrans.position;
	}

	void OnMouseEnter(){
		launchPoint.SetActive(true);
	}

	void OnMouseExit(){
		launchPoint.SetActive(false);
	}

	void OnMouseDown(){
		aiming = true;
		projectile = Instantiate (prefabProjectile) as GameObject;
		projectile.transform.position = launchPos;
		projectile.GetComponent<Rigidbody> ().isKinematic = true;
		projectileRigidbody = projectile.GetComponent<Rigidbody> ();
		projectileRigidbody.isKinematic = true;

	}

	// Use this for initialization
	void Start () {
		
	}
	
	void Update () {
		if (!aiming) return;

		// Get the current mouse position in 2D screen coordinates
		Vector3 mousePos2D = Input.mousePosition;
		mousePos2D.z = -Camera.main.transform.position.z;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint (mousePos2D);

		Vector3 mouseDelta = mousePos3D - launchPos;
		float maxMagnitude = this.GetComponent<SphereCollider> ().radius;
		if (mouseDelta.magnitude > maxMagnitude) {
			mouseDelta.Normalize ();
			mouseDelta *= maxMagnitude;
		}

		Vector3 projPos = launchPos + mouseDelta;
		projectile.transform.position = projPos;

		if (Input.GetMouseButtonUp (0)) {
			aiming = false;
			projectileRigidbody.isKinematic = false;
			projectileRigidbody.velocity = -mouseDelta * velocityMult;
			FollowCam.TrackedObj = projectile;
			projectile = null;
		}
	}
}
