using UnityEngine;
using System.Collections;

public class tower : MonoBehaviour {
	private enemyManager enemyMngr;
	private Transform playerTrance;

	public float maxAngularVelocity = 50;
	public float maxRotForce = 90;
	public float agroDistance = 5;
	void Start () {
		enemyMngr = GameObject.Find("enemyManager").GetComponent<enemyManager>() as enemyManager;
		playerTrance = GameObject.Find("player").GetComponent<Transform>() as Transform;
	}

	float distCurrentTarget;
	Vector3 currentTarget;
	bool targetFound = false;
	void FixedUpdate(){
		targetFound = false;
		//get targer
		if(enemyMngr.enemies.Count>0){
			currentTarget = enemyMngr.enemies[0].transform.position;
			distCurrentTarget = Vector3.Distance(transform.position,currentTarget);
			if(distCurrentTarget<agroDistance){
				targetFound = true;
			}else{
				for (int i = 0; i < enemyMngr.enemies.Count; i++){
					distCurrentTarget = Vector3.Distance(transform.position,currentTarget);
					float distNewTarget = Vector3.Distance(transform.position,enemyMngr.enemies[i].transform.position);
					if(distNewTarget<distCurrentTarget){
						currentTarget = enemyMngr.enemies[i].transform.position;
					}
					if(distCurrentTarget<agroDistance){
						targetFound = true;
						break;
					}
				}
			}
			if(distCurrentTarget < agroDistance && targetFound){
				//print("rotate"+"\n");
				rigidbody2D.AddTorque(movement.RotateForce(transform,currentTarget,maxRotForce,10));
			}
		}

		//transform.rotation =  Quaternion.Euler(new Vector3(0, 0, movement.RotateToPoint(transform,playerTrance.position)));
		//print(distCurrentTarget+"\n");
		//limit force
		rigidbody2D.angularVelocity = movement.limitTorque(rigidbody2D.angularVelocity,maxAngularVelocity);
	}
}
