using UnityEngine;
using System.Collections;

public class itemButton : MonoBehaviour {
	private PlayerMove playerScript;
	private SpriteRenderer Art;

	private void Start(){
		playerScript = GameObject.Find("player").GetComponent<PlayerMove>() as PlayerMove;
		Art = gameObject.GetComponent<SpriteRenderer>();
	}

	private void OnMouseDown () {
		playerScript.buttonPress(Art);
	}
}
