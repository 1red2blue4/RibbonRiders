using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RibbonRider : MonoBehaviour {
	public float kinetic;
	public float potential;
	public float friction;
	public Vector3 velocity;
	public float total;
	private Rigidbody myBody;
	private float potentialConstant;
	private float kineticConstant;
	// Use this for initialization
	void Start () {
		
		potentialConstant=1f;
		kineticConstant=150f;
		potential=transform.position.y*potentialConstant;
		kinetic=1;
		friction=0;
		total=kinetic+potential+friction;
		myBody=transform.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 currentVelocity=myBody.velocity;

		potential=transform.position.y*potentialConstant;
		kinetic=total-potential-friction+1;
		float speed=Mathf.Sqrt(kinetic*kineticConstant);
		currentVelocity/=currentVelocity.magnitude;
		currentVelocity*=speed;

		if(Mathf.Abs(currentVelocity.z)>Mathf.Abs(currentVelocity.x)){
			currentVelocity=new Vector3(0,currentVelocity.y,currentVelocity.z);
		}
		else{
			currentVelocity=new Vector3(currentVelocity.x,currentVelocity.y,0);
		}


		RaycastHit hit;
		RaycastHit hit2;
		if (Physics.Raycast(transform.position, -Vector3.up, out hit,101.0f) && kinetic>10) {
			if(hit.transform.tag=="Slow"){
				friction+=2*speed/100;
			}
			if(hit.transform.tag=="Medium"){
				friction+=1*speed/100;
			}
		}

		if(Mathf.Abs(currentVelocity.z)>Mathf.Abs(currentVelocity.x)){
			if(currentVelocity.z>0){
				if (Physics.Raycast(new Vector3(transform.position.x+1,transform.position.y,transform.position.z), new Vector3(0,0,1), out hit,100.0f) &&
					Physics.Raycast(new Vector3(transform.position.x-1,transform.position.y,transform.position.z), new Vector3(0,0,1), out hit2,100.0f)) {

					if((hit.point.z-0.5f)>hit2.point.z){
						
						currentVelocity=new Vector3(currentVelocity.z,currentVelocity.y,0);
					}
					else if((hit2.point.z-0.5f)>hit.point.z){
						currentVelocity=new Vector3(-1*currentVelocity.z,currentVelocity.y,0);
					}
					else{
						currentVelocity=new Vector3(0,currentVelocity.y,-1*currentVelocity.z);
					}
				}
			}
			else{
				if (Physics.Raycast(new Vector3(transform.position.x+1,transform.position.y,transform.position.z), new Vector3(0,0,-1), out hit,100.0f) &&
					Physics.Raycast(new Vector3(transform.position.x-1,transform.position.y,transform.position.z), new Vector3(0,0,-1), out hit2,100.0f)) {

					if((hit.point.z-0.5f)>hit2.point.z){

						currentVelocity=new Vector3(currentVelocity.z,currentVelocity.y,0);
					}
					else if((hit2.point.z-0.5f)>hit.point.z){
						currentVelocity=new Vector3(-1*currentVelocity.z,currentVelocity.y,0);
					}
					else{
						currentVelocity=new Vector3(0,currentVelocity.y,-1*currentVelocity.z);
					}
				}
			}
		}
		else{
			if(currentVelocity.x>0){
				if (Physics.Raycast(new Vector3(transform.position.x,transform.position.y,transform.position.z+1), new Vector3(1,0,0), out hit,100.0f) &&
					Physics.Raycast(new Vector3(transform.position.x,transform.position.y,transform.position.z-1), new Vector3(1,0,0), out hit2,100.0f)) {

					if((hit.point.x-0.5f)>hit2.point.x){

						currentVelocity=new Vector3(0,currentVelocity.y,currentVelocity.x);
					}
					else if((hit2.point.x-0.5f)>hit.point.x){
						currentVelocity=new Vector3(0,currentVelocity.y,-1*currentVelocity.x);
					}
					else{
						currentVelocity=new Vector3(-1*currentVelocity.x,currentVelocity.y,0);
					}
				}
			}
			else{
				if (Physics.Raycast(new Vector3(transform.position.x,transform.position.y,transform.position.z+1), new Vector3(-1,0,0), out hit,100.0f) &&
					Physics.Raycast(new Vector3(transform.position.x,transform.position.y,transform.position.z-1), new Vector3(-1,0,0), out hit2,100.0f)) {

					if((hit.point.x-0.5f)>hit2.point.x){

						currentVelocity=new Vector3(0,currentVelocity.y,currentVelocity.x);
					}
					else if((hit2.point.x-0.5f)>hit.point.x){
						currentVelocity=new Vector3(0,currentVelocity.y,-1*currentVelocity.x);
					}
					else{
						currentVelocity=new Vector3(-1*currentVelocity.x,currentVelocity.y,0);
					}
				}
			}
		}
		if (Physics.Raycast(transform.position, -Vector3.up, out hit,101.0f)) {
			if(hit.transform.tag=="Slow"){
				friction+=2*speed/100;
			}
			if(hit.transform.tag=="Medium"){
				friction+=1*speed/100;
			}
		}

		if(!(float.IsNaN(currentVelocity.x) || float.IsNaN(currentVelocity.y) || float.IsNaN(currentVelocity.z))){ 
			myBody.velocity=currentVelocity;
			velocity=myBody.velocity;
		}

	}
}
