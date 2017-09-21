using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    private Vector3 destination;
    private bool destHit = false;
    private Vector3 direction;
    private Vector3 startPos;
    private KeyCode arrowDir;
    private bool destArr = false;
    [SerializeField]
    private float time = 2f; //this should be TIME it takes to get to destination so all arrows match on all screen ratios
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
        direction = direction.normalized;

    }

    public void setRotation(Quaternion adj)
    {
        transform.rotation = Camera.main.transform.rotation * adj;
    }

    public void setDestAndDir(bool val, KeyCode dir)
    {
        arrowDir = dir;
        destArr = val;
        if (val)
        {
            GetComponent<Renderer>().material = alt;
        }
    }
    public void resetPosition()
    {
        transform.position = startPos;
        destHit = false;
    }

	// Update is called once per frame
	void FixedUpdate () {
		if (!destArr)
        {
            if (transform.position == destination || destHit)
            {
                destHit = true;
                transform.Translate(direction * Time.deltaTime * speed, Space.World);
                //transform.position = startPos;
            } else
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
            }
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (destArr && collider.tag == "Arrow")
        {
            if (Input.GetKeyDown(arrowDir))
            {
                Debug.Log(arrowDir);

                (collider.GetComponentInParent<Arrow>()).resetPosition();
            }
        }
    }
}
