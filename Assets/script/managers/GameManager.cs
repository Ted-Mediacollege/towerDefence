using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	[SerializeField]
	private int livePoints = 10;
	
	[SerializeField]
	private int money = 25;
	
	[SerializeField]
	private TextMesh livePointDisplay;
	[SerializeField]
	private TextMesh moneyDisplay;
	
	private void Start(){
		livePointDisplay.text = "life: "+livePoints.ToString();
		moneyDisplay.text = "money: "+money.ToString();
	}
	
	public void ChangeLife ( int c) {
		livePoints+=c;
		if(livePoints<0){
			livePointDisplay.text = "life: "+livePoints.ToString()+" gameOver";
		}else{
			livePointDisplay.text = "life: "+livePoints.ToString();
		}
	}
	
	public void ChangeMoney ( int c) {
		money+=c;
		moneyDisplay.text = "money: "+money.ToString();
	}
	
	public bool CheckMoney ( int c) {
		if(money>=c){
			return true;
		}else{
			return false;
		}
	}
	
}
