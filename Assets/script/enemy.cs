using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {
	private pathManager pathMngr;
	private Vector3[] path;
	private int ProgressInPath = 0;
	private float DistPointReached = 1.5f;

	public float angularVelocity;
	public float drag = 0.97f;
	public float dashForce = 200;
	public float moveForce = 2;
	public float maxRotateForce = 1;

	private void Start () {
		pathMngr = GameObject.Find("pathManager").GetComponent<pathManager>() as pathManager;
		path = pathMngr.getClosestPath(transform.position);
	}

	private void FixedUpdate(){
		float distanceToPoint = Vector3.Distance(transform.position,path[ProgressInPath]);
		rigidbody2D.AddForce( ForceAndAngleToDirection(moveForce,transform.rotation.eulerAngles.z));
		Drag();
		RotateToPoint();
		if(DistPointReached>distanceToPoint){
			ProgressInPath++;
			if(ProgressInPath>path.Length-1){
				path = pathMngr.getClosestPath(transform.position);
				ProgressInPath = 0;
			}
		}
	}

	private Vector2 ForceAndAngleToDirection(float force,float angle){
		float xForce = force * Mathf.Sin(angle*Mathf.PI/180);
		float yForce = force * Mathf.Cos(angle*Mathf.PI/180);
		return new Vector2(-xForce,yForce);
	}

	private void RotateToPoint(){
		float thisRotation =  transform.eulerAngles.z;
		float deltaY = transform.position.y - path[ProgressInPath].y;
		float deltaX = transform.position.x - path[ProgressInPath].x;
		float rotationGoal = (Mathf.Atan2(deltaY,deltaX) * 180 / Mathf.PI)+90;
		//transform.rotation =  Quaternion.Euler(new Vector3(0, 0, angleInDegrees));
		float deltaAngel = thisRotation - rotationGoal;
		//Debug.Log(thisRotation +"-"+rotationGoal+"="+deltaAngel);
		if(deltaAngel>360){
			deltaAngel -=360;
		}

		if(deltaAngel<0){
			deltaAngel +=360;
		}
		if(deltaAngel>maxRotateForce){
			deltaAngel = maxRotateForce;
		}else if(deltaAngel<maxRotateForce){
			deltaAngel = -maxRotateForce;
		}
		//Debug.Log(deltaAngel);
		//Debug.Log(path[ProgressInPath]);
		//get input
		//add force
		rigidbody2D.AddTorque(-deltaAngel);
		//limit force
		if(rigidbody2D.angularVelocity > angularVelocity){
			rigidbody2D.angularVelocity = angularVelocity;
		}else if(rigidbody2D.angularVelocity < -angularVelocity){
			rigidbody2D.angularVelocity = -angularVelocity;
		}
	}

	private void Drag(){
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
}
