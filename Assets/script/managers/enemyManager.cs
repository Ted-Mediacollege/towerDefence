using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyManager : MonoBehaviour {

	private GameObject enemieHolder;
	internal List<GameObject> enemies;
	public Transform[] SpawnPoints; 
	public Transform[] EndPoints; 
	private int timer;
	private GameObject test;
	private GameObject enemy1;

	private void Start(){
		enemies = new List<GameObject>();
		enemieHolder = GameObject.Find("enemies");
		enemy1 = Resources.Load("enemy") as GameObject;
	}

	private void FixedUpdate () {
		//Debug.Log("enemy count: "+enemies.Count);
		timer++;
		int randomSpawn = Random.Range(0,SpawnPoints.Length);
		if(enemies.Count<2000){//maxSpawn
			Vector3 spawn = (Vector3)SpawnPoints[randomSpawn].transform.position;
			if(timer%100==0){
				GameObject newEnemy = (GameObject)GameObject.Instantiate(enemy1,
				                                                         spawn,
				                                                         Quaternion.identity);
				newEnemy.transform.parent = enemieHolder.transform;
				enemies.Add(newEnemy);
			}
		}
	}

	public void removeEnemy(GameObject enemy){
		//for (int i = 0; i < enemies.Count;i++){
		//	if(enemies[i]==enemy){
		//		break;
		//		enemies.Remove(enemies[i]);
		//	}
		//}
		enemies.Remove(enemy);
		GameObject.Destroy(enemy);
	}

	public Vector3 getClosestEnd(Vector3 selfPos){
		int closest = 0;
		float distance = Vector3.Distance(EndPoints[closest].position, selfPos);
		
		for(int p = 1; p < EndPoints.Length; p++) {
			float newDistance = Vector3.Distance(EndPoints[p].position, selfPos);
			if(newDistance < distance) {
				distance = newDistance;
				closest = p;
			}
		}
		return EndPoints[closest].position;
	}
}
