using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

	public GameObject player;
	public Transform canvasPause;
	public Transform canvasPauseButton;
	public AudioClip statsSound;

	public void Pause(){
		
		player.GetComponent<AudioSource> ().PlayOneShot (statsSound, 15.0f);

		if (canvasPause.gameObject.activeInHierarchy == false) {
			//canvasPauseButton.gameObject.SetActive (false);
			canvasPause.gameObject.SetActive (true);

			Time.timeScale = 0;
			AudioListener.volume = 0;
		} else {
			canvasPauseButton.gameObject.SetActive (true);
			canvasPause.gameObject.SetActive (false);
			Time.timeScale = 1;
			AudioListener.volume = 1;
		}
	} 

	public void PlayButton(){
		player.GetComponent<AudioSource> ().PlayOneShot (statsSound, 15.0f);
		canvasPauseButton.gameObject.SetActive (true);
		canvasPause.gameObject.SetActive (false);
		Time.timeScale = 1;
		AudioListener.volume = 1;
	}

	public void QuitButton(){
		player.GetComponent<AudioSource> ().PlayOneShot (statsSound, 15.0f);
		Application.Quit ();
	}
}
