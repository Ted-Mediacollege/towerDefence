using UnityEngine;
using System.Collections;

public enum itemType{
	Bullet,
	Tower
}

public class Item : MonoBehaviour {
	public itemType type {
		get { return _type;} 
		private set { _type = value;}
	}

	[SerializeField]
	private itemType _type;
}
