using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Door : MonoBehaviour {

	public float width, height; //NOT the scale, bc this would affect doors and windows scale
	//Add list of 
	//wall "start position" represented by the gameobject position
	//probably add options for features like doors and windows later

	private Wall wall;

	private MeshFilter mf;
	private Mesh mesh;
	
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		
	}

	void Start()
	{

	}

	void Update()
	{
		//is the door on the wall or free floating?
		wall = transform.GetComponentInParent<Wall>();
		if (wall != null) {
			ClampedUpdate();
		}
	}

	void ClampedUpdate() {
		width = Mathf.Clamp(width, 0, wall.width);
		height = Mathf.Clamp(height, 0, wall.height);

		float localX = Mathf.Clamp(transform.localPosition.x, 0, wall.width-width);

		transform.localRotation = Quaternion.identity;
		transform.localScale = Vector3.one;
		transform.localPosition = new Vector3(localX, 0, 0);
	}

	public void ClampedManualUpdate(float newWidth, float newHeight) {
		if (wall != null) {
			width = Mathf.Clamp(newWidth, 0, wall.width);
			height = Mathf.Clamp(newHeight, 0, wall.height);
		} else {
			width = newWidth;
			height = newHeight;
		}
	}
}
