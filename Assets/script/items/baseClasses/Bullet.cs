using UnityEngine;
using System.Collections;

public class Bullet : Item {
	public float speed = 0.01f;
	private void FixedUpdate(){
		Vector2 move =  movement.AngleToDirection(transform.eulerAngles.z);
		transform.localPosition = transform.position +new Vector3(move.x*speed,move.y*speed,0);
	}
	
	private void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag!=gameObject.tag 
		   && col.gameObject.tag!="tower"
		   && col.gameObject.name!="player" ){
			GameObject.Destroy(gameObject);
		}
	}
}
