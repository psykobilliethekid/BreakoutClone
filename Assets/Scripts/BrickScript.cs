using UnityEngine;
using System.Collections;

public class BrickScript : MonoBehaviour {
	
	static int numBricks = 0;
	public int pointValue = 1; 
	public int hitPoints = 1;
	public int level;

	// Use this for initialization
	void Start () {
		numBricks++;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter (Collision col) {
		hitPoints--;
		
		if(hitPoints <= 0){
			Die();
		}
	}
	
	void Die(){
		Destroy(gameObject);
		PaddleScript paddleScript = GameObject.Find("Paddle").GetComponent<PaddleScript>();
		paddleScript.AddPoint(pointValue);
		numBricks--;
		Debug.Log (numBricks);
		
		 if(numBricks <= 0)
		{
			if(level == 1) 
			{
			Application.LoadLevel("level2");
			}
			
			else if(level == 2) 
			{
			Application.LoadLevel("level3");
			}
			
			else if(level == 3) 
			{
			Application.LoadLevel("level4");
			}
			
			else if(level == 4) 
			{
			Application.LoadLevel("level5");
			}
			
			else 
			{
			Application.LoadLevel("youWin");
			}
		}
	}
}
