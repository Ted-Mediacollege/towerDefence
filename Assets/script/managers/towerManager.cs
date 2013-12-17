using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class towerManager : MonoBehaviour {
	private GameObject towerHolder;
	internal List<GameObject> towers;
	private int timer;
	private GameObject test;

	private void Start(){
		towers = new List<GameObject>();
		towerHolder = GameObject.Find("towers");
	}
	
	public void LoadTower ( Vector3 spawnPoint , GameObject tower , Vector2 groundNormal) {
		Vector3 groundNormal3D = new Vector3(groundNormal.x,groundNormal.y,0);
		Quaternion quatGound = Quaternion.FromToRotation(Vector3.up , groundNormal3D);

		GameObject newEnemy = (GameObject)GameObject.Instantiate(tower,
		                                                         spawnPoint,
		                                                         quatGound);
		newEnemy.transform.parent = towerHolder.transform;
		towers.Add(newEnemy);
	}

}
