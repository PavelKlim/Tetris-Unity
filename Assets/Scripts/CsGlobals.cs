using UnityEngine;
using System.Collections;

public class CsGlobals : MonoBehaviour 
{
	public Model model = null;
	public int Score;
	public Figure currentFigure;
	public Figure nextFigure;
	public View view;

	public bool isPlayingGame;
	public bool menu;	
	public bool isSizeSet = false;
	public Color figureColor;
	public CubeInfo cubeInfo;
	public CubeInfo [,]cubesArray;
		
	void Start()
	{

	}
}
