using UnityEngine;
using System.Collections;

public class ItemButton : MonoBehaviour {
	private Player playerScript;
	private Collider2D Art;

	private void Start(){
		playerScript = GameObject.Find("player").GetComponent<Player>() as Player;
		Art = gameObject.GetComponent<Collider2D>();
	}

	private void OnMouseDown () {
		playerScript.buttonPress(Art);
	}
}
