using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Wall))]
public class WallInspector : Editor {

	private void OnSceneGUI() {
		Wall wall = target as Wall;
		Handles.color = Color.white;
		Transform handleTransform = wall.transform;
		Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

		Vector3 widthHeight;

		//these might need to be added to the wall object as private parameters
		Vector3 bLeft = handleTransform.TransformPoint(Vector3.zero);
		Vector3 tLeft =  handleTransform.TransformPoint(wall.height*Vector3.up);
		Vector3 bRight = handleTransform.TransformPoint(wall.width*Vector3.right);
		Vector3 tRight = handleTransform.TransformPoint(wall.width*Vector3.right+wall.height*Vector3.up);
		//handleTransform.TransformPoint(new Vector3(wall.transform.position.x, wall.transform.position.y) + scaledWidth + scaledHeight);
		
		//basically highlighting the outline of the wall

		Handles.DrawLine(bLeft, bRight);
		Handles.DrawLine(tLeft, tRight);
		Handles.DrawLine(bLeft, tLeft);
		Handles.DrawLine(bRight, tRight);

		EditorGUI.BeginChangeCheck();	
		widthHeight = Handles.DoPositionHandle(tRight, handleRotation);
		if (EditorGUI.EndChangeCheck()) {
			Undo.RecordObject(wall, "Scale Wall");
			EditorUtility.SetDirty(wall);
			Vector3 diag = handleTransform.InverseTransformPoint(widthHeight);
			wall.width = diag.x;
			wall.height = diag.y;
		}
	}
}
