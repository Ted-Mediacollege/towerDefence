using UnityEngine;
using System.Collections;

public enum GameStatus{
	Playing,
	GameOver,
	Won,
	Pause
}

public class winLoseScreen : MonoBehaviour {

	private float native_width  = 1280;
	private float native_height  = 720;
	public GameStatus gameStatus;
	public GUISkin skin;
	public string nextLevelName;
	private ItemManager itemMngr;

	void Start(){
		gameStatus = GameStatus.Playing;
		itemMngr = GameObject.Find("player").GetComponent<ItemManager>();
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)) { 
			if(gameStatus == GameStatus.Playing){
				gameStatus = GameStatus.Pause;
				Time.timeScale = 0;
				itemMngr.enabled = false;
			}else if(gameStatus == GameStatus.Pause){
				gameStatus = GameStatus.Playing;
				Time.timeScale = 1;
				itemMngr.enabled = true;
			}
		}
	}

	void OnGUI(){
		if(gameStatus!=GameStatus.Playing){
			GUI.skin = skin;
			float rx = Screen.width / native_width;
			float ry = Screen.height / native_height;
			GUI.matrix = Matrix4x4.TRS ( new Vector3(0, 0, 0),Quaternion.identity,  new Vector3 (rx, ry, 1));
		}
		if(gameStatus==GameStatus.Won||gameStatus==GameStatus.GameOver){
			if(Time.timeScale == 1){
				Time.timeScale = 0;
				itemMngr.enabled = false;
			}
		}
		switch(gameStatus){
		case GameStatus.Playing:
			break;
		case GameStatus.GameOver:
			GUI.Box(new Rect((1280/2)-250,(720/2)-200,500,400), "GameOver");
			if(GUI.Button(new Rect((1280/2)-100,450,200,60), "Restart")) {
				Application.LoadLevel(Application.loadedLevelName);
			}
			break;
		case GameStatus.Won:
			GUI.Box(new Rect((1280/2)-250,(720/2)-200,500,400), "Win");
			//GUI.TextField(new Rect((1280/2)-50,(720/2)-100,100,80),"SCORE: 0");
			if(GUI.Button(new Rect((1280/2)-80+120,450,160,60), "Main Menu")) {
				Application.LoadLevel("MainMenu");
			}
			if(GUI.Button(new Rect((1280/2)-80+0,450,160,60), "Restart")) {
				Application.LoadLevel(Application.loadedLevelName);
			}
			if(GUI.Button(new Rect((1280/2)-80-120,450,160,60), "Next")) {
				Application.LoadLevel(nextLevelName);
			}
			break;
		case GameStatus.Pause:
			GUI.Box(new Rect((1280/2)-250,(720/2)-200,500,400), "Menu");
			if(GUI.Button(new Rect((1280/2)-100+120,450,200,60), "Main Menu")) {
				Application.LoadLevel("MainMenu");
			}
			if(GUI.Button(new Rect((1280/2)-100-120,450,200,60), "Restart")) {
				Application.LoadLevel(Application.loadedLevelName);
			}
			break;
		}
	}
}
