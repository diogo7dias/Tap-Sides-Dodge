using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInit : MonoBehaviour 
{

	public static bool gameStarted;

	public GameObject player;
	public GameObject cam;
	public GameObject MenuBottomText;
	public GameObject PlayButton;
	//public GameObject splashScreen;

	public Transform canvasPauseButton;
	public Transform canvasPause;
	public Transform canVas;

	public AudioClip statsSound;
	public AudioClip woosh;

	private int ShowScore;
	//public static float ssTimer = 0.25f;

	void Awake ()
	{
		//DataManagement.datamanagement.LoadData ();
		//splashScreen.SetActive(true);
		Application.targetFrameRate = 60;
		PlayerCollide.health = 3;
		PlayerCollide.tokens = 0;
		Time.timeScale = 1.0f;
		gameStarted = false;
	}

	void Start(){
		DataManagement.datamanagement.LoadData ();
		MenuBottomText.GetComponent<Text> ().text = "Highscore = " + DataManagement.datamanagement.tokensHighscore.ToString();
	}

	void FixedUpdate () {
		///
		//ssTimer -= Time.deltaTime;
		//if (ssTimer < 0) {
		//	splashScreen.SetActive (false);
		//}
		///


		if (gameStarted == false) 
		{
			cam.gameObject.transform.position = new Vector3 (2.15f, -1.98f, -10);
			cam.GetComponent<Camera> ().orthographicSize = 2;
		} 
		else {
			StartCoroutine (StartGame ());

			cam.gameObject.transform.position = Vector3.Lerp (cam.gameObject.transform.position, new Vector3 (0, 0, -10), 2 * Time.deltaTime);
			cam.GetComponent<Camera> ().orthographicSize = Mathf.Lerp (cam.GetComponent<Camera> ().orthographicSize, 5, 5 * Time.deltaTime);
		}

		//DEACTIVATE PAUSE BUTTON IF PAUSE MENU IS SHOWING
		if (canvasPause.gameObject.activeInHierarchy == true) {
			canvasPauseButton.gameObject.SetActive (false);
		}
	}

	public void PlayButtonPressed(){
		player.GetComponent<AudioSource> ().PlayOneShot (woosh, 25.0f);
		player.GetComponent<AudioSource> ().PlayOneShot (statsSound, 15.0f);

		//DISABLE THE MAIN PANEL WHEN GAME STARTS
		GameObject goAwayPanel = canVas.transform.Find ("Panel").gameObject;
		goAwayPanel.SetActive (false);

		gameStarted = true;
	}

	public void SwapBottomMenu()
	{
		player.GetComponent<AudioSource> ().PlayOneShot (statsSound, 15.0f);
		if (ShowScore == 0) {
			ShowScore = 1;
		} 
		else if (ShowScore == 1) {
			ShowScore = 2;
		} 
		else if (ShowScore == 2) {
			ShowScore = 0;
		}

		switch (ShowScore) 
		{
		case 0:
			MenuBottomText.GetComponent<Text> ().text = "Highscore = " + DataManagement.datamanagement.tokensHighscore.ToString ();
			break;
		case 1:
			MenuBottomText.GetComponent<Text> ().text = "Total Collected = " + DataManagement.datamanagement.totalCollectedTokens.ToString ();
			break;
		case 2:
			MenuBottomText.GetComponent<Text> ().text = "Previous Time = " + DataManagement.datamanagement.bestTime.ToString ();
			break;
		}
	}
		
	IEnumerator StartGame()
	{
		
		MenuBottomText.SetActive (false);
		PlayButton.SetActive (false);
		GameObject.Find ("Player").SendMessage ("StartTimer");
		//yield return new WaitForSeconds (0.02f);

		yield return new WaitForSeconds (1.4f);

		canvasPauseButton.gameObject.SetActive (true);
	}


}
