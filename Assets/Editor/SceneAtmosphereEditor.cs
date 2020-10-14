using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneAtmosphereEditor : EditorWindow
{
    LightingSettings lightingSettingsProfile;

    //
    bool infoFO = false;
    bool sceneSettingsFO = false;
    bool lightProfileSettingsFO = false;

    [MenuItem("Tools/Atmosphere")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(SceneAtmosphereEditor));
    }

    void OnGUI()
    {
        GUILayout.Label("Atmosphere editor", EditorStyles.boldLabel);

        infoFO = EditorGUILayout.Foldout(infoFO, "Info");
        if (infoFO){ ShowInfo(); }

        sceneSettingsFO = EditorGUILayout.Foldout(sceneSettingsFO, "Scene Settings");
        if (sceneSettingsFO) { ShowSceneSettings(); }

        lightProfileSettingsFO = EditorGUILayout.Foldout(lightProfileSettingsFO, "Light Profile Settings");
        if (lightProfileSettingsFO) { ShowLightProfileSettings(); }
    }

    void ShowInfo()
    {
        GUILayout.BeginVertical("Box");
        GUILayout.Label("Info", EditorStyles.boldLabel);

        GUILayout.EndVertical();
    }

    void ShowSceneSettings()
    {
        GUILayout.BeginVertical("Box");
        GUILayout.Label("Scene Settings", EditorStyles.boldLabel);
        GUILayout.EndVertical();
    }

    void ShowLightProfileSettings()
    {
        GUILayout.BeginVertical("Box");
        GUILayout.Label("Light Profile Settings", EditorStyles.boldLabel);
        GUILayout.EndVertical();
    }
}
