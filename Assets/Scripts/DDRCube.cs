using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDRCube : MonoBehaviour {
    private Camera cam;
    private Vector3 startUp, startLeft, startDown, startRight;
    private Quaternion rotUp, rotDown, rotLeft, rotRight;
    public Arrow arrow;
    public Vector3 destination;
    // Use this for initialization
    void Start () {
        cam = Camera.main;
        rotUp = Quaternion.AngleAxis(180f, Vector3.forward);
        rotDown = Quaternion.AngleAxis(0f, Vector3.forward);
        rotLeft = Quaternion.AngleAxis(-90f, Vector3.forward);
        rotRight = Quaternion.AngleAxis(90f, Vector3.forward);
        startLeft = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, 0.5f));
        startRight = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, 0.5f));
        startDown = cam.ViewportToWorldPoint(new Vector3(0.5f, 0, 0.5f));
        startUp = cam.ViewportToWorldPoint(new Vector3(0.5f, 1, 0.5f));

        Arrow fRight = Instantiate<Arrow>(arrow);
        Arrow fLeft = Instantiate<Arrow>(arrow);
        Arrow fUp = Instantiate<Arrow>(arrow);
        Arrow fDown = Instantiate<Arrow>(arrow);
        fRight.setDestAndDir(true, KeyCode.D);
        fLeft.setDestAndDir(true, KeyCode.A);
        fUp.setDestAndDir(true, KeyCode.W);
        fDown.setDestAndDir(true, KeyCode.S);
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
	void Update () {//when evalcuating distance just use sqrmag to save performance
    }
}
