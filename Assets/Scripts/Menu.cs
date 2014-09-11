using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{

	public Texture buttonStartTexture;
	public Texture buttonResetTexture;
	public GUISkin buttonGUISkin;
	CsGlobals gl;
	// Use this for initialization

	void Start()
	{
		gl=GameObject.FindObjectOfType(typeof(CsGlobals)) as CsGlobals;
	}


	void OnGUI()
	{
		GUI.skin = buttonGUISkin;

		if(GUI.Button(new Rect(Screen.width-150, 10, 140, 60),buttonStartTexture))
		{
			gl.isPlayingGame=true;
		}
		
		if(GUI.Button(new Rect(Screen.width-150, 80, 140, 60), buttonResetTexture))
			Application.LoadLevel("Game");
	}

}
