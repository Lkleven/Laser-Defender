using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuColor : MonoBehaviour {
	private Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		RandomMenuColor ();
	}

	// X2 prints the string as two uppercase hexadecimal characters
	string ColorToHex(Color32 color){
		string hex = color.r.ToString ("X2") + color.g.ToString ("X2") + color.b.ToString ("X2");
		return hex;
	}

	Color32 RandomColor(){
		return new Color32 ((byte)Random.Range (0, 255), (byte)Random.Range (0, 255), (byte)Random.Range (0, 255), 255);
	}

	void RandomMenuColor(){	
		string ColorfulText ="";
		for (int i = 0; i < text.text.Length; i++) {
			ColorfulText += "<color=#" + ColorToHex (RandomColor ()) + ">"+ text.text[i] +"</color>";
		}
		text.text = ColorfulText;
	}
}