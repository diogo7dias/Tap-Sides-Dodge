using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour {

	public GameObject[] hearts;
	public Sprite heart;
	public Sprite emptyHeart;
	
	void Update () {
		
		if (PlayerCollide.health == 3) {
			hearts [0].GetComponent<SpriteRenderer> ().sprite = heart;
			hearts [1].GetComponent<SpriteRenderer> ().sprite = heart;
			hearts [2].GetComponent<SpriteRenderer> ().sprite = heart;
		}
		if (PlayerCollide.health == 2) {
			hearts [0].GetComponent<SpriteRenderer> ().sprite = heart;
			hearts [1].GetComponent<SpriteRenderer> ().sprite = heart;
			hearts [2].GetComponent<SpriteRenderer> ().sprite = emptyHeart;
		}
		if (PlayerCollide.health == 1) {
			hearts [0].GetComponent<SpriteRenderer> ().sprite = heart;
			hearts [1].GetComponent<SpriteRenderer> ().sprite = emptyHeart;
			hearts [2].GetComponent<SpriteRenderer> ().sprite = emptyHeart;
		}
		if (PlayerCollide.health < 1) {
			hearts [0].GetComponent<SpriteRenderer> ().sprite = emptyHeart;
			hearts [1].GetComponent<SpriteRenderer> ().sprite = emptyHeart;
			hearts [2].GetComponent<SpriteRenderer> ().sprite = emptyHeart;
		}
	}
}
