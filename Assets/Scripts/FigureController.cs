using UnityEngine;
using System.Collections;

public class FigureController : MonoBehaviour 
{
	public delegate void modelChanging();
	public static event modelChanging OnChanged; //fixing the moment of changing the model to launch the function cubesVisibilit()

	CsGlobals gl;
	//CubeInfo [,]cubesArray;
	Model model;
	ParticleSystem clearEffect;

	//Decreasing effect
	GameObject effectBlowGO;
	effectClearLineSpawner effectBlow;
	ArrayList decreasedLines; //Array that store number of decreased lines

	int collisionCounter=0;

	//Timer settings
	float timeMove;
	float timeMoveMax=0.8f;
	float timeMoveMin=0.3f;

	//Control variables
	bool keyPressed;
	KeyCode currentKey;

	//public Shader defaultShader;
	//public Shader illuminShader;

	public Material illuminColoredMaterial;
	public Material defaultColoredMaterial;
	public Material illuminContourMaterial;
	public Material defaultContourMaterial;

	void Start () 
	{
		Time.timeScale = 1;

		gl=GameObject.FindObjectOfType(typeof(CsGlobals)) as CsGlobals;
		gl.isPlayingGame = false;
		gl.menu = true;
		//cubesArray = new CubeInfo[gl.model.cells.GetLength(0), gl.model.cells.GetLength(1)]; //array of cubes, that changes by cubesVisibility function
		gl.figureColor =new Color (Random.Range(0.35f, 0.7f), Random.Range(0.35f, 0.7f), Random.Range(0.35f, 0.7f));

		//gl.cubeInfo = cubePrefab.gameObject.GetComponent<CubeInfo>();  //getting info about cube (color, size...)
		OnChanged += cubesVisibility;	//adding new subscriber to event of playfield changing

		decreasedLines = new ArrayList ();
		effectBlowGO = GameObject.Find ("clearLineEffect");
		effectBlow = effectBlowGO.gameObject.GetComponent<effectClearLineSpawner> ();

		timeMove = timeMoveMax;

		gl.Score = 0;
	}

	// Update is called once per frame
	void Update () 
	{
		if (gl.isPlayingGame)
		{
			Time.timeScale = 1;		
			if (gl.menu)
			{
				InvokeRepeating("moveDown", 0, timeMove);  //timer, that run function for figure falling
				gl.menu = false;
			}

			if (Input.GetKeyDown(KeyCode.A) && !gl.currentFigure.isCollisionLeft(gl.model))
			{
				if (gl.currentFigure.MoveLeft(gl.model))
					OnChanged();
			}
			else
				if(Input.GetKeyDown(KeyCode.D) && !gl.currentFigure.isCollisionRight(gl.model))
			{
				if (gl.currentFigure.MoveRight(gl.model))
					OnChanged();
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
				if(timeMove == timeMoveMax)
				{
					CancelInvoke();
					timeMove=timeMoveMin;
					InvokeRepeating("moveDown", 0, timeMove); //changing time of timer to smaller (faster moving of figure)
				}
			}
			
			if (Input.GetKeyDown(KeyCode.Q))
			{
				if (gl.currentFigure.rotateLeft(gl.model))
					OnChanged ();
			}
			else
			{
				if (Input.GetKeyDown (KeyCode.E))
					if (gl.currentFigure.rotateRight(gl.model))
						OnChanged();
			}
		}


	}

	public void moveDown()
	{
		if (gl.currentFigure!=null && gl.model!=null && gl.cubesArray[gl.cubesArray.GetLength(0)-1, gl.cubesArray.GetLength(1)-1]!=null)
		{
			if (gl.currentFigure.isCollisionBottom(gl.model))
				collisionCounter++;
			else
			{
				if (gl.currentFigure.MoveDown(gl.model))
					OnChanged();
			}

			if (collisionCounter > 1) 
			{
				CancelInvoke();
				timeMove=timeMoveMax;
				InvokeRepeating("moveDown", 0, timeMove);  //restore the default timer
				gl.model.Add(gl.currentFigure);
				if (gl.model.fullLinesClear (gl.cubesArray, ref decreasedLines) && gl.isPlayingGame)
				{
					gl.Score += 200;
					foreach (int line in decreasedLines)
					{
						effectBlow.showEffect(gl.model, line, gl.cubeInfo);
					}
					decreasedLines = new ArrayList();
				}
				if (!isGameOver(gl.model) && !gl.nextFigure.isOverlaying(gl.model))
				{
					gl.currentFigure = gl.nextFigure;
					gl.nextFigure = new Figure (gl.model);
					gl.figureColor =new Color (Random.Range(0.35f, 0.7f), Random.Range(0.35f, 0.7f), Random.Range(0.35f, 0.7f));
					model = gl.model;
					gl.Score += 50;
				}
				collisionCounter = 0;
			} 




			if (isGameOver(gl.model))
				Time.timeScale = 0;
		}
	}

	public void cubesVisibility()
	{
		for (int  y=0; y < gl.model.cells.GetLength (0); y++)
		{
			for (int x=0; x < gl.model.cells.GetLength(1); x++)
			{
				if (gl.cubesArray[y,x]!=null)
				{
					if(gl.model.cells[y,x] || gl.currentFigure.isFill(x,y))
					{
						gl.cubesArray[y,x].gameObject.SetActive(true);
						//gl.cubesArray[y,x].cube.SetActive(true);	
						//gl.cubesArray[y,x].cubeColored.SetActive(true);
						if(gl.currentFigure.isFill(x,y))
						{
							gl.cubesArray[y,x].cube.renderer.material = illuminContourMaterial;
							gl.cubesArray[y,x].cubeColored.renderer.material = defaultColoredMaterial;
							gl.cubesArray[y,x].cubeColored.renderer.material.color = gl.figureColor;
							//cubesArray[y,x].cubeColored.renderer.material=defaultMaterial;
							/*
							cubesArray[y,x].cube.renderer.material.shader=illuminShader;
							cubesArray[y,x].cubeColored.renderer.material.shader=defaultShader;
							*/
						}
						if (gl.model.cells[y,x])
						{
							//cubesArray[y,x].cubeColored.renderer.material=illuminMaterial;
							gl.cubesArray[y,x].cube.renderer.material = defaultContourMaterial;
							Color tempColor = gl.cubesArray[y,x].cubeColored.renderer.material.color;
							gl.cubesArray[y,x].cubeColored.renderer.material = illuminColoredMaterial;
							gl.cubesArray[y,x].cubeColored.renderer.material.color = tempColor;
							/*
							cubesArray[y,x].cube.renderer.material.shader=defaultShader;
							cubesArray[y,x].cubeColored.renderer.material.shader=illuminShader;
							*/
						}
					}
					else
					{
						gl.cubesArray[y,x].gameObject.SetActive(false);
						//gl.cubesArray[y,x].cube.SetActive(false);
						//gl.cubesArray[y,x].cubeColored.SetActive(false);
						//cubesArray[y,x].cubeColored.renderer.material.color=Color.white;
						//cubesArray[y,x].cubeColored.renderer.material=defaultMaterial;
						//cubesArray[y,x].cubeColored.renderer.material.shader=defaultShader;
					}
				}

			}
		}
	}

	public bool isGameOver(Model model)
	{
		for (int x=0; x < model.cells.GetLength (1); x++)
			if (model.cells[0, x])
			{
				gl.isPlayingGame=false;
				return true;
			}

		return false;
	}





}
