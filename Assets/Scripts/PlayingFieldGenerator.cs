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

	// Use this for initialization
	void Start () 
	{
		gl=GameObject.FindObjectOfType(typeof(CsGlobals)) as CsGlobals;
		playingField = GameObject.Find ("GamingField");
		background = GameObject.Find ("FieldBack");
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
		playingFieldGenerate ();
	}

	void playingFieldGenerate()
	{
		if (gl.model != null && !done && gl.isSizeSet)
		{
			gl.cubeInfo = cubePrefab.gameObject.GetComponent<CubeInfo>();  //getting info about cube (color, size...)
			done=!done;
			Vector3 newSize = new Vector3(1, 1, 1);
			if(gl.model.cells.GetLength (0) > gl.model.cells.GetLength (1))
			{
				gl.cubeInfo.cubeSize=heightPF/gl.model.cells.GetLength (0);
				widthPF=gl.cubeInfo.cubeSize*gl.model.cells.GetLength (1);
				newSize = new Vector3(widthPF/playingField.renderer.bounds.size.x, 1, 1);
			}
			else
				if (gl.model.cells.GetLength (0) < gl.model.cells.GetLength (1))
				{
					gl.cubeInfo.cubeSize=widthPF/gl.model.cells.GetLength (1);
					widthPF=gl.cubeInfo.cubeSize*gl.model.cells.GetLength (0);
					newSize = new Vector3(1, heightPF/playingField.renderer.bounds.size.y, 1);
				}
				else
				{

					gl.cubeInfo.cubeSize=widthPF/gl.model.cells.GetLength (1);
				}
			playingField.transform.localScale = Vector3.Scale (playingField.transform.localScale, newSize);
			placingCubes(gl.model, gl.cubeInfo.cubeSize);
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
			cubePrefab.transform.localScale = Vector3.Scale (cubePrefab.transform.localScale, new Vector3());
			for(int y=0; y < model.cells.GetLength(0); y++)
			{
				for(int x=0; x < model.cells.GetLength (1); x++)
				{
					cubeGO =(GameObject) Instantiate (cubePrefab, new Vector3(this.transform.position.x + x_, this.transform.position.y + y_, this.transform.position.z + z_), Quaternion.identity);
					cubeGO.SetActive(false);
					gl.cubesArray[y,x]=cubeGO.GetComponent<CubeInfo>();
					x_ += cubeSize;
				}
				x_ = cubeSize/2;
				y_ += -cubeSize;
				
			}
		}
	}
}
