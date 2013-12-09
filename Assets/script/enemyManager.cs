using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyManager : MonoBehaviour {

	private List<GameObject> enemies;
	public Transform[] SpawnPoints; 
	private int timer;
	private GameObject test;

	private void Start(){
		enemies = new List<GameObject>();
	}
	// Update is called once per frame
	void FixedUpdate () {
		timer++;
		int randomSpawn = Random.Range(0,3);
		Vector3 spawn = (Vector3)SpawnPoints[randomSpawn].transform.position;
		if(timer%50==0){
			GameObject newEnemy = (GameObject)GameObject.Instantiate(Resources.Load("enemy"),
			                                                         spawn,
			                                                         Quaternion.identity);
			enemies.Add(newEnemy);
			//enemies[enemies.Count].GetComponent<enemy>().spawnPoints = SpawnPoints;
		}
	}
}
