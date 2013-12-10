using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyManager : MonoBehaviour {

	public List<GameObject> enemies;
	public Transform[] SpawnPoints; 
	private int timer;
	private GameObject test;

	private void Start(){
		enemies = new List<GameObject>();
	}

	private void FixedUpdate () {
		timer++;
		int randomSpawn = Random.Range(0,3);
		Vector3 spawn = (Vector3)SpawnPoints[randomSpawn].transform.position;
		if(timer%20==0){
			GameObject newEnemy = (GameObject)GameObject.Instantiate(Resources.Load("enemy"),
			                                                         spawn,
			                                                         Quaternion.identity);
			enemies.Add(newEnemy);
		}
	}
}
