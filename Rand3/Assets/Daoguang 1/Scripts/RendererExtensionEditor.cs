using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using UnityEditorInternal;
using System.Reflection;

public class RendererExtensionEditor : Editor {

	public static string[] GetSortingLayerNames(){
		Type internalEditorUtilityType = typeof(InternalEditorUtility);
		PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
		return (string[])sortingLayersProperty.GetValue(null, new object[0]);
	}

	public static void OnInspectorGUIRenderer(Renderer renderer){

		GUILayout.BeginVertical("box");
		{
			EditorGUILayout.BeginHorizontal ();
			EditorGUI.BeginChangeCheck ();
			string[] names = GetSortingLayerNames ();
			
			int choice = 0;
			bool match = false;
			while (choice < names.Length) {
				if(names[choice] == renderer.sortingLayerName){
					match = true;
					break;
				}else{
					choice++;
				}		
			}
			if (!match) {
				choice = 0;		
			}
			int selection = EditorGUILayout.Popup ("Sorting Layer Name", choice, names);
			if (EditorGUI.EndChangeCheck ()) {
				renderer.sortingLayerName = names[selection];		
			}
			EditorGUILayout.EndHorizontal ();
			
			EditorGUILayout.BeginHorizontal ();
			EditorGUI.BeginChangeCheck ();
			int order = EditorGUILayout.IntField ("Sorting Order", renderer.sortingOrder);
			if (EditorGUI.EndChangeCheck ()) {
				renderer.sortingOrder = order;		
			}
			EditorGUILayout.EndHorizontal ();
			
		}
		GUILayout.EndVertical();
	}
}

[CustomEditor(typeof(MeshRenderer))]
public class MeshRendererExtensionEditor : Editor {
	public override void OnInspectorGUI(){
		base.OnInspectorGUI ();
			RendererExtensionEditor.OnInspectorGUIRenderer(target as Renderer);
	}
}

[CustomEditor(typeof(SkinnedMeshRenderer))]
public class SkinnedMeshRendererExtensionEditor : Editor {
	public override void OnInspectorGUI(){
		base.OnInspectorGUI ();
		RendererExtensionEditor.OnInspectorGUIRenderer(target as Renderer);
	}
}

[CustomEditor(typeof(SpriteRenderer))]
public class SpriteRendererExtensionEditor : Editor {
	public override void OnInspectorGUI(){
		base.OnInspectorGUI ();
		RendererExtensionEditor.OnInspectorGUIRenderer(target as Renderer);
	}
}

[CustomEditor(typeof(LineRenderer))]
public class LineRendererExtensionEditor : Editor {
	public override void OnInspectorGUI(){
		base.OnInspectorGUI ();
		RendererExtensionEditor.OnInspectorGUIRenderer(target as Renderer);
	}
}

//[CustomEditor(typeof(ClothRenderer))]
//public class ClothRendererExtensionEditor : Editor {
//	public override void OnInspectorGUI(){
//		base.OnInspectorGUI ();
//		RendererExtensionEditor.OnInspectorGUIRenderer(target as Renderer);
//	}
//}

[CustomEditor(typeof(TrailRenderer))]
public class TrailRendererExtensionEditor : Editor {
	public override void OnInspectorGUI(){
		base.OnInspectorGUI ();
		RendererExtensionEditor.OnInspectorGUIRenderer(target as Renderer);
	}
}

[CustomEditor(typeof(ParticleRenderer))]
public class ParticleRendererExtensionEditor : Editor {
	public override void OnInspectorGUI(){
		base.OnInspectorGUI ();
		RendererExtensionEditor.OnInspectorGUIRenderer(target as Renderer);
	}
}

//[CustomEditor(typeof(ParticleSystem))]
//public class ParticleSystemExtensionEditor : Editor {
//	public override void OnInspectorGUI(){
////		base.OnInspectorGUI ();
////		DrawDefaultInspector ();
////		RendererExtensionEditor.OnInspectorGUIRenderer(target as Renderer);
//	}
//}
