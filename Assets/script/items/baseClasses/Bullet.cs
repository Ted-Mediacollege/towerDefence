using UnityEngine;
using System.Collections;

public class Bullet : Item {
	public float speed = 0.01f;
	private void Update(){
		Vector2 move =  movement.AngleToDirection(transform.eulerAngles.z);
		transform.localPosition = transform.position +new Vector3(move.x*speed*Time.deltaTime,move.y*speed*Time.deltaTime,0);
	}
	
	private void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.name!="player"&&col.gameObject.tag!="bullet"){
			GameObject.Destroy(gameObject);
		}
	}
}
