﻿using UnityEngine;
using System.Collections;

public class minimap : MonoBehaviour {	
	private GameObject player;
	private enemyManager enemymanager;
	public float zoom = 1;
	public int size = 100;
	
	void Start () {
		player = GameObject.Find("player");
		enemymanager = GameObject.Find("enemyManager").GetComponent<enemyManager>() as enemyManager;
	}
	
	void OnGUI() {
		GUI.color = new Color(0F, 0F, 0F, 1F);
		GUI.DrawTexture(new Rect(0, 0, size * 2, size * 2), new Texture2D(20, 20));
		
		Vector3 playercoord = worldToMapCoord(player.transform.position);
		GUI.color = new Color(0F, 0F, 1F, 1F);
		GUI.DrawTexture(new Rect(playercoord.x + size - 2, -playercoord.y + size - 2, 4, 4), new Texture2D(20, 20));

		int count = enemymanager.enemies.Count;
		for(int i = 0; i < count; i++) {

			Vector3 enemycoord = worldToMapCoord(enemymanager.enemies[i].transform.position);
			GUI.color = new Color(1F, 0F, 0F, 1F);
			GUI.DrawTexture(new Rect(enemycoord.x + size - 1, -enemycoord.y + size - 1, 2, 2), new Texture2D(20, 20));
		}
	}
	
	Vector3 worldToMapCoord(Vector3 worldCoord) {
		Vector3 mapCoord = new Vector3(size,size,0);
		
		mapCoord.x = worldCoord.x;
		mapCoord.y = worldCoord.y;
		
		return mapCoord;
	}
}
