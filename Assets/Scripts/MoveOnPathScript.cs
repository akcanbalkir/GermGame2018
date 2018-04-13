using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPathScript : MonoBehaviour {

	public EditorPathScript PathToFollow;
	public GameControllerScript Controller;
	public int CurrentWayPointID = 0; // Waypoint we last visited

	private float jumpHeight = 1f;
	private float smoothing = 4f;
	private float jumpSmoothing = 3f;
	private int finalDestination;
	private Vector3 currentFlatPosition;
	private Vector3 finalPosition;
	private Dictionary<int,int> JumpPoints = new Dictionary<int, int>();
	private Dictionary<int,int> SlidePoints = new Dictionary<int, int>();
	private Dictionary<int,int> ImageMap = new Dictionary<int, int>();
	private float reachDistance = .05f; // distance between capsuel and point on curve (smaller it is uglier the movement)
	private AudioSource move;
	private AudioSource jump;
	private AudioSource slide;

	//COROUTINE PROTECTORS SO MULTIPLE MOVEMENT COROUTINES CANNOT BE CALLED AT THE SAME TIME
	private bool MovementIsOccuring = false;

	// Use this for initialization
	void Start () 
	{
		var aSources = GetComponents<AudioSource> ();
		move = aSources [0];
		jump = aSources [1];
		slide = aSources [2];

		JumpPoints.Add (5, 9); // Current space to jump space
		JumpPoints.Add (10, 19);
		JumpPoints.Add (15, 30);
		JumpPoints.Add (21, 24);
		JumpPoints.Add (32, 41);
		JumpPoints.Add (42, 48);

		SlidePoints.Add (13, 1); // Current space to slide space
		SlidePoints.Add (27, 17);
		SlidePoints.Add (34, 25);
		SlidePoints.Add (36, 7);
		SlidePoints.Add (45, 31);
		SlidePoints.Add (50, 37);

		ImageMap.Add (13, 0); // Current space to image
		ImageMap.Add (27, 1);
		ImageMap.Add (34, 2);
		ImageMap.Add (36, 3);
		ImageMap.Add (45, 4);
		ImageMap.Add (50, 5);
		ImageMap.Add (5, 6);
		ImageMap.Add (10, 7);
		ImageMap.Add (15, 8);
		ImageMap.Add (21, 9);
		ImageMap.Add (32, 10);
		ImageMap.Add (42, 11);

		//PathToFollow = GameObject.Find (pathName).GetComponent<EditorPathScript> ();
		if (CurrentWayPointID >= PathToFollow.path_objs.Count) 
		{
			CurrentWayPointID = 0;
		}
		transform.position = PathToFollow.path_objs[CurrentWayPointID].position;
		//last_position = transform.position;
		//pauseTimer = pauseTime;
	}

	public void Reset() {
		CurrentWayPointID = 0;
		transform.position = PathToFollow.path_objs[CurrentWayPointID].position;
		StopAllCoroutines();
		MovementIsOccuring = false;
	}

	public void MoveNSteps(int n) 
	{
		if (!MovementIsOccuring) {
			StartCoroutine (MoveNStepsCoroutine (n));
		} else {
			Debug.Log ("Movement COROUTINE IS ALREADY IN PROGRESS ERROR!!!!");
		}
	}

	public void Jump(int destination) 
	{
		if (!MovementIsOccuring) {
			StartCoroutine (JumpCoroutine (destination));
		} else {
			Debug.Log ("Movement COROUTINE IS ALREADY IN PROGRESS ERROR!!!!");
		}
	}

	public void Slide(int destination) 
	{
		if (!MovementIsOccuring) {
			StartCoroutine (SlideCoroutine (destination));
		} else {
			Debug.Log ("Movement COROUTINE IS ALREADY IN PROGRESS ERROR!!!!");
		}
	}

	public void JumpSlide() // jump or slide depending on current location
	{
		int moveLocation;
		if (JumpPoints.TryGetValue(CurrentWayPointID, out moveLocation))
		{
			Jump (moveLocation);
		}
		else if (SlidePoints.TryGetValue(CurrentWayPointID, out moveLocation))
		{
			Slide (moveLocation);
		}
	}

	public bool JumpSlideIsPossible()
	{
		int moveLocation;
		if (JumpPoints.TryGetValue (CurrentWayPointID, out moveLocation))
		{
			return true;
		} 
		else if (SlidePoints.TryGetValue (CurrentWayPointID, out moveLocation))
		{
			return true;
		} 
		else
		{
			return false;
		}
	}

	IEnumerator MoveNStepsCoroutine(int n) 
	{
		MovementIsOccuring = true;
		if ((CurrentWayPointID + n) >= PathToFollow.path_objs.Count) 
		{
			finalDestination = CurrentWayPointID;
		} 
		else 
		{
			finalDestination = CurrentWayPointID + n;
		}
		if ((CurrentWayPointID) > PathToFollow.path_objs.Count) 
		{
			Debug.Log ("YOU ARE OFF THE PATH ERROR!!! CWPID: " + CurrentWayPointID);
			MovementIsOccuring = false;
		} 
		else 
		{
			yield return new WaitForSeconds (1);
			while (CurrentWayPointID < finalDestination) //waypoint is target. do not overstep final target 
			{
				move.Play ();
				while (Vector3.Distance (transform.position, PathToFollow.path_objs [CurrentWayPointID + 1].position) > reachDistance) 
				{
					transform.position = Vector3.Lerp (transform.position, PathToFollow.path_objs [CurrentWayPointID + 1].position, smoothing * Time.deltaTime);

					yield return null;
				}
				CurrentWayPointID++;
			}
		}
		yield return new WaitForSeconds (1);
		MovementIsOccuring = false;
		if (JumpSlideIsPossible ())
		{
			int imageNumber;
			if (ImageMap.TryGetValue (CurrentWayPointID, out imageNumber))
			{
				Controller.LadderSlideView (imageNumber);
			}
		} 
		else if ((CurrentWayPointID + 1) == PathToFollow.path_objs.Count)
		{
			Controller.Victory ();
		}
		else
		{
			Controller.NextTurn ();
		}
	}

	IEnumerator JumpCoroutine(int destination)
	{
		MovementIsOccuring = true;
		if (destination >= PathToFollow.path_objs.Count || destination < 0)
		{
			Debug.Log ("JUMP DESTINATION: " + destination + " IS OUT OF BOUNDS ERROR!!");
		} 
		else
		{
			jump.Play ();
			finalPosition = PathToFollow.path_objs [destination].position;
			currentFlatPosition = transform.position;
			currentFlatPosition.y = finalPosition.y;
			float totalDist = Vector3.Distance (currentFlatPosition, finalPosition);
			while (Vector3.Distance (transform.position, finalPosition) > reachDistance)
			{
				currentFlatPosition = Vector3.Lerp (currentFlatPosition, finalPosition, jumpSmoothing * Time.deltaTime);
				float currentDist = Vector3.Distance (currentFlatPosition, finalPosition);
				transform.position = currentFlatPosition + new Vector3 (0f, jumpHeight * totalDist * Mathf.Sin (Mathf.PI * (currentDist / totalDist)), 0f);
				yield return null;
			}
			CurrentWayPointID = destination;
		}
		yield return new WaitForSeconds (1);
		Controller.NextTurn();
		MovementIsOccuring = false;
	}

	IEnumerator SlideCoroutine(int destination)
	{
		MovementIsOccuring = true;
		if (destination >= PathToFollow.path_objs.Count || destination < 0)
		{
			Debug.Log ("Slide DESTINATION: " + destination + " IS OUT OF BOUNDS ERROR!!");
		} 
		else
		{
			slide.Play ();
			finalPosition = PathToFollow.path_objs [destination].position;
			while (Vector3.Distance (transform.position, finalPosition) > reachDistance) 
			{
				transform.position = Vector3.Lerp (transform.position, finalPosition, smoothing * Time.deltaTime);
				yield return null;
			}
			CurrentWayPointID = destination;
		}
		yield return new WaitForSeconds (1);
		Controller.NextTurn();
		MovementIsOccuring = false;
	}

	// Update is called once per frame
	/*
	void Update () 
	{
		if (pause) {
			pauseTimer -= Time.deltaTime;
			if (pauseTimer <= 0) {
				pause = false;
				pauseTimer = pauseTime;
			}
		} else {
			float distance = Vector3.Distance (PathToFollow.path_objs [CurrentWayPointID].position, transform.position);
			float mappedDistance = Vector3.Distance (new Vector3 (PathToFollow.path_objs [CurrentWayPointID].position.x, 0f, PathToFollow.path_objs [CurrentWayPointID].position.z), new Vector3 (transform.position.x, 0f, transform.position.z));
			float totalDistance = Vector3.Distance (last_position, PathToFollow.path_objs [CurrentWayPointID].position);
			var rotation = Quaternion.LookRotation (PathToFollow.path_objs [CurrentWayPointID].position - transform.position);
			if (jumpEnabled) {
				Debug.Log (mappedDistance / totalDistance);
				float height = Mathf.Sin (Mathf.PI * (mappedDistance / totalDistance)) * jumpHeight * totalDistance;
				transform.position = Vector3.MoveTowards (transform.position, PathToFollow.path_objs [CurrentWayPointID].position, Time.deltaTime * speed);
				transform.position = new Vector3 (transform.position.x, last_position.y + height, transform.position.z);
			} else {
				transform.position = Vector3.MoveTowards (transform.position, PathToFollow.path_objs [CurrentWayPointID].position, Time.deltaTime * speed);
			}
			transform.position = Vector3.MoveTowards (transform.position, PathToFollow.path_objs [CurrentWayPointID].position, Time.deltaTime * speed);

			if (rotationEnabled) {
				transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotationSpeed);
			}

			if (distance <= reachDistance) {
				CurrentWayPointID++;
				pause = true;
				last_position = transform.position;
			}

			if (CurrentWayPointID >= PathToFollow.path_objs.Count) {
				CurrentWayPointID = 0;
			}
		}
	}
	*/
}
