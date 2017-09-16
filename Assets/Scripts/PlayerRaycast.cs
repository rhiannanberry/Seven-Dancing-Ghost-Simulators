using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour {
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float scale = 2f;
    public bool lockCursor = false;
    public DDRCube ddrCube;
    // Use this for initialization	
	// Update is called once per frame
    void Start ()
    {

    }

	void FixedUpdate () {
		if (Input.GetMouseButtonDown(0))
        {
            lockCursor = DetectHit();
        }
	}

    private bool DetectHit()
    {
        RaycastHit hit;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            DDRCube dCopy = Instantiate<DDRCube>(ddrCube);
            dCopy.setDestination(hit.point);
            return true;
        }
        return false;
    }
}
