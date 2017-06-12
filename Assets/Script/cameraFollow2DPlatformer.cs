﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow2DPlatformer : MonoBehaviour {

    public Transform target; //what the camera is following;
    public float smoothing; //dampening effect

    Vector3 offset;//defference between the camera and the target

    float lowY; //lowest point camera goes

	// Use this for initialization
	void Start () {
        offset = transform.position - target.position; // - new Vector3(0,0.5f,0);

        lowY = transform.position.y-10;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 targetCamPos = target.position + offset;
        
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        if (transform.position.y < lowY)
            transform.position = new Vector3(transform.position.x, lowY, transform.position.z);
	}
}