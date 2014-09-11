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


	
	public void showEffect(Model model, int _y)
	{		
		int _x = -12;
		int _z = 3;
		GameObject effectGO;
		effectBlow = new Blow[model.cells.GetLength(1)];
		float y = (model.cells.GetLength(0)-1-_y)*3 + 1.5f;
		if (model != null)
		{
			for (int i=0; i<model.cells.GetLength(0); i++)
			{
				effectGO =(GameObject) Instantiate(effectPSPrefab,new Vector3(_x, y, -_z), Quaternion.identity);
				effectBlow[i]=effectGO.GetComponent<Blow>();
				_x += 3;
			}
		}

	}
}
