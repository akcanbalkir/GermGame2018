using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.IO;

public class LanguageScript : MonoBehaviour {

	public TextAsset GameAsset;
	public List<string> languages = new List<string> ();
	public Dictionary<string, int> languageDirectory = new Dictionary<string, int> ();
	public List<Dictionary<string, string>> languageLibrary = new List<Dictionary<string, string>> ();

	private XmlDocument xmlDoc;
	private Dictionary<string, string> varDic;
	private int LanguageNumber = 0; // base language english

	// Use this for initialization
	void Start () {
		ImportLanguages();
	}

	public string GetString(string type)
	{
		string word;
		if (languageLibrary [LanguageNumber].TryGetValue (type, out word))
		{
			return word;
		} 
		else
		{
			return "NULL VALUE";
		}
	}

	public void SetLanguage(int i)
	{
		int ln;
		if (languageDirectory.TryGetValue (languages[i], out ln))
		{
			LanguageNumber = ln;
		} 
		else
		{
			Debug.Log ("ERROR LANGUAGE NOT FOUND");
		}
	}

	public void ImportLanguages()
	{
		xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
		xmlDoc.LoadXml(GameAsset.text); // load the file.
		XmlNodeList LanguageList = xmlDoc.GetElementsByTagName("language"); // array of the language nodes.
		for (int i = 0; i < LanguageList.Count; i++)
		{
			languages.Add (LanguageList.Item (i).Attributes ["name"].Value);
			languageDirectory.Add (LanguageList.Item(i).Attributes["name"].Value, i);
			XmlNodeList TextList = LanguageList.Item(i).ChildNodes;
			varDic = new Dictionary<string, string> ();
			foreach (XmlNode node in TextList)
			{
				varDic.Add (node.Attributes["name"].Value, node.InnerText);
			}
			languageLibrary.Add (varDic);
		}
	}
}
