using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ProceduralLevelGenerator))]
public class ProceduralGeneratorEditor : Editor {

	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		
		ProceduralLevelGenerator PG = (ProceduralLevelGenerator)target;
		if(GUILayout.Button("Generate in editor (Optional)")) {
			PG.EditorDoGenerate();
		}
		if(GUILayout.Button("Clear Current Level")) {
			PG.BlankMapEditor();
		}
	}
}
