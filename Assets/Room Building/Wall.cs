using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[ExecuteInEditMode]
public class Wall : MonoBehaviour {
	public float width, height; //NOT the scale, bc this would affect doors and windows scale
	//Add list of 
	//wall "start position" represented by the gameobject position
	//probably add options for features like doors and windows later

	private Door[] doors;

	private float[] sortedX, sortedY;
	Vertex[] vertz;
	Vertex[] origVertz;


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
		List<Vertex> xVertices = new List<Vertex>();
		List<Vertex> yVertices = new List<Vertex>();
		ArrayList tempVertList = new ArrayList();
		List<float> splitListX = new List<float>();
		List<float> splitListY = new List<float>();

		
		xVertices.Add(new Vertex(bLeft));
		xVertices.Add(new Vertex(bRight));
		yVertices.Add(new Vertex(bLeft));
		yVertices.Add(new Vertex(tLeft));

		doors = transform.GetComponentsInChildren<Door>();
		int id = 0;
		if (doors.Length > 0) {
			List<Vertex> origList = new List<Vertex>();

			Debug.Log("DOORS");
				splitListX.Add(0);
				splitListX.Add(tRight.x);
				splitListY.Add(0);
				splitListY.Add(tRight.y);
			foreach(Door door in doors) {
				Vector3 botLeftDoor = new Vector3(door.transform.localPosition.x, door.transform.localPosition.y);
				Vector3 topRightDoor = new Vector3((door.transform.localPosition.x+door.width), door.transform.localPosition.y+door.height);
				origList.Add(new Vertex(botLeftDoor, id, VertexType.Door));
				origList.Add(new Vertex(topRightDoor, id, VertexType.Door));

				Debug.Log("Orig list inner count: " + origList.Count);

				xVertices.Add(new Vertex(door.transform.localPosition.x*Vector3.right, id, VertexType.Door, VertexType.Wall));
				xVertices.Add(new Vertex((door.transform.localPosition.x+door.width)*Vector3.right, id, VertexType.Door, VertexType.Wall));
				yVertices.Add(new Vertex(door.transform.localPosition.y*Vector3.up, id, VertexType.Door, VertexType.Wall));
				yVertices.Add(new Vertex((door.transform.localPosition.y+door.height)*Vector3.up, id, VertexType.Door, VertexType.Wall));
				id++;
				splitListX.Add(door.transform.localPosition.x);
				splitListX.Add(door.transform.localPosition.x + door.width);
				splitListY.Add(door.transform.localPosition.y);
				splitListY.Add(door.transform.localPosition.y + door.height);

			}
			
			origVertz = origList.ToArray();
			Debug.Log("DOOR VERTICES " + origVertz.Length);
			
			sortedX = splitListX.Distinct().ToArray();
			System.Array.Sort(sortedX);
			sortedY = splitListY.Distinct().ToArray();
			System.Array.Sort(sortedY);

			vertz = Vertex.MergeDistinct(xVertices, yVertices, origList);
			
			Debug.Log("VERTICES: " + vertz.Length);
			
			string strng = "";
			foreach(Vertex v in vertz) {
				strng += "\n" + v.ToString();
			}

			Debug.Log(strng);


			Debug.Log("X count: " + sortedX.Length);
		
			
			Debug.Log("X's: " + string.Join(", ", sortedX.Select(f => f.ToString()).ToArray()));
		
			Debug.Log("Y count: " + sortedY.Length);
			Debug.Log("Y's: " +string.Join(", ", sortedY.Select(f => f.ToString()).ToArray()));

			//generate vert array now

			int vertNum = 0;
			int numVerts = sortedX.Length*sortedY.Length;
			verts = new Vector3[((sortedX.Length)*(sortedY.Length))];
			for(int row = 0; row < sortedY.Length; row++) {
				for (int col = 0; col < sortedX.Length; col++) {
					verts[vertNum] = new Vector3(sortedX[col], sortedY[row]);
					Debug.Log(verts[vertNum]);
					if (vertNum >= 1) {
						Debug.DrawLine(verts[vertNum-1], verts[vertNum], Color.red, 0);
					}
					vertNum++;
					
				}
			}
			Debug.Log("TOTL VERTS" + vertNum);
			mesh.vertices = verts;
			return;
		}

		
	
		verts[0] = bLeft;
		verts[1] = bRight;
		verts[2] = tLeft;
		verts[3] = tRight;

		if (transform.childCount > 0) { // just assume a door for now
			Door door = GetComponentInChildren<Door>();
			float locClampedX = Mathf.Clamp(door.transform.localPosition.x, 0, width-door.width);
			float locClampedY = 0;
			Vector3 locClamped = new Vector3(locClampedX, locClampedY, 0);

			float widthClamped = Mathf.Clamp(locClampedX + door.width, 0, width);
			float heightClamped = Mathf.Clamp(locClampedY + door.height, 0, height);

			verts[4] = locClamped;
			verts[5] = new Vector3(widthClamped, locClampedY);
			verts[6] = new Vector3(locClampedX, heightClamped);//locClamped + Vector3.up*door.height;
			verts[7] = new Vector3(widthClamped, heightClamped);
		}
		
		mesh.vertices = verts;
	}

	private void UpdateTris() {
		int[] tris = new int[6];
		List<int> triList = new List<int>();
		if (transform.childCount == 0) {
			tris[0] = 0;
			tris[1] = 2;
			tris[2] = 1;

			tris[3] = 2;
			tris[4] = 3;
			tris[5] = 1;
		} else {
			int faceColumnCount = sortedX.Length-1;
			int lineColumnCount = faceColumnCount + 1; //4
			int faceRowCount = sortedY.Length-1;
			Debug.Log("Face row count: " + faceRowCount);
			int lineRowCount = faceRowCount + 1;

			int triVertCount = (sortedX.Length-1)*(sortedY.Length-1)*2*3 - doors.Length*2*3;
			Debug.Log(triVertCount);
			tris = new int[triVertCount];
			
			int triVertNum = 0;
			for (int lineRow = 0; lineRow < faceRowCount; lineRow++) {
				for (int lineCol = 0; lineCol < faceColumnCount; lineCol++) {
					//TESTING LISTS:
					int botLeft = lineRow*lineColumnCount + lineCol;
					int topLeft = (lineRow + 1)*lineColumnCount + lineCol;
					int botRight = botLeft + 1;
					int topRight = topLeft + 1;
					bool doorDiag = vertz[botRight].door && vertz[topLeft].door;
					bool windowDiag = vertz[botRight].window && vertz[topLeft].window;

					Vector3 blPos = vertz[botLeft].position;
					Vector3 trPos = vertz[topRight].position;

					Debug.Log("Actual square: " + blPos + ", " + trPos);
					Debug.Log("Orig list count: " + origVertz.Length);
					//test the botRight and topLeft

					bool insideDoor = false;

					for(int i = 0; i < origVertz.Length-1; i++) {
						
						Vector3 blRange = origVertz[i].position;
						Vector3 trRange = origVertz[i+1].position;

						Debug.Log("Door square: " + blRange + ", " + trRange);

						if ((blPos.x >= blRange.x && blPos.x <= trRange.x) && (blPos.y >= blRange.y && blPos.y <= trRange.y)) {
							if ((trPos.x >= blRange.x && trPos.x <= trRange.x) && (trPos.y >= blRange.y && trPos.y <= trRange.y)) {
								insideDoor = true;
							}
						}
						if (insideDoor) {
							break;
						}
						//if both points are >= origlist[i] amd <= origlist[i+1]
						//your insider a door
					}
					if (!insideDoor) {
					//if (!(doorDiag && vertz[botLeft].door) && !(windowDiag && vertz[botLeft].window)) {
						//include bottom tri
						triList.Add(botLeft);
						triList.Add(topLeft);
						triList.Add(botRight);
					//}

					//if (!(doorDiag && vertz[topRight].door) && !(windowDiag && vertz[topRight].window)) {
						//include top tri 
						triList.Add(topLeft);
						triList.Add(topRight);
						triList.Add(botRight);
					//}
					}




					//WORKING TRI
					/*if(lineCol%2 == 0 || lineRow != 0) { // we skip odd ones
						//bot left tri
						int botLeft = lineRow*lineColumnCount + lineCol;
						int topLeft = (lineRow + 1)*lineColumnCount + lineCol;
						int botRight = botLeft + 1;
						int topRight = topLeft + 1;
						tris[triVertNum] = botLeft;
						triVertNum++;
						tris[triVertNum] = topLeft;
						triVertNum++;
						tris[triVertNum] = botRight;
						triVertNum++;


						//top right tri
						tris[triVertNum] = topLeft;
						triVertNum++;
						tris[triVertNum] = topRight;
						triVertNum++;
						tris[triVertNum] = botRight;
						triVertNum++;

						Vector3[] v = mesh.vertices;

						Vector3[] lines = new Vector3[]{v[botLeft], v[topLeft], v[botRight], v[botRight], v[topLeft], v[topRight]};

						Debug.DrawLine(lines[0], lines[1], Color.white, 0);
						Debug.DrawLine(lines[1], lines[2], Color.white, 0);
						Debug.DrawLine(lines[0], lines[2], Color.white, 0);
						Debug.DrawLine(lines[3], lines[4], Color.white, 0);
						Debug.DrawLine(lines[4], lines[5], Color.white, 0);
						Debug.DrawLine(lines[5], lines[3], Color.white, 0);

						/*

						Debug.Log("Bottom Leftt tri: (" + botLeft + ", " + topLeft + ", " + botRight + ")");
						Debug.Log("Top right tri: (" + botRight + ", " + topLeft + ", " + topRight + ")");
						


					}*/
				}
			}
			Debug.Log("End trivertnum: " + triVertNum);

		}
		tris = triList.ToArray();
		mesh.triangles = tris;
	}

	private void UpdateNormals() {
		Debug.Log("UPDATING NORMALS");
		Vector3[] norms = new Vector3[mesh.vertexCount];
		for (int i = 0; i < mesh.vertexCount;i++) {
			norms[i] = -1*Vector3.forward;

		}
		mesh.normals = norms;	
	}

	private void SortDoors() {
		for(int i = 1; i < doors.Length; i++) {
			for(int j = i-1; j >= 0; j--) {
				if (doors[j+1].transform.localPosition.x < doors[j].transform.localPosition.x) {
					//swap
					Door temp = doors[j+1];
					doors[j+1] = doors[j];
					doors[j] = temp;
				} else {
					break;
				}
			}
		}
	}


}
