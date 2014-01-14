using UnityEngine;
using System.Collections;

public class minimap : MonoBehaviour {	
	private GameObject player;
	private EnemyManager enemymanager;
	public float zoom = 2;
	public int size = 100;
	public float centerX = 0;
	public float centerY = 0;

	private float zooming = 1;

	public float native_width  = 1280;
	public float native_height  = 720;

	private Texture2D texture;
	public Texture2D iconPlayer;
	public Texture2D iconStart;
	public Texture2D iconEnd;
	public Texture2D iconEnemy;


	
	void Start () {
		player = GameObject.Find("player");
		enemymanager = GameObject.Find("gameManager").GetComponent<EnemyManager>() as EnemyManager;
		texture = new Texture2D(20, 20);
	}
	
	void OnGUI() {
		if(Input.GetKey(KeyCode.M)) {
			zooming += 0.02F;
			if(zooming > 2F) {
				zooming = 2F;
			}
		} else {
			zooming -= 0.02F;
			if(zooming < 1F) {
				zooming = 1F;
			}
		}

		//scale
		float rx = Screen.width / native_width;
		float ry = Screen.height / native_height;
		GUI.matrix = Matrix4x4.TRS ( new Vector3(0, 0, 0),Quaternion.identity,  new Vector3 (rx, ry, 1));

		//minimap
		GUI.color = new Color(0.85F, 0.77F, 0.54F, 1F);
		GUI.DrawTexture(new Rect(0, 0, size * 2 * zooming, size * 2 * zooming), texture);

		GUI.color = new Color(1F, 1F, 1F);

		int count = enemymanager.enemies.Count;
		for(int i = 0; i < count; i++) {

			Vector3 enemycoord = worldToMapCoord(enemymanager.enemies[i].transform.position);
			GUI.DrawTexture(new Rect(enemycoord.x + (size * zooming) - 6, -enemycoord.y + (size * zooming) - 6, 12 * zooming, 12 * zooming), iconEnemy);
		}

		drawStartAndEnd();
		drawPlayer(worldToMapCoord(player.transform.position));
	}
	
	Vector3 worldToMapCoord(Vector3 worldCoord) {
		Vector3 mapCoord = new Vector3(size,size,0);
		
		mapCoord.x = (worldCoord.x * zoom - centerX) * zooming;
		mapCoord.y = (worldCoord.y * zoom - centerY) * zooming;
		
		return mapCoord;
	}

	void drawStartAndEnd() {
		for(int i = 0; i < enemymanager.SpawnPoints.Length; i++) {
			Vector3 ipos = worldToMapCoord(enemymanager.SpawnPoints[i].transform.position);
			GUI.DrawTexture(new Rect(ipos.x + (size * zooming) - 12, -ipos.y + (size * zooming) - 12, 24 * zooming, 24 * zooming), iconStart);
		}
		for(int j = 0; j < enemymanager.EndPoints.Length; j++) {
			Vector3 jpos = worldToMapCoord(enemymanager.EndPoints[j].transform.position);
			GUI.DrawTexture(new Rect(jpos.x + (size * zooming) - 12, -jpos.y + (size * zooming) - 12, 24 * zooming, 24 * zooming), iconEnd);
		}
	}

	void drawPlayer(Vector3 playercoord) {
		GUI.DrawTexture(new Rect(playercoord.x + (size * zooming) - 12, -playercoord.y + (size * zooming) - 12, 24 * zooming, 24 * zooming), iconPlayer);
	}
}
