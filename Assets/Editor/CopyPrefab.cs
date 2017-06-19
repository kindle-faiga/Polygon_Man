using UnityEditor;

public class CopyPrefab : EditorWindow
{
	[MenuItem("Window/CopyPrefab")]
	static void Open()
	{
		EditorWindow.GetWindow<CopyPrefab>("CopyPrefab");
	}

	void OnGUI()
	{
		EditorGUILayout.LabelField("Test");
	}
}