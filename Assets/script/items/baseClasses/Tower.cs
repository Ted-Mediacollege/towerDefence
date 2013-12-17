using UnityEngine;
using System.Collections;

public class Tower : Item {
	private enemyManager enemyMngr;
	private Transform playerTrance;

	public float maxAngularVelocity = 50;
	public float maxRotForce = 90;
	public float agroDistance = 5;
	public GameObject bullet;
	private int shootTimer = 0;
	public int shootTime = 10;
	private GameObject bulletHolder;

	public GameObject gun;

	void Start () {
		enemyMngr = GameObject.Find("gameManager").GetComponent<enemyManager>() as enemyManager;
		playerTrance = GameObject.Find("player").GetComponent<Transform>() as Transform;

		bulletHolder = GameObject.Find("bullets");
	}

	private float distCurrentTarget;
	private Vector3 currentTarget;
	private bool targetFound = false;

	private void FixedUpdate(){
		targetFound = false;
		Rotate();
		Shoot();
	}

	private void Rotate(){
		//get targer
		if(enemyMngr.enemies.Count>0){
			currentTarget = enemyMngr.enemies[0].transform.position;
			distCurrentTarget = Vector3.Distance(transform.position,currentTarget);
			if(distCurrentTarget<agroDistance){
				targetFound = true;
			}else{
				for (int i = 0; i < enemyMngr.enemies.Count; i++){
					distCurrentTarget = Vector3.Distance(gun.transform.position,currentTarget);
					float distNewTarget = Vector3.Distance(gun.transform.position,enemyMngr.enemies[i].transform.position);
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
				gun.rigidbody2D.AddTorque(movement.RotateForce(gun.transform.position,
				                                               gun.transform.eulerAngles.z,
				                                           currentTarget,
				                                           maxRotForce,
				                                           10));
			}
		}
		//transform.rotation =  Quaternion.Euler(new Vector3(0, 0, movement.RotateToPoint(transform,playerTrance.position)));
		
		//limit force
		gun.rigidbody2D.angularVelocity = movement.limitTorque(rigidbody2D.angularVelocity,maxAngularVelocity);
	}

	private void Shoot(){
		if(shootTimer>0){
			shootTimer--;
		}

		//GameObject.Instantiate 
		if (shootTimer==0&&targetFound){
			shootTimer = shootTime;

			Vector3 displace = movement.AngleToDirection(gun.transform.eulerAngles.z);
			Vector3 thisPos = gun.transform.position;
			Vector3 spawnPoint = new Vector3(thisPos.x+(displace.x*0.2f),thisPos.y+(displace.y*0.2f),0);

			GameObject bul = GameObject.Instantiate( bullet
			                                        ,spawnPoint 
			                                        ,gun.transform.rotation) as GameObject;
			bul.transform.parent = bulletHolder.transform;
		}
	}
}



