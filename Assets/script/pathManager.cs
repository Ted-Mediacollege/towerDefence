using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class pathManager : MonoBehaviour 
{
	private GameObject[] paths;

	void Start () {
		int childscount = this.gameObject.transform.childCount;
		paths = new GameObject[childscount];
		
		for(int i = 0; i < childscount; i++) {
			Transform pathTransf = this.gameObject.transform.GetChild(i);
			GameObject path = pathTransf.gameObject;
			paths[i] = path;
		}
	}
	
	Vector3[] getClosestPath(Vector3 entitypos) {
		return null;
	}
}
