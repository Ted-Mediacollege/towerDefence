using UnityEngine;
using System.Collections;

public class tower : MonoBehaviour {
	private enemyManager enemyMngr;
	private Transform playerTrance;
	// Use this for initialization
	void Start () {
		enemyMngr = GameObject.Find("manager").GetComponent<enemyManager>() as enemyManager;
		Debug.Log( math.add(3,7));
		playerTrance = GameObject.Find("player").GetComponent<Transform>() as Transform;
	}

	void FixedUpdate(){
		Debug.Log(enemyMngr.enemies.Count);
		rigidbody2D.angularVelocity = movement.RotateForce(transform,playerTrance.position,20,1);
	}
}
