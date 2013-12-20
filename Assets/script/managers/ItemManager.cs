using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {
	private float height;
	private float width;

	[SerializeField]
	private Camera itemCamera;

	private Vector3 topRight;

	private PlayerMove playerScript;

	private SpriteRenderer[] sprites;

	float[] cT;
	int spriteNum = -1;

	void Start () {
		playerScript = GameObject.Find("player").GetComponent<PlayerMove>() as PlayerMove;

		height = 2f * itemCamera.orthographicSize;
		width = height * itemCamera.aspect;
		topRight = new Vector3( width/2, height/2,10);

		sprites = new SpriteRenderer[playerScript.items.Length*10];
		cT = new float[playerScript.items.Length*10];
		for ( int i = 0; i < playerScript.items.Length*10; i++){
			cT[i] = Random.Range(0.0f,1.0f);
		}

		for ( int j = 0; j < 10; j++){
			for ( int i = 0; i < playerScript.items.Length; i++){
				GameObject button = new GameObject("itemIcon");
				spriteNum++;
				SpriteRenderer img = button.AddComponent<SpriteRenderer>();
				img.sprite = playerScript.items[i].GetComponent<Item>().iconS;
				sprites[spriteNum] = img;
				button.transform.parent = itemCamera.transform;

				button.transform.position = itemCamera.transform.position+topRight+new Vector3(-0.32f+i*-0.64f,-0.32f+j*-0.64f,0);
				button.layer = LayerMask.NameToLayer("UI");
				transform.Translate(  new Vector3(0,0.64f,0));
			}
		}
	}

	void Update(){
		for ( int i = 0; i < cT.Length; i++){
			cT[i]+=0.01f;
			if(cT[i]>1)
				cT[i] = 0;
		}

		for ( int i = 0; i < sprites.Length; i++){
			sprites[i].color = 
				Color.Lerp(Color.red,Color.blue,cT[i]);
		}
	}
}
