using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

	private GameObject enemieHolder;
	internal List<GameObject> enemies;
	public Transform[] SpawnPoints; 
	public Transform[] EndPoints; 
	private int timer;
	private GameObject test;
	private GameObject enemy1;
	private GameObject moneyobject;

	[SerializeField]
	private int livePoints = 10;

	private void Start(){
		enemies = new List<GameObject>();
		enemieHolder = GameObject.Find("enemies");
		enemy1 = Resources.Load("enemy") as GameObject;
		moneyobject = Resources.Load("money") as GameObject;
	}

	private void FixedUpdate () {
		//Debug.Log("enemy count: "+enemies.Count);
		//timer++;
		//int randomSpawn = Random.Range(0,SpawnPoints.Length);
		//if(enemies.Count<2000){//maxSpawn
		//	Vector3 spawn = (Vector3)SpawnPoints[randomSpawn].transform.position;
		//	if(timer%100==0){
		//		GameObject newEnemy = (GameObject)GameObject.Instantiate(enemy1,
		//		                                                         spawn,
		//		                                                         Quaternion.identity);
		//		newEnemy.transform.parent = enemieHolder.transform;
		//		enemies.Add(newEnemy);
		//	}
		//}
	}

	public void spawnEnemy(string enemytype, string spawnpoint) {
		for(int i = 0; i < SpawnPoints.Length; i++) {
			if(SpawnPoints[i].name == spawnpoint) {
				Vector3 spawn = (Vector3)SpawnPoints[i].transform.position;
				spawn.x += -1 + Random.Range(0.0F,2.0F);
				spawn.y += -1 + Random.Range(0.0F,2.0F);

				GameObject newEnemy = (GameObject)GameObject.Instantiate(enemy1, spawn, Quaternion.identity);
				newEnemy.transform.parent = enemieHolder.transform;
				enemies.Add(newEnemy);
				break;
			}
		}
	}

	public void removeEnemy(GameObject enemy, bool reachedEnd = false){
		enemies.Remove(enemy);
		if(reachedEnd){
			livePoints--;
			if(livePoints<0){
				Debug.Log("life: "+livePoints+" gameOver");
			}else{
				Debug.Log("life: "+livePoints);
			}
		} else {
			GameObject money = (GameObject)GameObject.Instantiate(moneyobject, enemy.transform.position, Quaternion.identity);
		}
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
