using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    private Vector3 destination;
    private Vector3 direction;
    [SerializeField]
    private float speed = 1f;
	// Use this for initialization
	void Start () {
        //controller = GetComponent<CharacterController>();
	}
	
    public void setDestination(Vector3 dest)
    {
        destination = dest;
        direction = -1*destination.normalized;
    }

	// Update is called once per frame
	void FixedUpdate () {
		if (destination != null)
        {
            transform.Translate(direction * Time.deltaTime * speed);
            //controller.SimpleMove(destination * speed);
        }
	}
}
