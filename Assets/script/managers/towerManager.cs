using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class towerManager : MonoBehaviour {
	private GameObject towerHolder;
	internal List<GameObject> towers;
	private int timer;
	private GameObject test;
	public GameObject tower1;

	private void Start(){
		towers = new List<GameObject>();
		towerHolder = GameObject.Find("towers");
	}
	
	public void LoadTower ( Vector3 spawnPoint) {
		GameObject newEnemy = (GameObject)GameObject.Instantiate(tower1,
		                                                         spawnPoint,
		                                                         Quaternion.identity);
		newEnemy.transform.parent = towerHolder.transform;
		towers.Add(newEnemy);
	}

}
