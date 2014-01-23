using UnityEngine;
using System.Collections;

public enum GameStatus{
	Playing,
	GameOver,
	Won,
}

public class winScreen : MonoBehaviour {

	private float native_width  = 1280;
	private float native_height  = 720;
	public GameStatus gameStatus;
	public GUISkin skin;
	public string nextLevelName;

	void Start(){
		gameStatus = GameStatus.Playing;
	}

	void OnGUI(){
		if(gameStatus!=GameStatus.Playing){
			GUI.skin = skin;
			float rx = Screen.width / native_width;
			float ry = Screen.height / native_height;
			GUI.matrix = Matrix4x4.TRS ( new Vector3(0, 0, 0),Quaternion.identity,  new Vector3 (rx, ry, 1));
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
			GUI.TextField(new Rect((1280/2)-50,(720/2)-100,100,80),"SCORE: 0");
			if(GUI.Button(new Rect((1280/2)-100+120,450,200,60), "Next")) {
				Application.LoadLevel(nextLevelName);
			}
			if(GUI.Button(new Rect((1280/2)-100-120,450,200,60), "Restart")) {
				Application.LoadLevel(Application.loadedLevelName);
			}
			break;
		}
	}
}
