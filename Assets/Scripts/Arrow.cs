using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    private Vector3 destination;
    private Vector3 direction;
    private Vector3 startPos;
    private bool destArr = false;
    [SerializeField]
    private float time = 1f; //this should be TIME it takes to get to destination so all arrows match on all screen ratios
    [SerializeField]
    private float speed;
    private Material alt;
	// Use this for initialization
	void Start () {
        startPos = transform.position;
	}
	
    public void setDestination(Vector3 dest)
    {
        destination = dest;
        direction = destination - transform.position;
        speed = direction.magnitude / time;

    }

    public void setRotation(Quaternion adj)
    {
        transform.rotation = Camera.main.transform.rotation * adj;
    }

    public void setDestArr(bool val)
    {
        destArr = val;
        if (val)
        {
            GetComponent<Renderer>().material = alt;
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
		if (!destArr)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
            if (transform.position == destination)
            {
                transform.position = startPos;
            }
            Debug.DrawLine(startPos, destination);
            Debug.DrawRay(startPos, direction, Color.green);
        }
    }
}
