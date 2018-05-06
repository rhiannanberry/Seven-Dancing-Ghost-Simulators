using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {
	public LineRenderer lr;
	List<Vector3> points;
	
	public void UpdateLine(Vector3 mousePos) {
		if (points == null) {
			points = new List<Vector3>();
			SetPoint(mousePos);
			return;
		}
		if (Vector2.Distance(points[points.Count-1], mousePos) > .03f) {
			SetPoint(mousePos);
		}
	}

	void SetPoint (Vector3 mousePos) {
		points.Add(mousePos);
		lr.positionCount = points.Count;
		lr.SetPosition(points.Count - 1, mousePos);
	}
}
