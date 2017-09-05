using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisappear : MonoBehaviour {

	public float lifeTimer = 7.0f;
	
	// Update is called once per frame
	void Update () {
		
		lifeTimer -= Time.deltaTime;

		if (lifeTimer < 0) {
			
			gameObject.GetComponent<SpriteRenderer> ().color = Color.Lerp (GetComponent<SpriteRenderer> ().color, new Color (1, 1, 1, 0), 3 * Time.deltaTime);

			if (GetComponent<SpriteRenderer> ().color.a < 0.1) {
				Destroy (gameObject);
			}
		}
	}
}
