﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

	private Vector2 velocity;

	public float delayX;
	public float delayY;

	public Transform player;

	public bool bounds;
	public bool follow = true;
	public Vector3 minCameraPos;
	public Vector3 maxCameraPos;

	void Start() {
		SceneManagerScript sceneManager = GameObject.Find("/SceneManager").GetComponent<SceneManagerScript>();
		sceneManager.camera = this;

		if (sceneManager.checkpointBoss) {
			transform.position = new Vector3(sceneManager.checkpointCoordBoss.x, sceneManager.checkpointCoordBoss.y, -5);
        } else if (sceneManager.checkpoint) {
			transform.position = new Vector3(sceneManager.checkpointCoord.x, sceneManager.checkpointCoord.y, -5);
        } else {
			transform.position = new Vector3(sceneManager.startCoord.x, sceneManager.startCoord.y, -5);
        }
	}
	
	// Update is called once per frame
	void Update () {

		if (follow) {
			float posX = Mathf.SmoothDamp (transform.position.x, player.position.x, ref velocity.x, delayX);
			float posY = Mathf.SmoothDamp (transform.position.y, player.position.y, ref velocity.y, delayY);

			transform.position = new Vector3 (posX, posY, transform.position.z);
		}

		if (bounds) {
			float x = Mathf.Clamp (transform.position.x, minCameraPos.x, maxCameraPos.x);
			float y = Mathf.Clamp (transform.position.y, minCameraPos.y, maxCameraPos.y);
			float z = Mathf.Clamp (transform.position.z, minCameraPos.z, maxCameraPos.z);
			transform.position = new Vector3 (x, y, z);
		}
	}
}
