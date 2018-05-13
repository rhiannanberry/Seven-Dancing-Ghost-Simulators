using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : MonoBehaviour {
	public GameObject linePrefab;
	public RenderTexture rt;
	public Texture2D snowflake;
	Line activeLine;
	Vector2 bottomLeft, topRight;
	bool updateTexture = false;	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			GameObject lineGO = Instantiate(linePrefab);
			lineGO.transform.SetParent(this.transform);
			lineGO.transform.position = Camera.main.transform.position;
			lineGO.transform.rotation = Camera.main.transform.rotation;
			activeLine = lineGO.GetComponent<Line>();
			bottomLeft = topRight = Input.mousePosition;
		}

		if (Input.GetMouseButtonUp(0)) {
			updateTexture = true;
			
		}

		if (activeLine != null) {
			bottomLeft.x = Mathf.Min(bottomLeft.x, Input.mousePosition.x);
			bottomLeft.y = Mathf.Min(bottomLeft.y, Input.mousePosition.y);
			topRight.x = Mathf.Max(topRight.x, Input.mousePosition.x);
			topRight.y = Mathf.Max(topRight.y, Input.mousePosition.y);
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane+1.49f));
			activeLine.UpdateLine(mousePos);
		}
	}

	private void OnPostRender() {
		if (updateTexture) {
			snowflake.ReadPixels(new Rect(0,0, rt.width, rt.height),0,0,false);
			snowflake.Apply();
			snowflake.FloodFillArea(0,0, Color.red);
			snowflake.Apply();
			activeLine.gameObject.layer += 1; //moved to drawn layer so it's hidden from main camera
			activeLine = null;
			Debug.Log("BottomLeft Screenspace: " + bottomLeft + ", TopRight: " + topRight);
			updateTexture = false;
		}
	}
}
