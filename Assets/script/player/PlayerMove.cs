using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	public float maxAngularVelocity = 10;
	public float drag = 0.97f;
	public float dashForce = 200;
	public float moveForce = 5;
	public float rotateForce = 2;
    public GameObject[] items;
    public int shootTime = 10;

	private int itemIcons = 4;
	private CurrentItem currentItem = 0;
	private int shootTimer = 0;

	private towerManager towerMngr;
	private GameObject buttonHolder;
	private GameObject[] buttons;
	private SpriteRenderer[] buttonsArts;
	private GameObject bulletHolder;
	
	//enum currentItem
	private enum CurrentItem {
		item1 = 0,
		item2 = 1,
		item3 = 2,
		item4 = 3,
		item5 = 4
	};

	private void Start(){
		towerMngr = GameObject.Find("gameManager").GetComponent<towerManager>() as towerManager;
		buttonHolder = GameObject.Find("itembuttons");
		bulletHolder = GameObject.Find("bullets");

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
	}

	private void Update(){
		Fire();
		float scrol = -Input.GetAxis("Mouse ScrollWheel");
		if(scrol!=0){
			if(scrol>0){
				if((int)currentItem<itemIcons){
					currentItem++;
				}
			}else{
				if((int)currentItem>0){
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
				currentItem = (CurrentItem)i;
				updateUI();
				break;
			}
		}
	}

	private void updateUI(){
		for (int i = 0;i<buttonsArts.Length;i++){
			if(i==(int)currentItem){
				buttonsArts[i].color = new Color(1,1,1,1);
			}else{
				buttonsArts[i].color = new Color(0.4f,0.4f,0.4f,1);
			}
		}
	}

	private void Fire(){
		if(shootTimer>0){
			shootTimer--;
		}
		
		bool fire = Input.GetMouseButtonDown(0);
		bool firing = Input.GetMouseButton(0);
		Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 mousePosition = new Vector3(mouseRay.origin.x,mouseRay.origin.y,0);
		if(items.Length>(int)currentItem){
			itemType currentItemType = items[(int)currentItem].GetComponent<Item>().type;
			switch(currentItemType){
				case itemType.Tower:
					if (fire){
						Collider2D mouseHit = Physics2D.OverlapPoint(mousePosition);
						//Debug.Log(mousex+" "+mousey);
						if(mouseHit!=null){
							//Debug.Log(mouseHit.name);
						}else{
						towerMngr.LoadTower(mousePosition,items[(int)currentItem]);
						}
					}
				break;
				case itemType.Bullet:
					if (firing&&shootTimer==0){
						shootTimer = shootTime;
						GameObject bul = GameObject.Instantiate( items[(int)currentItem]
						,transform.position 
						,Quaternion.identity) as GameObject;
						bul.transform.rotation = movement.RotateToPoint(transform,mousePosition);
						bul.transform.parent = bulletHolder.transform;
				}
				break;
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
