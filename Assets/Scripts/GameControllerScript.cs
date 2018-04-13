using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {

	public GameObject StartCamera;
	public GameObject GameCamera;
	public GameObject DieCamera;
	public GameObject Player1Camera = null;
	public GameObject Player2Camera = null;
	public GameObject Player3Camera = null;
	public GameObject Player4Camera = null;
	public MoveOnPathScript Player1Move;
	public MoveOnPathScript Player2Move;
	public MoveOnPathScript Player3Move;
	public MoveOnPathScript Player4Move;
	public GameObject Player3;
	public GameObject Player4;
	public GameObject Canvas = null;
	public Text PlayerText;
	public Text WinText;
	public Text RollText;

	private LanguageScript ls;
	private int playerNumber = 2;
	private MoveOnPathScript CurrentPlayerMove;
	private int turnNumber = 0;
	private GameObject[] cameras;
	private GameObject[] canvases;
	private List<GameObject> LSImages = new List<GameObject> ();
	private List<GameObject> views = new List<GameObject> ();
	private Transform[] theArray;
	private AudioSource winSound;
	private int PreviousView;

	// Use this for initialization
	void Start () 
	{
		ls = GameObject.FindWithTag ("language").GetComponent<LanguageScript> ();
		winSound = GetComponent<AudioSource> ();
		cameras = new GameObject[7] { StartCamera, GameCamera, Player1Camera, Player2Camera, Player3Camera, Player4Camera, DieCamera }; // BUILD CAMERA ARRAY
		theArray = Canvas.GetComponentsInChildren<RectTransform> (true);
		foreach (Transform view in theArray) // BUILD VIEWS ARRAY
		{
			if (view.gameObject.CompareTag ("View"))
			{
				views.Add (view.gameObject);
			} 
			else if (view.gameObject.CompareTag ("LSImage"))
			{
				LSImages.Add (view.gameObject);
			}
		}
		ActivateCamera (0);
		ActivateView (0);
	}

	public void Reset () 
	{
		Player1Move.Reset ();
		Player2Move.Reset ();
		Player3Move.Reset ();
		Player4Move.Reset ();
		turnNumber = 0;
		ActivateCamera (0);
		ActivateView (0);
	}

	public void QuitGame()
	{
		Application.Quit ();
	}

	public void Victory () 
	{
		WinText.text = ls.GetString ("player") + " " + (((turnNumber - 1) % playerNumber) + 1) + " " + ls.GetString ("wins");
		winSound.Play ();
		ActivateView (10);
	}

	public void SetPlayerNumber(int i) 
	{
		playerNumber = i;
	}

	public void ActivateCamera(int i)
	{
		for (int j = 0; j < cameras.Length; j++) 
		{
			if (j == i) 
			{
				cameras [j].SetActive (true);
			}
			else 
			{
				cameras [j].SetActive (false);
			}
		}
	}

	public void MoveAsideNonPlayers() 
	{
		if (playerNumber == 2) 
		{
			Player3.transform.position = Player3.transform.position - new Vector3 (4f, 0f, 0f);
			Player4.transform.position = Player4.transform.position - new Vector3 (4f, 0f, 0f);
		} 
		else if (playerNumber == 3) 
		{
			Player4.transform.position = Player4.transform.position - new Vector3 (4f, 0f, 0f);
		} 
	}

	public void ActivateView(int i)
	{
		for (int j = 0; j < views.Count; j++) 
		{
			if (j == i) {
				views [j].SetActive (true);
			} else {
				if (views [j].activeInHierarchy)
				{
					PreviousView = j;
				}
				views [j].SetActive (false);
			}
		}
	}

	public void GetPreviousView()
	{
		ActivateView (PreviousView);
	}

	public void ActivateImage(int i)
	{
		for (int j = 0; j < LSImages.Count; j++) 
		{
			if (j == i) {
				LSImages [j].SetActive (true);
			} else {
				LSImages [j].SetActive (false);
			}
		}
	}

	public void LadderSlideView(int imageNumber) // pictureNumber indicates picture and text to display 
	{
		ActivateImage (imageNumber);
		ActivateView (6);
	}

	public void ResumeLadderSlide() 
	{
		CurrentPlayerMove.JumpSlide ();
	}

	public void MovePlayer(int n) 
	{
		RollText.text = ls.GetString ("player") + " " + ((turnNumber % playerNumber) + 1) + " " + ls.GetString ("rolled") + " " + n;
		ActivateView (12);
		if (turnNumber % playerNumber == 0) {
			ActivateCamera (2);
			CurrentPlayerMove = Player1Move;
		} else if (turnNumber % playerNumber == 1) {
			ActivateCamera (3);
			CurrentPlayerMove = Player2Move;
		} else if (turnNumber % playerNumber == 2) {
			ActivateCamera (4);
			CurrentPlayerMove = Player3Move;
		} else {
			ActivateCamera (5);
			CurrentPlayerMove = Player4Move;
		}
		CurrentPlayerMove.MoveNSteps(n);
		turnNumber += 1;
	}

	public void NextTurn() 
	{
		PlayerText.text = ls.GetString ("player") + " " + ((turnNumber % playerNumber) + 1) + " " + ls.GetString ("turn");
		ActivateCamera(1);
		ActivateView (4);
	}


}
