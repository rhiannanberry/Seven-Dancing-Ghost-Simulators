using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowflake : MonoBehaviour {

	Camera sourceCam;

	// Use this for initialization
	void Start () {
		sourceCam = transform.parent.GetComponent<Camera>();
		sourceCam.aspect = 1;
		float sideLenPixels = sourceCam.scaledPixelWidth;
		Vector3 start = sourceCam.ScreenToWorldPoint(new Vector2(0,0));
		Vector3 end = sourceCam.ScreenToWorldPoint(new Vector2(sideLenPixels, 0));
		float worldSize = Vector3.Distance(start, end)/4;
		//transform.SetParent(null);
		transform.localScale = Vector3.one * worldSize;
		//transform.SetParent(sourceCam.transform);
		Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane+1.5f));
		transform.position = pos;//new Vector3(transform.position.x, transform.position.y, pos.z);
		transform.localPosition = new Vector3(-1, 1, transform.localPosition.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
