using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCollide : MonoBehaviour {
	
	public GameObject cam;
	public GameObject tokenUI;
	public GameObject token;
	public GameObject canvasOverlay;
	public GameObject canvasOverlayText;
	public GameObject hearts;
	public GameObject timerScore;

	public static int health = 3;
	public static int tokens;

	public float slowness = 10f;

	public AudioClip[] collectToken;
	public AudioClip pain;
	public AudioClip[] explosionOfTokens;

	private bool canTakeDamage = true;
	private bool isInvin;

	void OnTriggerEnter2D(Collider2D trig)
	{
		switch (trig.gameObject.tag) {
		case "token":
			GetComponent<AudioSource> ().PlayOneShot (collectToken[Random.Range(0,collectToken.Length)], 2.0f);
			tokens++;
			DataManagement.datamanagement.totalCollectedTokens++;
			tokenUI.GetComponent<Text> ().text = (tokens.ToString ());
			Destroy (trig.gameObject);
			break;
		case "bigtoken":
			GetComponent<AudioSource> ().PlayOneShot (collectToken[Random.Range(0,collectToken.Length)], 1f);//MAKE DIFFERENT AUDIO 
			StartCoroutine (SpawnExplosionOfTokensAfterBigToken ());
			Destroy (trig.gameObject);
			break;
		case "log":
			TakeDamage ();
			break;
		case "biglog":
			TakeBigDamage ();
			break;
		case "gapempty":
			StartCoroutine (SpawnExplosionOfTokens ());
			break;
		case "invincibilitytoken":
			StartCoroutine (Invincibility ());
			Destroy (trig.gameObject);
			break;
		case "hearttoken":
			GainHeart ();
			Destroy (trig.gameObject);
			break;
		}
	}

	void TakeDamage()
	{
		if (canTakeDamage == true) {
			//SMALL PAUSE WHEN HIT
			StartCoroutine (SlowTimeDamage ());

			//iTween.ShakePosition (cam, new Vector3 (0.28f, 0.28f, 0.28f), 0.5f);
			//iTween.PunchPosition (gameObject, Vector3.down * 1.75f, 1.5f);
			GetComponent<AudioSource> ().PlayOneShot (pain, 10f);
			health--;

			if (health <= 0) {
				StartCoroutine (GameOver ());
			} else {
				StartCoroutine (CantGetHurt());
			}
		}
	}

	void TakeBigDamage()
	{
		if (canTakeDamage == true) {
			//iTween.ShakePosition (cam, new Vector3 (0.4f, 0.4f, 0.4f), 1);
			GetComponent<AudioSource> ().PlayOneShot (pain, 10f);//MAKE DIFFERENT AUDIO
			health -= 3;
			StartCoroutine (GameOver ());
		}
	}

	void GainHeart()
	{
		if (health < 3) {
			health++;
			//PLAY AUDIO
		}
	}

	public void ReloadScene()
	{
		SceneManager.LoadScene("Main");
	}

	IEnumerator SpawnExplosionOfTokens()
	{
		yield return new WaitForSeconds (0.3f);
		int explodingTokens = Random.Range(5,10);

		GetComponent<AudioSource> ().PlayOneShot (explosionOfTokens[Random.Range(0,explosionOfTokens.Length)], 1f);
		for (int i = 0; i < explodingTokens; i++) {

			GameObject t = Instantiate (token, new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y + 1), Quaternion.identity) as GameObject;
			t.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * Random.Range (8.0f, 12.0f), ForceMode2D.Impulse);
			int r = Random.Range (0, 2);
			if (r > 0) {
				t.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * Random.Range (4.5f, 5.0f), ForceMode2D.Impulse);
			} else {
				t.GetComponent<Rigidbody2D> ().AddForce (Vector2.left * Random.Range (4.5f, 5.0f), ForceMode2D.Impulse);
			}
		}

		yield return new WaitForSeconds (0.20f);
		//SLOW DOWN TIME 
		Time.timeScale = 1 / slowness;
		Time.fixedDeltaTime = Time.fixedDeltaTime / slowness;

		yield return new WaitForSeconds(1.1f / slowness);

		Time.timeScale = 1f;
		Time.fixedDeltaTime = Time.fixedDeltaTime * slowness;

		//yield return new WaitForSeconds (1f);
	}

	IEnumerator SlowTimeDamage()
	{
		Time.timeScale = 1 / 30f;
		Time.fixedDeltaTime = Time.fixedDeltaTime / 30f;

		yield return new WaitForSeconds(0.035f / slowness);

		Time.timeScale = 1f;
		Time.fixedDeltaTime = Time.fixedDeltaTime * slowness;
	}

	IEnumerator SpawnExplosionOfTokensAfterBigToken()
	{
		GetComponent<AudioSource> ().PlayOneShot (explosionOfTokens[Random.Range(0,explosionOfTokens.Length)], 1f);
		for (int i = 0; i < 15; i++) {

			GameObject t = Instantiate (token, new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y + 1), Quaternion.identity) as GameObject;
			t.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * Random.Range (8.0f, 12.0f), ForceMode2D.Impulse);

			int r = Random.Range (0, 2);

			if (r > 0) {
				t.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * Random.Range (4.5f, 5.0f), ForceMode2D.Impulse);//(3.0f, 5.0f)
			} else {
				t.GetComponent<Rigidbody2D> ().AddForce (Vector2.left * Random.Range (4.5f, 5.0f), ForceMode2D.Impulse);//(3.0f, 5.0f)
			}
		}

		yield return new WaitForSeconds (0.40f);
		//SLOW DOWN TIME 
		Time.timeScale = 1 / slowness;
		Time.fixedDeltaTime = Time.fixedDeltaTime / slowness;

		yield return new WaitForSeconds(1.4f / slowness);

		Time.timeScale = 1f;
		Time.fixedDeltaTime = Time.fixedDeltaTime * slowness;

	}

	IEnumerator Invincibility()
	{
		canTakeDamage = false;
		isInvin = true;
		gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);//gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 0.62f, 0.0f, 0.5f);- ORANGE         FF9F00FF - ORANGE
		gameObject.GetComponent<AudioSource> ().pitch = 1.2f;

		yield return new WaitForSeconds (12);

		for (int i = 0; i < 10; i++) {
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);//gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);- ORANGE         FF9F00FF - ORANGE
			yield return new WaitForSeconds (0.25f);
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);//gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);- ORANGE         FF9F00FF - ORANGE
			yield return new WaitForSeconds (0.25f);
		}

		gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);//gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);- ORANGE         FF9F00FF - ORANGE
		gameObject.GetComponent<AudioSource> ().pitch = 1.0f;
		canTakeDamage = true;
		isInvin = false;

	}

	IEnumerator CantGetHurt()
	{
		canTakeDamage = false;
		for (int i = 0; i < 10; i++) {
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);//gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);- ORANGE         FF9F00FF - ORANGE
			yield return new WaitForSeconds (0.10f);

			gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);//gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);- ORANGE         FF9F00FF - ORANGE
			yield return new WaitForSeconds (0.10f);
		}

		if (isInvin == false) {
			canTakeDamage = true;
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);//gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);- ORANGE         FF9F00FF - ORANGE
			gameObject.GetComponent<AudioSource> ().pitch = 1.0f;
		}
	}

	IEnumerator GameOver()
	{


		if (tokens > DataManagement.datamanagement.tokensHighscore) {
			DataManagement.datamanagement.tokensHighscore = tokens;
		}

		DataManagement.datamanagement.bestTime = GameObject.Find ("Player").GetComponent<Timer> ().timerText.text;
		DataManagement.datamanagement.SaveData ();

		canvasOverlay.SetActive (true);
		canvasOverlayText.GetComponent<Text> ().text = tokens.ToString();

		timerScore.GetComponent<Text> ().text = GameObject.Find ("Player").GetComponent<Timer> ().timerText.text;

		hearts.SetActive (false);
		gameObject.GetComponent<PlayerMovement> ().enabled = false;
		gameObject.GetComponent<Collider2D> ().enabled = false;

		GameObject.Find ("Player").SendMessage ("Finish");

		yield return new WaitForSeconds (1f);
		//STOP TIME
		Time.timeScale = 0;

	}	
}

