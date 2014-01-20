using UnityEngine;
using System.Collections;

public class GuiObject{
	public float xPos;
	public float yPos;
}

[System.Serializable]
public class guiButton:GuiObject{
	public string text;
	public string levelName;
}
[ExecuteInEditMode]
public class MenuStart : MonoBehaviour {
	
	public float native_width  = 1280;
	public float native_height  = 720;
	public guiButton[] buttons;

	public Vector3 posMenuStart = new Vector3(0,0,0);
	public Vector3 posMenuLevel = new Vector3(1280,0,0);
	public Vector3 posMenuCredits = new Vector3(1280,0,0);

	//transitio
	public Vector3 levelMenuDisplaysment = new Vector3(0, 0, 0);
	public float speed = 1.0F;
	private float startTime = 0;
	private float journeyLength = 0;
	public float smooth = 5.0F;
	private Vector3 traget = new Vector3(0,0,0);
	private Vector3 startPos = new Vector3(0,0,0);

	public GUISkin style;

	void Start(){
		traget = transform.position;
		startPos = transform.position;
		levelMenuDisplaysment = new Vector3(0, 0, 0);
	}
	void Update() {
		float distCovered = (Time.time - startTime) * speed;
		journeyLength = Vector3.Distance(startPos, traget);
		float fracJourney = distCovered / journeyLength;
		levelMenuDisplaysment = Vector3.Lerp(startPos, traget, fracJourney);
	}

	void switchScreen( Vector3 newTarget){
		traget = newTarget;
		startPos = levelMenuDisplaysment;
		startTime = Time.time;
		journeyLength = Vector3.Distance(startPos, traget);
	}

	void OnGUI(){
		GUI.skin = style;

		float rx = Screen.width / native_width;
		float ry = Screen.height / native_height;
		GUI.matrix = Matrix4x4.TRS ( levelMenuDisplaysment,Quaternion.identity,  new Vector3 (rx, ry, 1));

		GUI.Box(new Rect((1280/2)-250,(720/2)-200,500,400), "Menu");

		if(GUI.Button(new Rect((1280/2)-80,(720/2)-80,160,60), "Level Menu")) {
			switchScreen(posMenuLevel);
		}
		if(GUI.Button(new Rect((1280/2)-80,(720/2)+0,160,60), "Credit Menu")) {
			switchScreen(posMenuCredits);
		}

		// level menu
		GUI.matrix = Matrix4x4.TRS ( levelMenuDisplaysment-posMenuLevel,Quaternion.identity,  new Vector3 (rx, ry, 1));
		foreach(guiButton but in buttons){
			if(GUI.Button(new Rect(but.xPos,but.yPos,80,20), but.text)) {
				Application.LoadLevel(but.levelName);
			}
		}
		if(GUI.Button(new Rect(100,100,80,20), "Back")) {
			switchScreen(posMenuStart);
		}
		// credit menu
		GUI.matrix = Matrix4x4.TRS ( levelMenuDisplaysment-posMenuCredits,Quaternion.identity,  new Vector3 (rx, ry, 1));
		if(GUI.Button(new Rect(100,100,80,20), "Back")) {
			switchScreen(posMenuStart);
		}

	}
}