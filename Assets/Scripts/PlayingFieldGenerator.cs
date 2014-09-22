using UnityEngine;
using System.Collections;

public class PlayingFieldGenerator : MonoBehaviour 
{
	CsGlobals gl;

	public GameObject playingField; // next PF
	public GameObject background;
	float widthPF;
	float heightPF;
	bool done=false;
	float borderBF; //border of white background Plane

	public GameObject cubePrefab;
	public GameObject cubePrefabColored;
	public GameObject figureSpawner;

	// Use this for initialization
	void Start () 
	{
		gl=GameObject.FindObjectOfType(typeof(CsGlobals)) as CsGlobals;
		playingField = GameObject.Find ("GamingField");
		background = GameObject.Find ("FieldBack");
		figureSpawner = GameObject.Find ("FigureSpawner");
		widthPF = playingField.renderer.bounds.size.x;
		heightPF = playingField.renderer.bounds.size.y;
		cubePrefab = (GameObject)Resources.Load("Cube", typeof(GameObject));  //loading prefab of cube
		cubePrefabColored = (GameObject)Resources.Load("CubeColored", typeof(GameObject)); //loading colored part of cube, its doing for acces to any colored part of cube for changing color		
		gl.currentFigure = new Figure (gl.model);
		gl.nextFigure = new Figure (gl.model);	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//playingFieldGenerate ();
	}

	public void playingFieldGenerate()
	{
		if (gl.model != null)
		{
			gl.cubeInfo = cubePrefab.gameObject.GetComponent<CubeInfo>();  //getting info about cube (color, size...)
			done=!done;
			Vector3 newSize = new Vector3(1, 1, 1);
			Vector3 newPosition = new Vector3(0, 0, 0);
			if(gl.model.cells.GetLength (0) > gl.model.cells.GetLength (1))
			{
				gl.cubeInfo.cubeSize=heightPF/gl.model.cells.GetLength (0);
				widthPF=gl.cubeInfo.cubeSize*gl.model.cells.GetLength (1);
				newSize = new Vector3(widthPF/playingField.renderer.bounds.size.x, 1, 1);
				newPosition = new Vector3(figureSpawner.transform.position.x+heightPF/2-widthPF/2, figureSpawner.transform.position.y, figureSpawner.transform.position.z);
			}
			else
				if (gl.model.cells.GetLength (0) < gl.model.cells.GetLength (1))
				{
					gl.cubeInfo.cubeSize=widthPF/gl.model.cells.GetLength (1);
					heightPF=gl.cubeInfo.cubeSize*gl.model.cells.GetLength (0);
					newSize = new Vector3(1, 1, heightPF/playingField.renderer.bounds.size.y);
					newPosition = new Vector3(figureSpawner.transform.position.x, figureSpawner.transform.position.y-widthPF/2+heightPF/2, figureSpawner.transform.position.z);
				}
				else
				{
					newPosition = new Vector3(figureSpawner.transform.position.x, figureSpawner.transform.position.y-widthPF/2+heightPF/2, figureSpawner.transform.position.z);
					gl.cubeInfo.cubeSize=widthPF/gl.model.cells.GetLength (1);
				}
			playingField.transform.localScale = Vector3.Scale (playingField.transform.localScale, newSize);
			figureSpawner.transform.position = newPosition;
			background.transform.localScale = Vector3.Scale (new Vector3(playingField.renderer.bounds.size.x/background.renderer.bounds.size.x+0.05f, playingField.renderer.bounds.size.y/background.renderer.bounds.size.y+0.05f, background.transform.localScale.z), background.transform.localScale);
			background.transform.position = playingField.transform.position;
			placingCubes(gl.model, gl.cubeInfo.cubeSize);
			done = true;
		}
	}

	public void placingCubes(Model model, float cubeSize)
	{
		if(model != null)
		{
			float x_ =  cubeSize/2;
			float y_ = -cubeSize/2;
			float z_ = -cubeSize/2;
			GameObject cubeGO;
			gl.cubeInfo = cubePrefab.gameObject.GetComponent<CubeInfo>();  //getting info about cube (color, size...)
			float temp = cubePrefab.renderer.bounds.size.x;
			for(int y=0; y < model.cells.GetLength(0); y++)
			{
				for(int x=0; x < model.cells.GetLength (1); x++)
				{
					cubeGO =(GameObject) Instantiate (cubePrefab, new Vector3(this.transform.position.x + x_, this.transform.position.y + y_, this.transform.position.z + z_), Quaternion.identity);
					//cubeGO.SetActive(false);
					gl.cubesArray[y,x]=cubeGO.GetComponent<CubeInfo>();
					gl.cubesArray[y,x].gameObject.SetActive(false);
					gl.cubesArray[y,x].cubeColored.transform.localScale = Vector3.Scale (gl.cubesArray[y,x].cubeColored.transform.localScale, new Vector3(gl.cubeInfo.cubeSize/gl.cubesArray[y,x].cubeColored.renderer.bounds.size.x * 0.9f, gl.cubeInfo.cubeSize/gl.cubesArray[y,x].cubeColored.renderer.bounds.size.y * 0.9f, gl.cubeInfo.cubeSize/gl.cubesArray[y,x].cubeColored.renderer.bounds.size.z * 0.9f));
					gl.cubesArray[y,x].cube.transform.localScale = Vector3.Scale (gl.cubesArray[y,x].cube.transform.localScale, new Vector3(gl.cubeInfo.cubeSize/gl.cubesArray[y,x].cube.renderer.bounds.size.x, gl.cubeInfo.cubeSize/gl.cubesArray[y,x].cube.renderer.bounds.size.y, gl.cubeInfo.cubeSize/gl.cubesArray[y,x].cube.renderer.bounds.size.z));
					x_ += cubeSize;
				}
				x_ = cubeSize/2;
				y_ += -cubeSize;
				
			}
		}
	}
}
