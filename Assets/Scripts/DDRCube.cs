﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDRCube : MonoBehaviour {
    private Camera cam;
    private Vector3 startUp, startLeft, startDown, startRight;
    public Arrow arrow;
    public Vector3 destination;
    // Use this for initialization
    void Start () {
        cam = Camera.main;
        Quaternion rotUp = Quaternion.AngleAxis(180f, Vector3.forward);
        Quaternion rotDown = Quaternion.AngleAxis(0f, Vector3.forward);
        Quaternion rotRight = Quaternion.AngleAxis(-90f, Vector3.forward);
        Quaternion rotLeft = Quaternion.AngleAxis(90f, Vector3.forward);
        startRight = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, 0.5f));
        startLeft = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, 0.5f));
        startDown = cam.ViewportToWorldPoint(new Vector3(0.5f, 0, 0.5f));
        startUp = cam.ViewportToWorldPoint(new Vector3(0.5f, 1, 0.5f));

        Arrow fRight = Instantiate<Arrow>(arrow);
        Arrow fLeft = Instantiate<Arrow>(arrow);
        Arrow fUp = Instantiate<Arrow>(arrow);
        Arrow fDown = Instantiate<Arrow>(arrow);
        fRight.setDestArr(true);
        fLeft.setDestArr(true);
        fUp.setDestArr(true);
        fDown.setDestArr(true);
        fRight.transform.position = destination;
        fLeft.transform.position = destination;
        fUp.transform.position = destination;
        fDown.transform.position = destination;
        fRight.setRotation(rotRight);
        fLeft.setRotation(rotLeft);
        fUp.setRotation(rotUp);
        fDown.setRotation(rotDown);

        //cube1.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        Arrow arrowRight = Instantiate<Arrow>(arrow);

        arrowRight.transform.position = startRight;
        arrowRight.setRotation(rotRight);

        Arrow arrowDown = Instantiate<Arrow>(arrow);
        arrowDown.transform.position = startDown;
        arrowDown.setRotation(rotDown);

        Arrow arrowLeft = Instantiate<Arrow>(arrow);
        arrowLeft.transform.position = startLeft;
        arrowLeft.setRotation(rotLeft);

        Arrow arrowUp = Instantiate<Arrow>(arrow);
        arrowUp.transform.position = startUp;
        arrowUp.setRotation(rotUp);

        //Arrow arrowDown = Instantiate<Arrow>(arrow);
        //arrowDown.transform.position = startDown;
        arrowRight.setDestination(destination);
        arrowLeft.setDestination(destination);
        arrowDown.setDestination(destination);
        arrowUp.setDestination(destination);
    }
	
    public void setDestination(Vector3 dest)
    {
        destination = dest;
    }
	// Update is called once per frame
	void Update () {
		if (destination != null)
        {
            Debug.DrawLine(cam.transform.position, destination);
        }
	}
}
