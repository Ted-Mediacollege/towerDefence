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
	public float current_width  = 1280;
	public float current_height  = 720;
	public guiButton[] buttons;

	public Vector3 posMenuStart = new Vector3(0,0,0);
	public Vector3 posMenuLevel = new Vector3(1280,0,0);
	public Vector3 posMenuCredits = new Vector3(1280,0,0);
	public Vector3 posMenuInstruct = new Vector3(200,1480,0);
	public Vector3 posMenuSettings = new Vector3(-200,-200,0);
	private Vector3 menuPosScale;
	private Vector3 menuPosTarget;

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
		current_width = Screen.width;
		current_height = Screen.height;
		traget = transform.position;
		startPos = transform.position;
		levelMenuDisplaysment = new Vector3(0, 0, 0);
		menuPosTarget = new Vector3(0, 0, 0);
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
		float rx = Screen.width / native_width;
		float ry = Screen.height / native_height;
		menuPosScale = new Vector3(rx,ry,0);
		
		// resolution change
		if(current_width!=Screen.width||current_height!=Screen.height){
			current_width = Screen.width;
			current_height = Screen.height;
			switchScreen(Vector3.Scale(menuPosTarget,menuPosScale));
		}
		
		GUI.skin = style;

		//start menu
		GUI.matrix = Matrix4x4.TRS ( levelMenuDisplaysment,Quaternion.identity,  new Vector3 (rx, ry, 1));
		//GUI.Box(new Rect((1280/2)-250,(720/2)-200,500,400), "Menu");
		if(GUI.Button(new Rect((1280/2)-80-400,(720/2)-80,160,60), "Levels")) {
			switchScreen(Vector3.Scale( posMenuLevel,menuPosScale));
			menuPosTarget = posMenuLevel;
		}
		if(GUI.Button(new Rect((1280/2)-80-400,(720/2)+0,160,60), "Credits")) {
			switchScreen(Vector3.Scale( posMenuCredits,menuPosScale));
			menuPosTarget = posMenuCredits;
		}

		if(GUI.Button(new Rect((1280/2)-80-400,(720/2)+80,160,60), "Instructions")) {
			switchScreen(Vector3.Scale( posMenuInstruct,menuPosScale));
			menuPosTarget = posMenuCredits;
		}

		if(GUI.Button(new Rect((1280/2)-80-400,(720/2)+160,160,60), "Settings")) {
			switchScreen(Vector3.Scale( posMenuSettings,menuPosScale));
			menuPosTarget = posMenuCredits;
		}

		// level menu
		GUI.matrix = Matrix4x4.TRS ( levelMenuDisplaysment-Vector3.Scale( menuPosScale,posMenuLevel),Quaternion.identity,  new Vector3 (rx, ry, 1));
		foreach(guiButton but in buttons){
			if(GUI.Button(new Rect(but.xPos,but.yPos,80,80), but.text)) {
				Application.LoadLevel(but.levelName);
			}
		}
		if(GUI.Button(new Rect(100,600,100,60), "Back")) {
			switchScreen(Vector3.Scale( posMenuStart,menuPosScale));
			menuPosTarget = posMenuStart;
		}
		// credit menu
		GUI.matrix = Matrix4x4.TRS ( levelMenuDisplaysment-Vector3.Scale( menuPosScale,posMenuCredits),Quaternion.identity,  new Vector3 (rx, ry, 1));
		if(GUI.Button(new Rect(100,600,100,60), "Back")) {
			switchScreen(Vector3.Scale( posMenuStart,menuPosScale));
			menuPosTarget = posMenuStart;
		}
		GUI.TextArea(new Rect(300,200,680,320)
		             ,"R1\n"+
		             "R2\n"+
		             "R3\n"+
		             "R4\n"
		);

		// instructions menu
		GUI.matrix = Matrix4x4.TRS ( levelMenuDisplaysment-Vector3.Scale( menuPosScale,posMenuInstruct),Quaternion.identity,  new Vector3 (rx, ry, 1));
		if(GUI.Button(new Rect(100,600,100,60), "Back")) {
			switchScreen(Vector3.Scale( posMenuStart,menuPosScale));
			menuPosTarget = posMenuStart;
		}

		// settings menu
		GUI.matrix = Matrix4x4.TRS ( levelMenuDisplaysment-Vector3.Scale( menuPosScale,posMenuSettings),Quaternion.identity,  new Vector3 (rx, ry, 1));
		if(GUI.Button(new Rect(100,600,100,60), "Back")) {
			switchScreen(Vector3.Scale( posMenuStart,menuPosScale));
			menuPosTarget = posMenuStart;
		}
		
		
		
		
		
		
		
		
		
	}
}