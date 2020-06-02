using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
	public float kineticThreshold;
	public GameObject victory;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.transform.GetComponent<RibbonRider>() && other.transform.GetComponent<RibbonRider>().kinetic<kineticThreshold){
			victory.SetActive(true);
			other.transform.GetComponent<RibbonRider>().enabled=false;
		}
	}
}
