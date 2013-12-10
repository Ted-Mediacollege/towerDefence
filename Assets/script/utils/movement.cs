using UnityEngine;
using System.Collections;

static class movement {

	static public int add(int a,int b){
		return a+b;
	}

	static public float RotateForce(Transform selfTrans, Vector3 target,float maxRotateForce , float angularVelocity){
		float returnValue;
		float thisRotation =  selfTrans.eulerAngles.z;
		float deltaY = selfTrans.position.y - target.y;
		float deltaX = selfTrans.position.x - target.x;
		float rotationGoal = (Mathf.Atan2(deltaY,deltaX) * 180 / Mathf.PI)+90;
		//transform.rotation =  Quaternion.Euler(new Vector3(0, 0, angleInDegrees));
		float deltaAngel = thisRotation - rotationGoal;
		//Debug.Log(thisRotation +"-"+rotationGoal+"="+deltaAngel);
		if(deltaAngel>360){
			deltaAngel -=360;
		}
		
		if(deltaAngel<0){
			deltaAngel +=360;
		}
		if(deltaAngel>maxRotateForce){
			deltaAngel = maxRotateForce;
		}else if(deltaAngel<maxRotateForce){
			deltaAngel = -maxRotateForce;
		}
		//Debug.Log(deltaAngel);
		//Debug.Log(path[ProgressInPath]);
		//get input
		//add force
		//rigidbody2D.AddTorque(-deltaAngel);
		//limit force
		return deltaAngel;
	}
}
