using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetDiceValue : MonoBehaviour {

	public Text valueText;
	public Die_d6 dieScript;

	public void displayValue() {
		valueText.text = dieScript.value.ToString();
	}

}
