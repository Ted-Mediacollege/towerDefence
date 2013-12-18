using UnityEngine;
using System.Collections;

public class TowerMachineGun : Tower {


	
	//top.LookAt(enemyPos);

	public override void Rotate(){
		if(enemyMngr.enemies.Count>0){
			if(distCurrentTarget < agroDistance && targetFound){
				float bulSpeed = bullet.GetComponent<Bullet>().speed;
				float bulletSpeed = bulSpeed;
				float enemySpeed = currentTarget.transform.rigidbody2D.velocity.magnitude / 50; //enemy speed per fixed update
				Vector2 enemyDirection = movement.AngleToDirection( currentTarget.transform.eulerAngles.z);
				Vector2 towerPos = new Vector2(transform.position.x,transform.position.y);
				float distance = Vector2.Distance(currentTargetPos,towerPos);
				float tBullet = distance / bulletSpeed;
				float sEnemy = enemySpeed * tBullet;
				
				Vector2 estimatedTargetPosPos = new Vector2(currentTargetPos.x + enemyDirection.x*sEnemy
				                                            ,currentTargetPos.y + enemyDirection.y*sEnemy);
				currentTargetPos = estimatedTargetPosPos;

				gun.rigidbody2D.AddTorque(movement.RotateForce(gun.transform.position,
				                                               gun.transform.eulerAngles.z,
				                                               currentTargetPos,
				                                               maxRotForce,
				                                               10));
			}
		}
	}
}
