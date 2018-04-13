using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTextScript : MonoBehaviour {

	// Put this on any text object
	public string TextType;
	private Text obj;
	private LanguageScript ls;

	void Start()
	{
		ls = GameObject.FindWithTag ("language").GetComponent<LanguageScript> ();
		obj = GetComponent<Text> ();
	}

	void OnGUI()
	{
		obj.text = ls.GetString (TextType);
	}

}
