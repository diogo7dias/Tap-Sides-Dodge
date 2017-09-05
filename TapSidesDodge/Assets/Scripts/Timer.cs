using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour 
{

	public Text timerText;

	private float startTime;
	private bool finished = false;
	private bool started = false;

	// Use this for initialization
	void StartTimer () 
	{
		started = true;
		startTime = Time.deltaTime;
	}

	// Update is called once per frame
	void Update () 
	{

		if (finished) {
			return;
		}
		else if(!finished && started) {
			
			float t = Time.timeSinceLevelLoad- startTime;

			string minutes = ((int)t / 60).ToString ();
			string seconds = (t % 60).ToString ("f2");

			timerText.text = minutes + ":" + seconds;
		}
	}

	public void Finish()
	{
		finished = true;
	}

}
