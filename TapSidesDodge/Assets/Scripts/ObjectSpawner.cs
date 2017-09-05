using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {
	public GameObject player;
	public GameObject token;
	public GameObject bigToken;
	public GameObject cam;
	public GameObject[] trees;
	public GameObject bigTree;
	public GameObject[] debrees;
	public GameObject gapEmpty;
	public GameObject invincibilityToken;
	public GameObject heartToken;

	private float tokenTimer;
	private float treeTimer;
	private float bigTreeTimer;
	private float gapTreesTimer;
	private float bigTokenTimer;
	private float invincibilityTokenTimer;
	private float heartTokenTimer;

	public float slowness = 10f;

	public AudioClip bigTreeFalling;
	public AudioClip gapTreePop;
	public AudioClip gapTreeRumble;
	public AudioClip gapTreeSwoosh;

	void Start()
	{
		bigTreeTimer = Random.Range(25.0f, 45.0f);//(25.0f, 45.0f)
		gapTreesTimer = Random.Range(25.0f, 45.0f);
		treeTimer = Random.Range(2.0f, 5.0f);
		tokenTimer = Random.Range(0.5f, 0.8f);
		bigTokenTimer = Random.Range(12.0f,20.0f);
		invincibilityTokenTimer = Random.Range(30.0f,90.0f);
		heartTokenTimer = Random.Range(30.0f,60.0f);
	}

	void Update ()
	{

		if (GameInit.gameStarted == true) {
			tokenTimer -= Time.deltaTime;
			bigTokenTimer -= Time.deltaTime;
			treeTimer -= Time.deltaTime;
			bigTreeTimer -= Time.deltaTime;
			gapTreesTimer -= Time.deltaTime;
			invincibilityTokenTimer -= Time.deltaTime;
			heartTokenTimer -= Time.deltaTime;

			if(tokenTimer < 0){
				SpawnToken();
			}

			if(bigTokenTimer < 0){
				SpawnBigToken();
			}

			if(invincibilityTokenTimer < 0){
				SpawnInvincibilityToken();
			}

			if(heartTokenTimer < 0){
				SpawnHeartToken();
			}

			if(treeTimer < 0){
				SpawnTree();
			}

			if (bigTreeTimer < 0){
				StartCoroutine (SpawnBigTree ());	
			}

			if (gapTreesTimer < 0){
				StartCoroutine (SpawnGapTrees ());	
			}
		}
	}

	void SpawnToken()
	{
		GameObject tok = Instantiate(token, new Vector2(Random.Range(-6, 6), 6), Quaternion.identity) as GameObject;
		tokenTimer = Random.Range(0.5f, 0.8f);
	}

	void SpawnBigToken()
	{
		Instantiate (bigToken, new Vector2 (Random.Range (-6, 6), 6), Quaternion.identity);
		bigTokenTimer = Random.Range(12.0f,20.0f);
	}

	void SpawnInvincibilityToken()
	{
		Instantiate (invincibilityToken, new Vector2 (Random.Range (-6, 6), 6), Quaternion.identity);
		invincibilityTokenTimer = Random.Range(30.0f,90.0f);
	}

	void SpawnHeartToken()
	{
		Instantiate (heartToken, new Vector2 (Random.Range (-6, 6), 6), Quaternion.identity);

		if (PlayerCollide.tokens > 99) {
			heartTokenTimer = Random.Range (15.1f, 30.0f);
		} else {
			heartTokenTimer = Random.Range (30.0f, 60.0f);
		}


	}

	void SpawnTree()
	{
		GameObject tre = Instantiate(trees[Random.Range(0,trees.Length)], new Vector2(Random.Range(-6, 6), 10), Quaternion.identity) as GameObject;

		//CONTROL THE SPAWN SPEED
		if (PlayerCollide.tokens < 25) {
			//RANDOMIZE THE VELOCITY
			tre.GetComponent<Rigidbody2D>().gravityScale = Random.Range(1f, 2f);
			treeTimer = Random.Range(0.5f, 2.5f);
		}
		if (PlayerCollide.tokens > 24 && PlayerCollide.tokens < 50) {
			//RANDOMIZE THE VELOCITY
			tre.GetComponent<Rigidbody2D>().gravityScale = Random.Range(1f, 2.1f);
			treeTimer = Random.Range(0.3f, 2.0f);
		}
		if (PlayerCollide.tokens > 49 && PlayerCollide.tokens < 75)  {
			//RANDOMIZE THE VELOCITY
			tre.GetComponent<Rigidbody2D>().gravityScale = Random.Range(1f, 2.1f);
			treeTimer = Random.Range(0.2f, 1.5f);
		}
		if (PlayerCollide.tokens > 74 && PlayerCollide.tokens < 100)  {
			//RANDOMIZE THE VELOCITY
			tre.GetComponent<Rigidbody2D>().gravityScale = Random.Range(1.2f, 2.2f);
			treeTimer = Random.Range(0.2f, 1.0f);
		}
		if (PlayerCollide.tokens > 99 && PlayerCollide.tokens < 201)  {
			//RANDOMIZE THE VELOCITY
			tre.GetComponent<Rigidbody2D>().gravityScale = Random.Range(1.2f, 2.2f);
			treeTimer = Random.Range(0.1f, 0.8f);
		}
		if (PlayerCollide.tokens > 200 && PlayerCollide.tokens < 1000)  {
			//RANDOMIZE THE VELOCITY
			tre.GetComponent<Rigidbody2D>().gravityScale = Random.Range(1.2f, 2.3f);
			treeTimer = Random.Range(0.08f, 0.6f);
		}
		if (PlayerCollide.tokens > 999)  {
			//RANDOMIZE THE VELOCITY
			tre.GetComponent<Rigidbody2D>().gravityScale = Random.Range(1.3f, 2.4f);
			treeTimer = Random.Range(0.08f, 0.6f);
		}
	}

	IEnumerator SpawnBigTree()
	{
		bigTreeTimer = 1000;
		gapTreesTimer = 1000;
		Vector2 spawnSpot;

		int debreeTimer = Random.Range (20, 30);
		int spawnOnPlayerPer = Random.Range (0, 100);

		if (spawnOnPlayerPer < 50) 
		{
			spawnSpot = new Vector2 (Random.Range (-6, 6), 20);
		} 
		else 
		{
			spawnSpot = new Vector2 (player.transform.position.x, 20);
		}

		while (debreeTimer > 0) 
		{
			debreeTimer--;
			float t = Random.Range (0.005f, 0.1f);
			yield return new WaitForSeconds (t);
			GameObject debree = Instantiate (debrees [Random.Range (0, debrees.Length)], new Vector2 (spawnSpot.x + Random.Range (-1f, 1), spawnSpot.y - 10), Quaternion.identity) as GameObject;
		}

		//iTween.ShakePosition (cam, new Vector3 (0.08f, 0.08f, 0.08f), 3);
		yield return new WaitForSeconds (1);
		Instantiate (bigTree, spawnSpot, Quaternion.identity);
		player.GetComponent<AudioSource> ().PlayOneShot (bigTreeFalling, 7.0f);

		StartCoroutine (BigTreeSlowMo ());

		//CONTROL THE SPAWN SPEED BIG TREE
		if (PlayerCollide.tokens < 100) 
		{
			
			bigTreeTimer = Random.Range (25.0f, 45.0f);
		}
		if (PlayerCollide.tokens > 99 && PlayerCollide.tokens < 201)  {
		
			bigTreeTimer = Random.Range(24.0f,33.0f);
		}
		if (PlayerCollide.tokens > 200 && PlayerCollide.tokens < 401)  {
		
			bigTreeTimer = Random.Range(22.0f,30.0f);
		}
		if (PlayerCollide.tokens > 400 && PlayerCollide.tokens < 601)  {
		
			bigTreeTimer = Random.Range(18.0f,28.0f);
		}
		if (PlayerCollide.tokens > 600 && PlayerCollide.tokens < 801)  {
		
			bigTreeTimer = Random.Range(15.0f,25.0f);
		}
		if (PlayerCollide.tokens > 800 && PlayerCollide.tokens < 1001)  {
			bigTreeTimer = Random.Range(12.0f,20.0f);
		}
		if (PlayerCollide.tokens > 1000) {
			
			bigTreeTimer = Random.Range(9.0f,15.0f);
		}


		//CONTROL THE SPAWN SPEED GAPTREE
		if (PlayerCollide.tokens < 100) {
			gapTreesTimer = Random.Range(25.0f, 45.0f);		
		}
		if (PlayerCollide.tokens > 99 && PlayerCollide.tokens < 201)  {
			gapTreesTimer = Random.Range(24.0f,33.0f);//(24.0f,33.0f)
		}
		if (PlayerCollide.tokens > 200 && PlayerCollide.tokens < 401)  {
			gapTreesTimer = Random.Range(22.0f,30.0f);
		}
		if (PlayerCollide.tokens > 400 && PlayerCollide.tokens < 601)  {
			gapTreesTimer = Random.Range(18.0f,28.0f);
		}
		if (PlayerCollide.tokens > 600 && PlayerCollide.tokens < 801)  {
			gapTreesTimer = Random.Range(15.0f,25.0f);
		}
		if (PlayerCollide.tokens > 800 && PlayerCollide.tokens < 1001)  {
			gapTreesTimer = Random.Range(12.0f,20.0f);
		} 
		if (PlayerCollide.tokens > 1000) {
			gapTreesTimer = Random.Range(9.0f,15.0f);
		}
	}

	IEnumerator SpawnGapTrees()
	{
		
		//CHEAT WAY TO STOP THE OTHER FUNTIONS FROM ACTIVATING
		bigTreeTimer = 1000;
		treeTimer = 1000;
		tokenTimer = 1000;
		gapTreesTimer = 1000;

		int numTreeSets = Random.Range(1, 5);

		for(int v = 0; v < numTreeSets; v++)
		{
			
			yield return new WaitForSeconds(1);
			int xPos = -10;
			int GapXPos = Random.Range(-6, 6);
			GameObject gapTreeGroup = new GameObject();
			gapTreeGroup.name = "Gap Tree Group";
			gapTreeGroup.AddComponent<AudioSource> ();
			gapTreeGroup.GetComponent<AudioSource>().pitch = 0.5f;

			for(int i = 0; i < 20; i++)
			{
				GameObject tre = Instantiate(trees[Random.Range(0,trees.Length)], new Vector2(xPos, 5), Quaternion.identity) as GameObject;
				gapTreeGroup.GetComponent<AudioSource> ().PlayOneShot (gapTreePop, 1f);
				gapTreeGroup.GetComponent<AudioSource> ().pitch = gapTreeGroup.GetComponent<AudioSource> ().pitch + 0.1f;

				Destroy(tre.GetComponent<Rigidbody2D>());
				if(xPos != GapXPos){
					xPos++;

				}
				else{
					GameObject e = Instantiate (gapEmpty, new Vector2 (xPos + 1, 5), Quaternion.identity) as GameObject;
					xPos = xPos + 2;
					e.gameObject.transform.parent = gapTreeGroup.gameObject.transform;
				}
				tre.gameObject.transform.parent = gapTreeGroup.gameObject.transform;
				yield return new WaitForSeconds(0.03f);
			}

			//TIME FOR TREES TO FALL 
			float randomTime = Random.Range(0.5f, 2.0f);
			//iTween.ShakePosition (gapTreeGroup, new Vector2 (0.2f, 0.2f), randomTime);
			gapTreeGroup.GetComponent<AudioSource> ().pitch = 1.0f;
			gapTreeGroup.GetComponent<AudioSource> ().PlayOneShot (gapTreeRumble, 1f);
			yield return new WaitForSeconds (randomTime);

			gapTreeGroup.AddComponent<Rigidbody2D>();
			gapTreeGroup.GetComponent<Rigidbody2D>().gravityScale = 10;
			gapTreeGroup.GetComponent<AudioSource> ().PlayOneShot (gapTreeSwoosh, 1f);
			gapTreeGroup.gameObject.layer = 9;
			gapTreeGroup.gameObject.tag = "log";
			gapTreeGroup.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
			gapTreeGroup.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
			gapTreeGroup.AddComponent<AudioSource>();
		}

		treeTimer = Random.Range(2.0f, 5.0f);
		tokenTimer = Random.Range(0.5f, 0.8f);

		//CONTROL THE SPAWN SPEED
		if (PlayerCollide.tokens < 100) {
			gapTreesTimer = Random.Range(25.0f, 45.0f);		
		}
		if (PlayerCollide.tokens > 99 && PlayerCollide.tokens < 201)  {
			gapTreesTimer = Random.Range(24.0f,33.0f);//(24.0f,33.0f)
		}
		if (PlayerCollide.tokens > 200 && PlayerCollide.tokens < 401)  {
			gapTreesTimer = Random.Range(22.0f,30.0f);
		}
		if (PlayerCollide.tokens > 400 && PlayerCollide.tokens < 601)  {
			gapTreesTimer = Random.Range(18.0f,28.0f);
		}
		if (PlayerCollide.tokens > 600 && PlayerCollide.tokens < 801)  {
			gapTreesTimer = Random.Range(15.0f,25.0f);
		}
		if (PlayerCollide.tokens > 800 && PlayerCollide.tokens < 1001)  {
			gapTreesTimer = Random.Range(12.0f,20.0f);
		} 
		if (PlayerCollide.tokens > 1000) {
			gapTreesTimer = Random.Range(9.0f,15.0f);
		}

		//CONTROL THE SPAWN SPEED BIG TREE
		if (PlayerCollide.tokens < 100) {
			bigTreeTimer = Random.Range (25.0f, 45.0f);
		}
		if (PlayerCollide.tokens > 99 && PlayerCollide.tokens < 201)  {
			bigTreeTimer = Random.Range(24.0f,33.0f);
		}
		if (PlayerCollide.tokens > 200 && PlayerCollide.tokens < 401)  {
			bigTreeTimer = Random.Range(22.0f,30.0f);
		}
		if (PlayerCollide.tokens > 400 && PlayerCollide.tokens < 601)  {
			bigTreeTimer = Random.Range(18.0f,28.0f);
		}
		if (PlayerCollide.tokens > 600 && PlayerCollide.tokens < 801)  {
			bigTreeTimer = Random.Range(15.0f,25.0f);
		}
		if (PlayerCollide.tokens > 800 && PlayerCollide.tokens < 1001)  {
			bigTreeTimer = Random.Range(12.0f,20.0f);
		}
		if (PlayerCollide.tokens > 1000) {
			bigTreeTimer = Random.Range(9.0f,15.0f);
		}
  	}

	IEnumerator BigTreeSlowMo(){
		yield return new WaitForSeconds (0.50f);
		//SLOW DOWN TIME 
		Time.timeScale = 1 / slowness;
		Time.fixedDeltaTime = Time.fixedDeltaTime / slowness;

		yield return new WaitForSeconds(0.40f / slowness);

		Time.timeScale = 1f;
		Time.fixedDeltaTime = Time.fixedDeltaTime * slowness;
	}

}
