using UnityEngine;
using System.Collections;

public class crossHair : MonoBehaviour {

	[SerializeField]
	private Texture2D crosshair;

	void Start(){
		Screen.showCursor = false;
	}
	void OnGUI()
	{
		#if UNITY_WEBPLAYER || UNITY_EDITOR
		float xMin = Input.mousePosition.x - (crosshair.width / 4);
		float yMin = Screen.height-Input.mousePosition.y - (crosshair.height / 4);
		GUI.DrawTexture(new Rect(xMin, yMin, crosshair.width/2, crosshair.height/2), crosshair);
		#elif UNITY_PSM
		#endif
	}
}
