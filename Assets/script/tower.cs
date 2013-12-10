using UnityEngine;
using System.Collections;

public class tower : MonoBehaviour {
	private enemyManager enemyMngr;
	private Transform playerTrance;

	public float maxAngularVelocity = 50;
	public float maxRotForce = 90;
	public float maxangularVelocity = 90;
	void Start () {
		enemyMngr = GameObject.Find("manager").GetComponent<enemyManager>() as enemyManager;
		playerTrance = GameObject.Find("player").GetComponent<Transform>() as Transform;
	}

	void FixedUpdate(){
		//Debug.Log(enemyMngr.enemies.Count);
		//transform.rotation =  Quaternion.Euler(new Vector3(0, 0, movement.RotateToPoint(transform,playerTrance.position)));
		rigidbody2D.AddTorque(movement.RotateForce(transform,
		                                           playerTrance.position,
		                                           maxRotForce));
		//limit force
		rigidbody2D.angularVelocity = movement.limitTorque(rigidbody2D.angularVelocity,maxAngularVelocity);
	}
}
