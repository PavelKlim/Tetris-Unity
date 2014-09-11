using UnityEngine;
using System.Collections;

public class NextFigurePreview : MonoBehaviour 
{

	CsGlobals gl;
	GameObject nextFigureGOPrefab;
	GameObject nextFigureGO;
	bool placed;

	void Start () 
	{
		gl=GameObject.FindObjectOfType(typeof(CsGlobals)) as CsGlobals;
		placed = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		figureRotaion(gl.nextFigure);
	}

	public void figureRotaion(Figure nextFigure)
	{
		if (gl.isPlayingGame)
		{
			if (nextFigureGOPrefab != null)
				if (gl.nextFigure.nameFigure.ToString() != nextFigureGOPrefab.name)
					placed = false;

			if (!placed)
			{
				nextFigureGOPrefab = (GameObject)Resources.Load(nextFigure.nameFigure.ToString());
				if(nextFigureGO!=null)
					Destroy(nextFigureGO);
				nextFigureGO = (GameObject) Instantiate(nextFigureGOPrefab, new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z), Quaternion.identity);
				nextFigureGO.transform.Rotate(0, 0, -30);
				//nextFigureGO.transform.localRotation=Quaternion.identity;
				placed = true;
			}
			//Space relativeTo = Space.Self;
			//nextFigureGO.transform.Rotate (new Vector3(0,Time.deltaTime*50,0),relativeTo);
			nextFigureGO.transform.RotateAround(new Vector3(nextFigureGO.transform.position.x, nextFigureGO.transform.position.y, nextFigureGO.transform.position.z), Vector3.up, Time.deltaTime*50);
		}
	}
}
