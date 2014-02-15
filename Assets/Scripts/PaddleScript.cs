using UnityEngine;
using System.Collections;

public class PaddleScript : MonoBehaviour
{
	
	float paddleSpeed = 10f;
	public GameObject ballPrefab;
	GameObject attachedBall = null;
	
	int lives = 20;
	GUIText guiLives;
	
	int score = 0;
	
	public GUISkin ScoreBoardSkin;

	// Use this for initialization
	void Start (){
		DontDestroyOnLoad(gameObject);
		DontDestroyOnLoad(GameObject.Find("guiLives"));
		
		guiLives = GameObject.Find("guiLives").GetComponent<GUIText>();
		guiLives.text = "Lives: " + lives;
		
		SpawnBall();
	
	}
	
	public void OnLevelWasLoaded(int level){
		SpawnBall();
	}

	public void AddPoint(int v){
		score += v;	
	}
	
	public void LoseLife(){
		lives --;
		guiLives.text = "Lives: " + lives;
		if (lives > 0)
			SpawnBall();
		else{
			Destroy(gameObject);
			Application.LoadLevel("gameOver");
		}
	}
	
	//Spawn/Instantiate new ball
	public void SpawnBall ()
	{
		if (ballPrefab == null) {
			Debug.Log ("Add the damn ball idgit!");
			return;
		}
		
		attachedBall = (GameObject)Instantiate (ballPrefab, transform.position + new Vector3 (0, 0.75f, 0), Quaternion.identity);			
	}
	
	void OnGUI(){
		GUI.skin = ScoreBoardSkin;
		GUI.Label( new Rect(0,10,300,100), "Score: " + score);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Left-Right Motion
		transform.Translate (paddleSpeed * Time.deltaTime * Input.GetAxis ("Horizontal"), 0, 0);
		
		//Make paddle stay inside boundary
		if (transform.position.x > 7.4f) {
			transform.position = new Vector3(7.4f, transform.position.y, transform.position.z);
			}
		if (transform.position.x < -7.4f) {
			transform.position = new Vector3(-7.4f, transform.position.y, transform.position.z);
			}

		
		if (attachedBall) {
			Rigidbody ballRigidbody = attachedBall.rigidbody;
			ballRigidbody.position = transform.position + new Vector3 (0, 0.75f, 0);
			
			if (Input.GetButtonDown ("LaunchBall")) {
				//Fire the ball
				
				ballRigidbody.isKinematic = false;
				ballRigidbody.AddForce (300f * Input.GetAxis("Horizontal"), 300f, 0);
				attachedBall = null;	
			}
		}
	}
	
	void FixedUpdate(){
	}
	
	void LateUpdate(){
	}
	
	void OnCollisionEnter (Collision col)
	{
		foreach (ContactPoint contact in col.contacts) {
			if (contact.thisCollider == collider) {
				//This is the paddle's contact point
				float english = contact.point.x - transform.position.x;
				
				contact.otherCollider.rigidbody.AddForce (300f * english, 0, 0);
				
			}
		}
	}
}
