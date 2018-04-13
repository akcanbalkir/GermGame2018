using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageMenuScript : MonoBehaviour {

	private LanguageScript ls;

	void Start()
	{
		ls = GameObject.FindWithTag ("language").GetComponent<LanguageScript> ();
	}
	
	// Update is called once per frame
	void OnGUI () {
		for (int i = 0; i < ls.languages.Count; i++)
		{
			if (GUI.Button (new Rect (200, 100 + (i * 40), 100, 30), ls.languages [i]))
			{
				ls.SetLanguage (i);
			}
		}
	}
}
