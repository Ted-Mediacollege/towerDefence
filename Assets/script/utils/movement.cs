using UnityEngine;
using System.Collections;

static class movement {

	static public float RotateForce(Transform selfTrans, 
	                                Vector3 target,
	                                float maxRotateForce,
	                                float forceMultiply){
		float thisRotation =  selfTrans.eulerAngles.z;
		float deltaY = selfTrans.position.y - target.y;
		float deltaX = selfTrans.position.x - target.x;
		float rotationGoal = (Mathf.Atan2(deltaY,deltaX) * 180 / Mathf.PI)+90;
		float deltaAngel = thisRotation - rotationGoal;
		if(deltaAngel>180){
			deltaAngel -=360;
		}
		
		if(deltaAngel<-180){
			deltaAngel +=360;
		}
		float force = deltaAngel * forceMultiply;
		if(force>maxRotateForce){
			force = maxRotateForce;
		}else if(force<-maxRotateForce){
			force = -maxRotateForce;
		}else{
		}
		return -force;
	}

	static public float limitTorque(float torque, float limit){
		if(torque > limit){
			torque = limit;
		}else if(torque < -limit){
			torque = -limit;
		}
		return torque;
	}

	static private float angleToPoint(Transform selfTrans, Vector3 target){
		float thisRotation =  selfTrans.eulerAngles.z;
		float deltaY = selfTrans.position.y - target.y;
		float deltaX = selfTrans.position.x - target.x;
		float rotationGoal = (Mathf.Atan2(deltaY,deltaX) * 180 / Mathf.PI)+90;
		return rotationGoal;
	}

	static public Quaternion RotateToPoint( Transform selfTrans, Vector3 point ){
		return  Quaternion.Euler(new Vector3(0, 0, movement.angleToPoint(selfTrans,point)));
	}
}
