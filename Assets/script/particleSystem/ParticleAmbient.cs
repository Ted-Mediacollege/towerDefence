using UnityEngine;
using System.Collections;

public class ParticleAmbient : MonoBehaviour {

	[SerializeField]
	private GameObject[] particles;

	private GameObject bubbleHolder;
	private float spawn;

	void Start () {
		spawn = 1F;
	}

	void Update () {
		spawn -= Time.deltaTime;

		if(spawn < 0F)
		{
			spawnParticle();
			spawn = 1F;
		}
	}

	void spawnParticle() {
		GameObject buble = GameObject.Instantiate(particles[0], transform.position, new Quaternion(0,0,45,0)) as GameObject;
	}
	
	/*
	void spawnParticle() {
		Quaternion partRotation = new Quaternion();
		partRotation.eulerAngles = new Vector3(0,0,45);
		GameObject buble = GameObject.Instantiate(particles[0],transform.position,partRotation) as GameObject;
		buble.transform.parent = bubbleHolder.transform;


		float angle;
		Quaternion partRotation = new Quaternion();
		Vector3 partSpawn = spawnPoint.position;
		partSpawn.y += Random.Range(-(spawnDisplacement/2),(spawnDisplacement/2));
		angle = -45;
		partRotation.eulerAngles = new Vector3(0,0,angle);
		GameObject buble = GameObject.Instantiate(particles[0],partSpawn,partRotation) as GameObject;
		buble.transform.parent = bubbleHolder.transform;
	}*/
}
