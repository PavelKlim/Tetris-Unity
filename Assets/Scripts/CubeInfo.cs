using UnityEngine;
using System.Collections;

public class CubeInfo : MonoBehaviour 
{
	public float cubeSize = 0;
	public Color cubeColor=Color.white;
	public GameObject cube;
	public GameObject cubeColoredPrefab;
	public GameObject cubeColored;

	void Start()
	{
		string cubeNameColored = "CubeColored";
		cube = this.gameObject;
		cubeColoredPrefab=(GameObject)Resources.Load(cubeNameColored, typeof(GameObject));
		cubeColored=(GameObject) Instantiate (cubeColoredPrefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
		//cubeColored.SetActive(false);
		cubeColored.transform.parent = this.transform;

	}	
}
