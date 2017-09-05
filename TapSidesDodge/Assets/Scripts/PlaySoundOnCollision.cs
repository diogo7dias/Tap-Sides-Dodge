using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour 
{

	public AudioClip[] collision;

	void OnCollisionEnter2D(Collision2D col)
	{

		if (col.gameObject.tag == "floor") 
		{
			//CONTINUOUS JUMP
			//gameObject.GetComponent<Rigidbody2D> ().AddForce (transform.up * 7,ForceMode2D.Impulse);
			GetComponent<AudioSource> ().PlayOneShot (collision[Random.Range(0,collision.Length)], 0.5f);
		}
	}
}
