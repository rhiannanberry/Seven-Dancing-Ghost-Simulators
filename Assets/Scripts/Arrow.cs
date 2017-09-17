﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    private Vector3 destination;
    private Vector3 direction;
    private Vector3 startPos;
    private bool destArr = false;
    [SerializeField]
    private float speed = 1f;
	// Use this for initialization
	void Start () {
        startPos = transform.position;
        
        //controller = GetComponent<CharacterController>();
	}
	
    public void setDestination(Vector3 dest)
    {
        destination = dest;
        direction = destination - transform.position;
    }

    public void setRotation(Quaternion adj)
    {
        transform.rotation = Camera.main.transform.rotation * adj;
        //transform.rotation = transform.rotation * adj;
    }

    public void setDestArr(bool val)
    {
        destArr = val;
    }

	// Update is called once per frame
	void FixedUpdate () {
		if (!destArr)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
            Debug.DrawLine(startPos, destination);
            Debug.DrawRay(startPos, direction, Color.green);

            //controller.SimpleMove(destination * speed);
        }
    }
}
