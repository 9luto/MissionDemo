using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

	static public GameObject TrackedObj;

	[Header("set in inspector")]
	public float easing = 0.05f;

	public Vector3 minXY = Vector3.zero;

	[Header("Set Dynamically")]
	public float camZ;



	void awake()
	{
		camZ = this.transform.position.z;
	}

	void Start () {
		
	}
	
	void FixedUpdate () 
	{
		if (TrackedObj == null)
			return;

		//get pos of the object your camera will track
		Vector3 destination = TrackedObj.transform.position;
		destination.x = Mathf.Max (minXY.x, destination.x);
		destination.x = Mathf.Max (minXY.x, destination.y);


		destination = Vector3.Lerp (transform.position, destination, easing);

		destination.z = camZ;

		transform.position = destination;

		Camera.main.orthographicSize = destination.y + 10;
	}
}
