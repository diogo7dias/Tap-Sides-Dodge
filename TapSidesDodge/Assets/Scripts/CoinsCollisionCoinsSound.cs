using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsCollisionCoinsSound : MonoBehaviour 
{
	public AudioClip[] coinCollisionCoin;

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "token")
		{
			GetComponent<AudioSource> ().PlayOneShot (coinCollisionCoin[Random.Range(0,coinCollisionCoin.Length)], 1.25f);
		}
	}
}
