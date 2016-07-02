using System;
using UnityEngine;
using UnityEditor;
using com.ootii.Input;

[CanEditMultipleObjects]
[CustomEditor(typeof(InputManagerCore))]
public class InputManagerCoreEditor : Editor
{
    // Helps us keep track of when the things need to be saved.
    private bool mIsDirty;

    // The actual class we're storing
    private InputManagerCore mInputManagerCore;
    private SerializedObject mInputManagerCoreSO;

    /// <summary>
    /// Called when the script object is loaded
    /// </summary>
    void OnEnable()
    {
        // Grab the serialized objects
        mInputManagerCore = (InputManagerCore)target;
        mInputManagerCoreSO = new SerializedObject(target);

        // Start up the input manager and set the stub
        InputManager.Core = mInputManagerCore;
    }

    /// <summary>
    /// Called when the inspector needs to draw
    /// </summary>
    public override void OnInspectorGUI()
    {
        // Pulls variables from runtime so we have the latest values.
        mInputManagerCoreSO.Update();

        EditorGUILayout.HelpBox("This component isn't required unless you want to set input properties different than the defaults", MessageType.Info);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(new GUIContent("Enable Xbox Controller", "Determines if we look for input from an Xbox controller"), GUILayout.Width(150));
        bool lNewIsXboxControllerEnabled = EditorGUILayout.Toggle(InputManager.IsXboxControllerEnabled);
        if (lNewIsXboxControllerEnabled != InputManager.IsXboxControllerEnabled)
        {
            mIsDirty = true;
            mInputManagerCore.IsXboxControllerEnabled = lNewIsXboxControllerEnabled;
        }
        EditorGUILayout.EndHorizontal();

        // If there is a change... update.
        if (mIsDirty)
        {
            // Flag the object as needing to be saved
            EditorUtility.SetDirty(mInputManagerCore);

            // Pushes the values back to the runtime so it has the changes
            mInputManagerCoreSO.ApplyModifiedProperties();

            // Finally, tell the actual input manager to initialize
            InputManager.Initialize();

            // Clear out the dirty flag
            mIsDirty = false;
        }
    }
}
