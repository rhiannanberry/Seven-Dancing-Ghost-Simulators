using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Window))]
public class WindowInspector : Editor {
	private void OnSceneGUI() {
		Window window = target as Window;
		Handles.color = Color.white;
		Transform handleTransform = window.transform;
		Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

		Vector3 widthHeight;

		//these might need to be added to the window object as private parameters
		Vector3 bLeft = handleTransform.TransformPoint(Vector3.zero);
		Vector3 tLeft =  handleTransform.TransformPoint(window.height*Vector3.up);
		Vector3 bRight = handleTransform.TransformPoint(window.width*Vector3.right);
		Vector3 tRight = handleTransform.TransformPoint(window.width*Vector3.right+window.height*Vector3.up);

		//basically highlighting the outline of the window

		Handles.DrawLine(bLeft, bRight);
		Handles.DrawLine(tLeft, tRight);
		Handles.DrawLine(bLeft, tLeft);
		Handles.DrawLine(bRight, tRight);

		EditorGUI.BeginChangeCheck();	
		widthHeight = Handles.DoPositionHandle(tRight, handleRotation);
		if (EditorGUI.EndChangeCheck()) {
			Undo.RecordObject(window, "Scale window");
			EditorUtility.SetDirty(window);
			Vector3 diag = handleTransform.InverseTransformPoint(widthHeight);
			window.ClampedManualUpdate(diag.x, diag.y);
			//window.width = diag.x;
			//window.height = diag.y;
		}
	}
}
