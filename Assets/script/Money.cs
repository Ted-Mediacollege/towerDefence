using UnityEngine;
using System.Collections;

public class Money : MonoBehaviour 
{
	private int amount = 1;
	private GameObject p;
	private GameManager gameMngr;

	void Start () {
		gameMngr = GameObject.Find("gameManager").GetComponent<GameManager>() as GameManager;
		p = GameObject.Find("player");
	}

	void Update () {
		float dis = Vector3.Distance(p.transform.position, transform.position); 
		if(dis < 0.5F) {
			pickUp();
		} else if(dis < 8) {
			Vector3 newpos = transform.position;
			Vector2 change = movement.ForceAndAngleToDirection(0.1F / dis, movement.angleToPoint(transform, p.transform.position));

			newpos.x += change.x;
			newpos.y += change.y;

			transform.position = newpos;
		}
	}

	void pickUp() {
		gameMngr.ChangeMoney(10);
		GameObject.Destroy(gameObject);
	}
}
