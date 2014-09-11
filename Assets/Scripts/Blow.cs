using UnityEngine;
using System.Collections;

public class Blow : MonoBehaviour {

	public ParticleSystem patricleSystemEffect;
	public GameObject effectGO;

	void Start () 
	{
		effectGO = this.gameObject;
		patricleSystemEffect = effectGO.GetComponent<ParticleSystem>();
	}

}
