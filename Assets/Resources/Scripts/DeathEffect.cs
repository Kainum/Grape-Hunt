using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour {

	private float timeCount;

	// Use this for initialization
	void Start () {
		timeCount = 0.6f;
	}
	
	// Update is called once per frame
	void Update () {
		if (timeCount > 0) {
			timeCount -= Time.deltaTime;
		} else {
			Destroy (gameObject);
		}
	}
}
