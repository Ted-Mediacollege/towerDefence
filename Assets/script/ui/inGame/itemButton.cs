using UnityEngine;
using System.Collections;

public class itemButton : MonoBehaviour {
	private PlayerMove playerScript;

	private void Start(){
		playerScript = GameObject.Find("player").GetComponent<PlayerMove>() as PlayerMove;
	}

	//private void OnCollisionEnter2D(Collision2D collision){
	private void OnMouseDown () {
		Debug.Log("mouseDown");
		playerScript.buttonPress(gameObject);
	}
}
