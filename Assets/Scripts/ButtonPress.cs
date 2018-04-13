using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour {

	public Button P2Button;
	public Button P3Button;
	public Button P4Button;

	void Start() {
		Press (2);
	}

	public void Press(int button) {
		if (button == 2) {
			P2Button.image.color = Color.yellow;
			P3Button.image.color = Color.white;
			P4Button.image.color = Color.white;
		} else if (button == 3) {
			P2Button.image.color = Color.white;
			P3Button.image.color = Color.yellow;
			P4Button.image.color = Color.white;
		} else {
			P2Button.image.color = Color.white;
			P3Button.image.color = Color.white;
			P4Button.image.color = Color.yellow;
		}
	}
}
