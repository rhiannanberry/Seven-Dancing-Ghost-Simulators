using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baseboard : MonoBehaviour {
	Wall wall;
	public void UpdateBaseboard() {
		/*
		 *
		 * Get relevant values and create necessary arrays
		 *
		 */

		wall = GetComponentInParent<Wall>();
		Mesh prefabMesh = wall.baseboard.GetComponent<MeshFilter>().sharedMesh;
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		Mesh mesh = meshFilter.sharedMesh;

		int prefVertexCount = prefabMesh.vertexCount;
		Vector3[] prefVertices = prefabMesh.vertices;	//sillohuette of the baseboard

		int meshVertexCount = prefVertexCount*2;
		Vector3[] meshVertices = new Vector3[meshVertexCount];
		Vector2[] meshUVs = new Vector2[meshVertexCount];

		int endCapTriCount = prefVertexCount-2;
		int tubeTriCount = 2*(prefVertexCount-1);
		int triCount = 2*endCapTriCount + tubeTriCount;
		int triVertexCount = triCount*3;
		int[] meshTriangles = new int[triVertexCount];

		int additionalLoopCount = 1 + wall.doors.Length * 2;


		if (mesh == null) {
			mesh = new Mesh();
			meshFilter.mesh = mesh;
		} 

		/*
		 * Update vertex array and UV array
		 */

		//translation matrix just moves indiv. vertices like transform.Translate() does
		Matrix4x4 transMatrix = Matrix4x4.Translate(Vector3.right*wall.width);
		for (int i = 0; i < prefVertexCount; i++) {
			Vector3 translation = transMatrix.MultiplyPoint3x4(prefVertices[i]);
			meshVertices[i] = prefVertices[i];
			meshVertices[i+prefVertexCount] = translation;

			meshUVs[i] = prefVertices[i];
			meshUVs[i+prefVertexCount] = translation;
		}

		/*
		 * Update triangle vertex array
		 */
		int currentTriVertex = 0;

		//left end cap
		for (int j = 0; j < prefVertexCount - 2; j++) {
			meshTriangles[currentTriVertex++] = j+1;
			meshTriangles[currentTriVertex++] = 0;
			meshTriangles[currentTriVertex++] = j+2;
		}

		//tube
		for (int k = 0; k < prefVertexCount-1; k++) {
			//back triangle
			meshTriangles[currentTriVertex++] = k + prefVertexCount;
			meshTriangles[currentTriVertex++] = k;
			meshTriangles[currentTriVertex++] = k + 1;

			//front triangle
			meshTriangles[currentTriVertex++] = k + prefVertexCount;
			meshTriangles[currentTriVertex++] = k + 1;
			meshTriangles[currentTriVertex++] = k + prefVertexCount + 1;
		}

		//right end cap
		for (int l = prefVertexCount; l < meshVertexCount-2; l++) {
			meshTriangles[currentTriVertex++] = 0;
			meshTriangles[currentTriVertex++] = l+1;
			meshTriangles[currentTriVertex++] = l+2;
		}

		/*
		 * Set all new mesh values and calculate normals
		 */

		mesh.vertices = meshVertices;
		mesh.triangles = meshTriangles;
		mesh.uv = meshUVs;
		mesh.RecalculateNormals();
		Debugging();
	}

	private void Debugging() {
		Debug.Log("DOOR BOTTOM LEFT: " + wall.doors[0].transform.localPosition.x);
		Debug.Log("DOOR BOTTOM RIGHT: " + wall.doors[0].transform.localPosition.x + wall.doors[0].width);

		Debug.DrawLine(Vector3.down, Vector3.right*(wall.doors[0].transform.localPosition.x + wall.doors[0].width)*wall.transform.localScale.x, Color.red, 0);
	}
	
}
