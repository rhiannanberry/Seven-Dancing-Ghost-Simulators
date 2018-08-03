using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Wall : MonoBehaviour {
	public float width, height; //NOT the scale, bc this would affect doors and windows scale
	//Add list of 
	//wall "start position" represented by the gameobject position
	//probably add options for features like doors and windows later

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
		if (mf == null) {
			mf = GetComponent<MeshFilter>();
			Debug.Log("TEST");
		}

		if (mf.sharedMesh == null) {
			mesh = new Mesh();
			mf.mesh = mesh;
			GenerateNewMesh();
		} else {
			mesh = mf.sharedMesh;
		}
		UpdateVertices();
		UpdateTris();
		UpdateNormals();
	}

	private void GenerateNewMesh() {
		Debug.Log("GEN MESH");
		Vector3[] verts = new Vector3[4];
		verts[0] = transform.position;
		verts[1] = new Vector3(width, transform.position.y, 0);
		verts[2] = new Vector3(transform.position.x, height, 0);
		verts[3] = new Vector3(width, height);

		mesh.vertices = verts;

		int[] tris = new int[6];
		tris[0] = 0;
		tris[1] = 2;
		tris[2] = 1;

		tris[3] = 2;
		tris[4] = 3;
		tris[5] = 1;

		mesh.triangles = tris;

		Vector3[] normals = new Vector3[4];
		normals[0] = -Vector3.forward;
		normals[1] = -Vector3.forward;
		normals[2] = -Vector3.forward;
		normals[3] = -Vector3.forward;

		mesh.normals = normals;

		Vector2[] uv = new Vector2[4];
		uv[0] = new Vector2(0, 0);
		uv[1] = new Vector2(1, 0);
		uv[2] = new Vector2(0, 1);
		uv[3] = new Vector2(1, 1);

		mesh.uv = uv;
	}

	private void UpdateVertices() {
		Debug.Log("UPDATING VERTS");
		
		Vector3 bLeft = Vector3.zero;
		Vector3 tLeft =  height*Vector3.up;
		Vector3 bRight = width*Vector3.right;
		Vector3 tRight = width*Vector3.right+height*Vector3.up;
		//these might need to be added to the wall object as private parameters
		Vector3[] verts = new Vector3[4];
		if (transform.childCount > 0) { // just assume a door for now
			verts = new Vector3[8];
		}
	
		verts[0] = bLeft;
		verts[1] = bRight;
		verts[2] = tLeft;
		verts[3] = tRight;

		if (transform.childCount > 0) { // just assume a door for now
			Door door = GetComponentInChildren<Door>();
			verts[4] = door.transform.localPosition;
			verts[5] = door.transform.localPosition + Vector3.right*door.width;
			verts[6] = door.transform.localPosition + Vector3.up*door.height;
			verts[7] = door.transform.localPosition + Vector3.right*door.width + Vector3.up*door.height;
		}
		
		mesh.vertices = verts;
	}

	private void UpdateTris() {
		int[] tris = new int[6];
		if (transform.childCount == 0) {
			tris[0] = 0;
			tris[1] = 2;
			tris[2] = 1;

			tris[3] = 2;
			tris[4] = 3;
			tris[5] = 1;
		} else {
			tris = new int[18];

			tris[0] = 0;
			tris[1] = 6;
			tris[2] = 4;

			tris[3] = 0;
			tris[4] = 2;
			tris[5] = 6;

			tris[6] = 6;
			tris[7] = 2;
			tris[8] = 3;

			tris[9] = 6;
			tris[10] = 3;
			tris[11] = 7;

			tris[12] = 7;
			tris[13] = 3;
			tris[14] = 1;

			tris[15] = 5;
			tris[16] = 7;
			tris[17] = 1;

		}

		mesh.triangles = tris;
	}

	private void UpdateNormals() {
		Debug.Log("UPDATING NORMALS");
		mesh.RecalculateNormals();
		
	}
}
