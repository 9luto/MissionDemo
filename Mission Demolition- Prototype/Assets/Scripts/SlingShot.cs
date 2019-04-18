using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour {
	public GameObject PrefabProjectile;
	public GameObject LaunchPoint;
	public Vector3 LaunchPos;
	public GameObject Projectile;
	public bool AimingMode;

	void Awake()
	{
		Transform LaunchPointTrans = transform.Find ("LaunchPoint");
		LaunchPoint = LaunchPointTrans.gameObject;
		LaunchPoint.SetActive (false);
		LaunchPos = LaunchPointTrans.position;
	}

	void OnMouseEnter()
	{
		//print ("SlingShot:OnMouseEnter()");
		LaunchPoint.SetActive (true);
	}

	void OnMouseExit()
	{
		//print ("SlingShot:OnMouseExit()");
		LaunchPoint.SetActive (false);

	}

	void OnMouseDOwn(){
		AimingMode = true;
		Projectile = Instantiate (PrefabProjectile) as GameObject;
		Projectile.transform.position = LaunchPos;
		Projectile.GetComponent<Rigidbody> ().isKinematic = true;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
