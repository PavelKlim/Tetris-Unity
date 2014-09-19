using UnityEngine;
using System.Collections;

public class effectClearLineSpawner : MonoBehaviour {


	public Blow []effectBlow; 

	GameObject effectPSPrefab;
	string effectName = "BlowEffect";
	void Start () 
	{
		effectPSPrefab = (GameObject)Resources.Load (effectName);
	}


	
	public void showEffect(Model model, int _y, CubeInfo cubeInfo)
	{		
		float _x = model.cells.GetLength (1)/2;
		float position_x = -_x * cubeInfo.cubeSize + 1.5f + cubeInfo.cubeSize/2;
		float _z = cubeInfo.cubeSize;
		GameObject effectGO;
		effectBlow = new Blow[model.cells.GetLength(1)];
		float y = (model.cells.GetLength(0)-1-_y)*cubeInfo.cubeSize + cubeInfo.cubeSize/2;
		if (model != null)
		{
			for (int i=0; i<model.cells.GetLength(1); i++)
			{
				effectGO =(GameObject) Instantiate(effectPSPrefab,new Vector3(position_x, y, -_z), Quaternion.identity);
				effectBlow[i]=effectGO.GetComponent<Blow>();
				position_x += cubeInfo.cubeSize;
			}
		}

	}
}
