using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenRotate : MonoBehaviour 
{

	private bool canRotate = false;

	void FixedUpdate () 
	{
		if (canRotate == true)
		{
			transform.Rotate (Vector2.up * Random.Range(100,250) * Time.deltaTime);
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		canRotate = true;
	}
}
