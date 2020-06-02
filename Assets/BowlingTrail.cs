using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingTrail : MonoBehaviour {
	public Vector3[] trailPoints;
	public float[] frictionPoints;
	public float[] zRotPoints;
	private int index;
	public float mass;
	public float potential;
	public float kinetic;
	public float friction;
	private float totalEnergy;
	public GameObject potentialRibbon;
	public GameObject kineticRibbon;
	public GameObject frictionRibbon;
	public float scale;
	public GameObject Rider;
	// Use this for initialization
	void Start () {
		totalEnergy=potential+kinetic+friction;
	}

	// Update is called once per frame
	void Update () {
		/*if(index==trailPoints.Length)return;

		float velocity = Mathf.Sqrt(kinetic/mass);
		Vector3 direction = trailPoints[index] - transform.position;
		float distance = Vector3.Distance(transform.position, trailPoints[index]);
		Vector3 originalPos = transform.position;

		if (direction.magnitude > 0)
		{
			direction = (direction / direction.magnitude) * velocity;
			transform.position += direction;
		}

		if ((distance < Vector3.Distance(transform.position, trailPoints[index])) || distance < 0.1f) 
		{
			transform.position = trailPoints[index];
			index++;
			if(index==trailPoints.Length){
				Time.timeScale=0;
				return;
			}
		}

		kinetic = kinetic - (transform.position.y - originalPos.y)*mass/100 - velocity*frictionPoints[index];
		potential = potential + (transform.position.y - originalPos.y)*mass/100;
		friction = friction+ velocity*frictionPoints[index];*/
		transform.position=Rider.transform.position;
		float zRot=0;
		if(Mathf.Abs(Rider.transform.GetComponent<RibbonRider>().velocity.x)>Mathf.Abs(Rider.transform.GetComponent<RibbonRider>().velocity.z)){
			zRot=0;
		}
		else{
			zRot=90;
		}
		kinetic=Rider.transform.GetComponent<RibbonRider>().kinetic;
		potential=Rider.transform.GetComponent<RibbonRider>().potential;
		friction=Rider.transform.GetComponent<RibbonRider>().friction;
		totalEnergy=Rider.transform.GetComponent<RibbonRider>().total;

		float potentialScale=(potential/totalEnergy)*scale;
		float kineticScale=((kinetic)/totalEnergy)*scale/potentialScale;
		float frictionScale=((friction/totalEnergy)*scale)/potentialScale/kineticScale;

		potentialRibbon.transform.localScale = new Vector3(7f, potentialScale, 7f);
		if(!float.IsNaN(kineticScale))
		kineticRibbon.transform.localScale = new Vector3(6f/7, kineticScale, 6f/7);
		if(!float.IsNaN(frictionScale))
		frictionRibbon.transform.localScale = new Vector3(5f/6/7,frictionScale,5f/6/7);


		//float zRot=zRotPoints[index];
		potentialRibbon.transform.rotation=Quaternion.Euler(90,0,zRot);//Quaternion.LookRotation(new Vector3(direction.x,direction.y,0));
		kineticRibbon.transform.rotation=Quaternion.Euler(90,0,zRot);//Quaternion.LookRotation(new Vector3(direction.x,direction.y,0));
		frictionRibbon.transform.rotation=Quaternion.Euler(90,0,zRot);//Quaternion.LookRotation(new Vector3(direction.x,direction.y,0));

		float colScale=1;
		/*if(distance<5){
		//	colScale=distance/5;
		}
		if(index>0 && Vector3.Distance(originalPos,trailPoints[index-1])<5){
		//	colScale=Vector3.Distance(originalPos,trailPoints[index-1])/5;
		}*/
		GameObject temp=(GameObject)Instantiate(potentialRibbon,potentialRibbon.transform.position,potentialRibbon.transform.rotation);
		temp.transform.localScale*=colScale;
		//temp=(GameObject)Instantiate(kineticRibbon,kineticRibbon.transform.position,kineticRibbon.transform.rotation);
		//temp.transform.localScale*=colScale;
		//temp=(GameObject)Instantiate(frictionRibbon,frictionRibbon.transform.position,frictionRibbon.transform.rotation);
		//temp.transform.localScale*=colScale;


	}
}
