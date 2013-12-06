using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	private Transform playerTrans;
	private Vector2 velocity;
	public float smoothTime = 0.2f;

	private void Start () {
		playerTrans = GameObject.Find("player").GetComponent<Transform>();
	}

	private void Update(){
		float newXPos = Mathf.SmoothDamp(transform.position.x, 
		                                 playerTrans.position.x, 
		                                 ref velocity.x,smoothTime);
		float newYPos = Mathf.SmoothDamp(transform.position.y, 
		                                 playerTrans.position.y, 
		                                 ref velocity.y,smoothTime);
		transform.position = new Vector3(newXPos,newYPos,transform.position.z);
	}
}
