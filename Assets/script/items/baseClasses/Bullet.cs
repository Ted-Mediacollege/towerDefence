using UnityEngine;
using System.Collections;

public class Bullet : Item {
	[SerializeField]
	private float _speed = 0.01f;
	public int damage = 10;
	private Vector3 velocity;

	public float speed {
		get { return _speed;} 
	}
	
	private void Start(){
		Vector2 move =  movement.AngleToDirection(transform.eulerAngles.z);
		velocity = new Vector3(move.x*speed,move.y*speed,0);
	}

	private void FixedUpdate(){
		transform.localPosition = transform.position + velocity;
	}
	
	private void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag!=gameObject.tag 
		   && col.gameObject.tag!="tower"
		   && col.gameObject.name!="player" ){
			GameObject.Destroy(gameObject);
		}
	}
}
