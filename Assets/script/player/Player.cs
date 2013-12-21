using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float maxAngularVelocity = 10;
	public float drag = 0.97f;
	public float dashForce = 200;
	public float moveForce = 5;
	public float rotateForce = 2;
    public GameObject[] items;
    public int shootTime = 10;
    
    [SerializeField]
    private Camera uiCam;

	private int itemIcons = 4;
	private int currentItem = 0;
	private int shootTimer = 0;

	private TowerManager towerMngr;
	private ItemManager itemMngr;
	private GameObject buttonHolder;
	private GameObject[] buttons;
	private Collider2D[] buttonsArts;
	private GameObject bulletHolder;
	private int buttonCount;

	//tower
	private float towerSpawnDistace = 0.3f;

	//line
	private LineRenderer lineRenderer;
	public Color lineColor1 = Color.yellow;
	public Color lineColor2 = Color.red;
	private int lengthOfLineRenderer = 2;
	

	private void Start(){
		itemMngr = transform.GetComponent<ItemManager>();
		towerMngr = GameObject.Find("gameManager").GetComponent<TowerManager>() as TowerManager;
		buttonHolder = GameObject.Find("itembuttons");
		bulletHolder = GameObject.Find("bullets");

		buttonCount = buttonHolder.gameObject.transform.childCount;
		buttonsArts = new Collider2D[buttonCount];
		for(int i = 0; i < buttonCount; i++) {
			buttonsArts[i] = buttonHolder.gameObject.transform.GetChild(i).GetComponent<Collider2D>();
		}
		//create line
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.SetColors(lineColor1, lineColor2);
		lineRenderer.SetWidth(0.1F, 0.1F);
		lineRenderer.SetVertexCount(lengthOfLineRenderer);
		lineRenderer.material = new Material (Shader.Find("Particles/Additive"));
		lineRenderer.enabled = false;
		lineRenderer.sortingLayerName = "level";

		itemMngr.setItem(currentItem);
	}

	private void FixedUpdate () {
		Move();
		Rotate();
	}

	private void Update(){
		ItemUpdate();
		float scrol = -Input.GetAxis("Mouse ScrollWheel");
		if(scrol!=0){
			if(scrol>0){
				if(currentItem<buttonCount-1){
					currentItem++;
				}
			}else{
				if(currentItem>0){
					currentItem--;
				}
			}
			itemMngr.setItem(currentItem);
		}
	}

	public void buttonPress(Collider2D buttonArt){
		for (int i = 0;i<buttonCount;i++){
			if(buttonsArts[i] == buttonArt){
				currentItem = i;
				itemMngr.setItem(currentItem);
				break;
			}
		}
	}

	private void ItemUpdate(){

		if(shootTimer>0){
			shootTimer--;
		}

		//get input and mouse position
		bool click = Input.GetMouseButtonDown(0);
		bool firing = Input.GetMouseButton(0);
		Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		Ray uiRay = uiCam.ScreenPointToRay(Input.mousePosition);
		Vector3 mousePosition = new Vector3(mouseRay.origin.x,mouseRay.origin.y,0);
		Vector2 mousePos2D = new Vector2(mouseRay.origin.x,mouseRay.origin.y);
		Vector2 thisPos2D = new Vector2(transform.position.x,transform.position.y);
		
		
		
		if(buttonCount>currentItem){
			Collider2D uiHit = Physics2D.OverlapPoint(uiRay.origin
			                                          ,1 << LayerMask.NameToLayer("UI"));
			
			if(uiHit&&click){   
				//buttonPress(uiHit);
				for (int i = 0;i<buttonCount;i++){
					if(buttonsArts[i] == uiHit){
						currentItem = i;
						itemMngr.setItem(currentItem);
						break;
					}
				}
			}else{
				itemType currentItemType = items[currentItem].GetComponent<Item>().type;
				switch(currentItemType){
				case itemType.Tower:
					
					//cast aim ray
					Vector2 mousedirection = mousePos2D-thisPos2D;
					RaycastHit2D aimRay = Physics2D.Raycast(thisPos2D,mousedirection,100,1 << LayerMask.NameToLayer("Level"));
					//draw aim line
					lineRenderer.enabled = true;
					lineRenderer.SetPosition(0, transform.position);
					lineRenderer.SetPosition(1, aimRay.point);
					
					//spawn tower
					if (click){                      
						Collider2D mouseCircle = Physics2D.OverlapCircle(aimRay.point
						                                                 ,towerSpawnDistace
						                                                 ,1 << LayerMask.NameToLayer("Towers"));
						if(mouseCircle!=null){
						}else{
							towerMngr.LoadTower(aimRay.point,items[currentItem],aimRay.normal);
						}
					}
					break;
				case itemType.Bullet:
					//fire bullet
					lineRenderer.enabled = false;
					if (firing&&shootTimer==0){
						shootTimer = shootTime;
						GameObject bul = GameObject.Instantiate( items[currentItem]
						                                        ,transform.position 
						                                        ,movement.RotateToPoint(transform,mousePosition)) as GameObject;
						bul.transform.parent = bulletHolder.transform;
					}
					break;
				}
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
