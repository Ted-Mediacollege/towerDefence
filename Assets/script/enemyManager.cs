using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyManager : MonoBehaviour {

	private GameObject enemieHolder;
	public List<GameObject> enemies;
	public Transform[] SpawnPoints; 
	private int timer;
	private GameObject test;

	private void Start(){
		enemies = new List<GameObject>();
		enemieHolder = GameObject.Find("enemies");
	}

	private void FixedUpdate () {
		timer++;
		int randomSpawn = Random.Range(0,SpawnPoints.Length);
		if(enemies.Count<200){//maxSpawn
			Vector3 spawn = (Vector3)SpawnPoints[randomSpawn].transform.position;
			if(timer%100==0){
				GameObject newEnemy = (GameObject)GameObject.Instantiate(Resources.Load("enemy"),
				                                                         spawn,
				                                                         Quaternion.identity);
				newEnemy.transform.parent = enemieHolder.transform;
				enemies.Add(newEnemy);
			}
		}
	}
}
