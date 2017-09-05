using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
	private int xPos;
	public bool isTouch;
	private float t = 0.1f;
	public AudioClip[] step;

	void FixedUpdate (){
		if (isTouch == true) {
			//ANDROID
			if (Input.touchCount > 0 && GameInit.gameStarted == true) {
				
				if (Input.GetTouch (0).position.x > Screen.width / 2 && xPos < 5.8 && t > 0.075) {
					if (Input.GetTouch (0).phase == TouchPhase.Stationary) {
						xPos++;
						t = 0.0f;
						GetComponent<AudioSource> ().PlayOneShot (step[Random.Range(0,step.Length)], 15.0f);
					}
				}

				if (Input.GetTouch (0).position.x < Screen.width / 2 && xPos > -5.8 && t > 0.075) {
					if (Input.GetTouch (0).phase == TouchPhase.Stationary) {
						xPos--;
						t = 0.0f;
						GetComponent<AudioSource> ().PlayOneShot (step[Random.Range(0,step.Length)], 15.0f);					
					}
					//To flip side -> transform.localScale = new Vector2 (-x,y);
				}
				t += Time.deltaTime;
			}
		} else {
			//KEYBOARD
			if (Input.GetButton ("Horizontal") && GameInit.gameStarted == true) {
				
				if (Input.GetAxis ("Horizontal") > 0 && xPos < 5.8 && t > 0.075) {
					xPos++;
					t = 0.0f;
					GetComponent<AudioSource> ().PlayOneShot (step[Random.Range(0,step.Length)], 15.0f);
				}

				if (Input.GetAxis ("Horizontal") < 0 && xPos > -5.8 && t > 0.075) {
					xPos--;
					t = 0.0f;
					GetComponent<AudioSource> ().PlayOneShot (step[Random.Range(0,step.Length)], 15.0f);		
				}
				t += Time.deltaTime;
			}
		}
		MovePlayer ();
	}

	void MovePlayer(){
		Vector2 playerPos = gameObject.transform.position;
		playerPos.x = xPos;
		gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, playerPos, 10 * Time.deltaTime);
	}
}
