using UnityEngine;
using System.Collections;

public class itemButton : MonoBehaviour {
	private PlayerMove playerScript;
	private SpriteRenderer Art;

	private void Start(){
		playerScript = GameObject.Find("player").GetComponent<PlayerMove>() as PlayerMove;
		Art = gameObject.GetComponent<SpriteRenderer>();
	}

	//private void OnCollisionEnter2D(Collision2D collision){
	private void OnMouseDown () {
		Debug.Log("mouseDown");
		playerScript.buttonPress(Art);
	}
}
