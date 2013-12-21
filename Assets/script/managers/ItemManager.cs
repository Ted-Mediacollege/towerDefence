using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour {
	
	[SerializeField]
	private GameObject itemHolder;
	
	private float screenHeight;
	private float screenWidth;
	private Vector3 topRight;
	private Vector3 itemDisplasment;
	
	private Player playerScript;
	private SpriteRenderer[] sprites;
	internal List<GameObject> itemList;

	float[] cT;
	bool[] cB;
	int itemLenght;

	void Awake () {
		itemList = new List<GameObject>();
		playerScript = transform.GetComponent<Player>() as Player;
		itemLenght = playerScript.items.Length;

		screenHeight = 2f * Camera.main.orthographicSize;
		screenWidth = screenHeight * Camera.main.aspect;
		topRight = new Vector3( screenWidth/2, screenHeight/2,10);
		itemDisplasment = new Vector3( -0.2f, -0.2f,0);

		sprites = new SpriteRenderer[itemLenght];
		cT = new float[itemLenght];
		cB = new bool[itemLenght];
		
		for ( int i = 0; i < itemLenght; i++){
			//create button
			GameObject button = new GameObject("itemIcon");
			
			// add renderer
			SpriteRenderer img = button.AddComponent<SpriteRenderer>();
			img.sprite = playerScript.items[i].GetComponent<Item>().iconS;
			img.sortingLayerName = "ui";
			sprites[i] = img;
			
			//add collider
			BoxCollider2D collider = button.AddComponent<BoxCollider2D>();
			collider.size = new Vector2(0.64f,0.64f);
			collider.isTrigger = true;
			
			//add script 
			//button.AddComponent<ItemButton>();
			
			
			//position button
			button.transform.parent = itemHolder.transform;
			button.transform.position = itemHolder.transform.position+topRight+itemDisplasment+new Vector3(-0.32f+i*-0.64f,-0.32f,0);
			button.layer = LayerMask.NameToLayer("UI");
			transform.Translate(  new Vector3(0,0.64f,0));
			itemList.Add (button);
		}
	}
	
	public void setItem(int item){
		for ( int i = 0; i < itemLenght; i++){
			if(item ==i){
				cB[i] = true;
			}else{
				cB[i] = false;
			}
		}
	}

	void Update(){
		for ( int i = 0; i < itemLenght; i++){
			if(cB[i]){
				if(cT[i]<1)
					cT[i]+=0.1f;
			}else{
				if(cT[i]>0)
					cT[i]-=0.1f;
			}
		}

		for ( int i = 0; i < sprites.Length; i++){
			sprites[i].color = Color.Lerp(Color.white,Color.grey,cT[i]);
		}
	}
}
