using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	public PlayingFieldGenerator playFG;
	public Texture buttonStartTexture;
	public Texture buttonResetTexture;
	public GUISkin buttonGUISkin;
	CsGlobals gl;
	int width = 6;
	int height = 6;
	// Use this for initialization

	void Start()
	{
		gl = GameObject.FindObjectOfType(typeof(CsGlobals)) as CsGlobals;
	}


	void OnGUI()
	{
		GUI.skin = buttonGUISkin;

		GUI.Label (new Rect (15, 10, 100, 20), "Score " + gl.Score);

		if(GUI.Button(new Rect(Screen.width-150, 10, 140, 60), buttonStartTexture) && gl.isSizeSet)
		{
			gl.isPlayingGame = true;
		}
		
		if(GUI.Button(new Rect(Screen.width-150, 80, 140, 60), buttonResetTexture))
			Application.LoadLevel("Game");

		//Buttons fo chaging size of width
		GUI.Label (new Rect (22, 40, 100, 20), "Width");
		GUI.Label (new Rect (35, 60, 100, 20), width.ToString());

		if (GUI.Button (new Rect(10,60,20,20), "-") && width >= 7)
		{
			width--;
		}

		if (GUI.Button (new Rect(50,60,20,20), "+") && width <= 19)
		{
			width++;
		}

		//Buttons fo chaging size of hight
		GUI.Label (new Rect (22, 80, 100, 20), "Height");
		GUI.Label (new Rect (35, 100, 100, 20), height.ToString());
		
		if (GUI.Button (new Rect(10, 100, 20, 20), "-") && height >= 7)
		{
			height--;
		}
		
		if (GUI.Button (new Rect(50, 100, 20, 20), "+") && height <= 19)
		{
			height++;
		}

		//Set button
		if (GUI.Button (new Rect(10, 120, 60, 40), "Set"))
		{
			gl.model = new Model(width, height);
			gl.cubesArray = new CubeInfo[gl.model.cells.GetLength(0), gl.model.cells.GetLength(1)]; //array of cubes, that changes by cubesVisibility function
			gl.currentFigure = new Figure (gl.model);
			gl.nextFigure = new Figure (gl.model);	
			gl.isSizeSet=true;
			playFG.playingFieldGenerate();
		}

	}

}
