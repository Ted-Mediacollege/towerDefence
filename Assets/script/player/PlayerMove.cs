using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	public float maxAngularVelocity = 10;
	public float drag = 0.97f;
	public float dashForce = 200;
	public float moveForce = 5;
	public float rotateForce = 2;

	private int items = 4;
	private int currentItem = 0;

	private towerManager towerMngr;
	private GameObject buttonHolder;
	private GameObject[] buttons;
	private SpriteRenderer[] buttonsArts;

	private void Start(){
		towerMngr = GameObject.Find("gameManager").GetComponent<towerManager>() as towerManager;
		buttonHolder = GameObject.Find("itembuttons");

		int buttonCount = buttonHolder.gameObject.transform.childCount;
		buttonsArts = new SpriteRenderer[buttonCount];
		
		for(int i = 0; i < buttonCount; i++) {
			buttonsArts[i] = buttonHolder.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>();
		}

		//buttonsArts = new SpriteRenderer[buttons.Length];
		//for (int i = 0;i<buttons.Length;i++){
		//	buttonsArts[i] = buttons[i].GetComponent<SpriteRenderer>();
		//}
		updateUI();
	}

	private void FixedUpdate () {
		Move();
		Rotate();
		Fire();
	}

	private void Update(){
		float scrol = -Input.GetAxis("Mouse ScrollWheel");
		if(scrol!=0){
			if(scrol>0){
				if(currentItem<items){
					currentItem++;
				}
			}else{
				if(currentItem>0){
					currentItem--;
				}
			}
			updateUI();
			//Debug.Log(scrol+" - "+currentItem);
		}
	}

	public void buttonPress(SpriteRenderer buttonArt){
		for (int i = 0;i<buttonsArts.Length;i++){
			if(buttonsArts[i] == buttonArt){
				currentItem = i;
				updateUI();
				break;
			}
		}
	}

	private void updateUI(){
		for (int i = 0;i<buttonsArts.Length;i++){
			if(i==currentItem){
				buttonsArts[i].color = new Color(1,1,1,1);
			}else{
				buttonsArts[i].color = new Color(0.4f,0.4f,0.4f,1);
			}
		}
	}

	private void Fire(){
		bool Fire = Input.GetMouseButtonDown(0);
		if (Fire){
			Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			Collider2D mouseHit = Physics2D.OverlapPoint(mouseRay.origin);
			float mousex =  mouseRay.origin.x;
			float mousey =  mouseRay.origin.y;
			//Debug.Log(mousex+" "+mousey);
			if(mouseHit!=null){
				//Debug.Log(mouseHit.name);
			}else{
				Vector3 spawnPosition = new Vector3(mousex,mousey,0);
				towerMngr.LoadTower(spawnPosition);
			}
		}
	}

	private void Rotate(){
		//get input
		float hor = -Input.GetAxis("Horizontal");
		//add force
		rigidbody2D.AddTorque(hor*rotateForce);
		//limit force
		rigidbody2D.angularVelocity = movement.limitTorque(rigidbody2D.angularVelocity,maxAngularVelocity);
	}

	private void Move(){
		//dash
		bool jump = Input.GetButtonDown("Jump");
		if(jump){
			rigidbody2D.AddForce( movement.ForceAndAngleToDirection(dashForce,transform.rotation.eulerAngles.z));
		}
		//move
		bool upKeyDown;
		if(Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.W)){
			upKeyDown = true;
		}else{
			upKeyDown = false;
		}
		if(upKeyDown){
			rigidbody2D.AddForce( movement.ForceAndAngleToDirection(moveForce,transform.rotation.eulerAngles.z));
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
}
