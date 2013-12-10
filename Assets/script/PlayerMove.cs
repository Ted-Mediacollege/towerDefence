using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	public float angularVelocity;
	public float drag = 0.97f;
	public float dashForce = 200;
	public float moveForce = 5;
	public float rotateForce = 2;

	private void FixedUpdate () {
		Move();
		Rotate();
	}

	private void Rotate(){
		//get input
		float hor = -Input.GetAxis("Horizontal");
		//add force
		rigidbody2D.AddTorque(hor*rotateForce);
		//limit force
		if(rigidbody2D.angularVelocity > angularVelocity){
			rigidbody2D.angularVelocity = angularVelocity;
		}else if(rigidbody2D.angularVelocity < -angularVelocity){
			rigidbody2D.angularVelocity = -angularVelocity;
		}
	}

	private void Move(){
		//dash
		bool jump = Input.GetButtonDown("Jump");
		if(jump){
			rigidbody2D.AddForce( ForceAndAngleToDirection(dashForce,transform.rotation.eulerAngles.z));
		}
		//move
		bool upKeyDown = Input.GetKey(KeyCode.UpArrow);
		if(upKeyDown){
			rigidbody2D.AddForce( ForceAndAngleToDirection(moveForce,transform.rotation.eulerAngles.z));
		}
		//drag
		float newX = 0;
		float newY = 0;
		if(rigidbody2D.velocity.x!=0){
			newX = rigidbody2D.velocity.x*drag;
		}else{
			newX = rigidbody2D.velocity.x;
		}
		if(rigidbody2D.velocity.y!=0){
			newY= rigidbody2D.velocity.y*drag;
		}else{
			newY= rigidbody2D.velocity.y;
		}
		rigidbody2D.velocity = new Vector2(newX,newY);
	}

	private Vector2 ForceAndAngleToDirection(float force,float angle){
		float xForce = force * Mathf.Sin(angle*Mathf.PI/180);
		float yForce = force * Mathf.Cos(angle*Mathf.PI/180);
		return new Vector2(-xForce,yForce);
	}
}
