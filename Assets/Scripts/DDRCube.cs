using System.Collections;
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
        Quaternion rotRight = Quaternion.AngleAxis(90f, Vector3.forward);
        Quaternion rotLeft = Quaternion.AngleAxis(-90f, Vector3.forward);
        startRight = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, 0.5f));
        startLeft = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, 0.5f));
        startUp = cam.ViewportToWorldPoint(new Vector3(0.5f, 0, 0.5f));
        startDown = cam.ViewportToWorldPoint(new Vector3(0.5f, 1, 0.5f));


        //cube1.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        Arrow arrowRight = Instantiate<Arrow>(arrow);
        arrowRight.transform.position = startRight;
        arrowRight.transform.rotation = rotRight * arrowRight.transform.rotation;

        Arrow arrowUp = Instantiate<Arrow>(arrow);
        arrowUp.transform.position = startUp;
        arrowUp.transform.rotation = rotUp * arrowUp.transform.rotation;

        Arrow arrowLeft = Instantiate<Arrow>(arrow);
        arrowLeft.transform.position = startLeft;
        arrowLeft.transform.rotation = rotLeft * arrowLeft.transform.rotation;

        Arrow arrowDown = Instantiate<Arrow>(arrow);
        arrowDown.transform.position = startDown;
        arrowRight.setDestination(destination);
        arrowLeft.setDestination(destination);
        arrowUp.setDestination(destination);
        arrowDown.setDestination(destination);
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
