using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Rendering.HighDefinition;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class SceneAtmosphereEditor : EditorWindow
{
    LightingSettings lightingSettingsProfile;
    SceneAtmosphereEditor_Light lightData = new SceneAtmosphereEditor_Light();

    //
    bool infoFO = false;
    bool sceneSettingsFO = false;
    bool lightProfileSettingsFO = false;

    int sortMode = 0; //0 == Name / 1 == Type

    Color defautGuiColor;

    VolumeProfile prof;

    [MenuItem("Tools/Atmosphere")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(SceneAtmosphereEditor));
    }

    void OnGUI()
    {
        GUILayout.Label("Atmosphere editor", EditorStyles.boldLabel);

        sceneSettingsFO = EditorGUILayout.Foldout(sceneSettingsFO, "Scene Settings");
        if (sceneSettingsFO) { ShowSceneSettings(); }

        lightProfileSettingsFO = EditorGUILayout.Foldout(lightProfileSettingsFO, "Light Profile Settings");
        if (lightProfileSettingsFO) { ShowLightProfileSettings(); }

        infoFO = EditorGUILayout.Foldout(infoFO, "Lights");
        if (infoFO) { LightsInScene(); }
    }

    public void Awake()
    {
        defautGuiColor = GUI.color;
    }

    void ShowSceneSettings()
    {
        GUILayout.BeginVertical("Box");
        GUILayout.Label("Scene Light Settings", EditorStyles.boldLabel);

        GUILayout.EndVertical();
    }

    void ShowLightProfileSettings()
    {
        GUILayout.BeginVertical("Box");
        GUILayout.Label("Environment Profile", EditorStyles.boldLabel);
        EnvironmentProfile();
        GUILayout.EndVertical();
    }

    //EnvironmentProfile
    void EnvironmentProfile()
    {
        prof = EditorGUILayout.ObjectField("Volume Profile:", prof, typeof(VolumeProfile), false) as VolumeProfile;
    }

    //Lights
    void LightsInScene()
    {
        GUILayout.BeginVertical("Box");
        if (GUILayout.Button("Refresh", GUILayout.Width(60)))
        {
            RefreshLightsData();
        }

        GUILayout.BeginHorizontal();
        GUILayout.Label("Sort:", EditorStyles.boldLabel);
        if (GUILayout.Button("Name"))
        {
        }
        if (GUILayout.Button("Type"))
        {
        }
        GUILayout.EndHorizontal();


        //Lights
        try
        {
            for (int i = 0; i < lightData.lights.Count; i++)
            {
                GUILayout.BeginHorizontal();

                //lightData.lights[i].foldOut = EditorGUILayout.Foldout(lightData.lights[i].foldOut, lightData.lights[i].obj.name);
                if (GUILayout.Button(lightData.lights[i].obj.name + " - " + lightData.lights[i].light.type.ToString()))
                {
                    lightData.lights[i].foldOut = !lightData.lights[i].foldOut;
                }

                if (GUILayout.Button("Select", GUILayout.Width(65)))
                {
                    Selection.activeObject = lightData.lights[i].obj;
                }
                GUILayout.EndHorizontal();
                //Foldout
                if (lightData.lights[i].foldOut)
                {
                    GUILayout.Label("Type: " + lightData.lights[i].light.type.ToString());
                    GUILayout.Label("Intesity: " + lightData.lights[i].light.intensity.ToString());

                    //Show units
                    CheckLightUnit(i);
                    GUILayout.Label("Unit: " + lightData.lights[i].light.lightUnit.ToString());
                }
            }
        }
        catch
        {
            CheckRemovedLight();
        }

        GUILayout.EndVertical();
    }
    void CheckLightUnit(int lightid)
    {
        GUILayout.BeginHorizontal();

        if(lightData.lights[lightid].light.lightUnit == LightUnit.Lumen)
        { GUI.color = Color.green; } else { GUI.color = defautGuiColor;  }
        EditorGUI.BeginDisabledGroup(CheckTypeAvailable(lightData.lights[lightid].light.type, LightUnit.Lumen));
        if (GUILayout.Button("Lumen")) { lightData.lights[lightid].light.SetLightUnit(LightUnit.Lumen); }
        EditorGUI.EndDisabledGroup();

        if (lightData.lights[lightid].light.lightUnit == LightUnit.Candela)
        { GUI.color = Color.green; }
        else { GUI.color = defautGuiColor; }
        EditorGUI.BeginDisabledGroup(CheckTypeAvailable(lightData.lights[lightid].light.type, LightUnit.Candela));
        if (GUILayout.Button("Candela")) { lightData.lights[lightid].light.SetLightUnit(LightUnit.Candela); }
        EditorGUI.EndDisabledGroup();

        if (lightData.lights[lightid].light.lightUnit == LightUnit.Lux)
        { GUI.color = Color.green; }
        else { GUI.color = defautGuiColor; }
        EditorGUI.BeginDisabledGroup(CheckTypeAvailable(lightData.lights[lightid].light.type, LightUnit.Lux));
        if (GUILayout.Button("Lux")) { lightData.lights[lightid].light.SetLightUnit(LightUnit.Lux); }
        EditorGUI.EndDisabledGroup();

        if (lightData.lights[lightid].light.lightUnit == LightUnit.Nits)
        { GUI.color = Color.green; }
        else { GUI.color = defautGuiColor; }
        EditorGUI.BeginDisabledGroup(CheckTypeAvailable(lightData.lights[lightid].light.type, LightUnit.Nits));
        if (GUILayout.Button("Nits")) { lightData.lights[lightid].light.SetLightUnit(LightUnit.Nits); }
        EditorGUI.EndDisabledGroup();

        if (lightData.lights[lightid].light.lightUnit == LightUnit.Ev100)
        { GUI.color = Color.green; }
        else { GUI.color = defautGuiColor; }
        EditorGUI.BeginDisabledGroup(CheckTypeAvailable(lightData.lights[lightid].light.type, LightUnit.Ev100));
        if (GUILayout.Button("Ev100")) { lightData.lights[lightid].light.SetLightUnit(LightUnit.Ev100); }
        EditorGUI.EndDisabledGroup();

        GUILayout.EndHorizontal();
        GUI.color = defautGuiColor;
    }
    bool CheckTypeAvailable(HDLightType type, LightUnit unittype)
    {
        if (type == HDLightType.Directional && unittype == LightUnit.Lumen || type == HDLightType.Directional && unittype == LightUnit.Candela || type == HDLightType.Directional && unittype == LightUnit.Nits || type == HDLightType.Directional && unittype == LightUnit.Ev100)
            return true;
        if (type == HDLightType.Point && unittype == LightUnit.Nits)
            return true;
        if (type == HDLightType.Spot && unittype == LightUnit.Nits)
            return true;
        if (type == HDLightType.Area && unittype == LightUnit.Candela || type == HDLightType.Area && unittype == LightUnit.Lux)
            return true;

        return false;
    }
    void CheckRemovedLight()
    {
        for (int i = 0; i < lightData.lights.Count; i++)
        {
            if(lightData.lights[i].obj == null)
            {
                lightData.lights.RemoveAt(i);
                break;
            }
        }
    }
    void RefreshLightsData()
    {
        GameObject[] obj = getObjects();

        for (int i = 0; i < obj.Length; i++)
        {
            SceneAtmosphereEditor_Lights newlight = new SceneAtmosphereEditor_Lights();
            newlight.obj = obj[i];
            newlight.light = obj[i].GetComponent<HDAdditionalLightData>();

            bool checkexist = false;
            for (int o = 0; o < lightData.lights.Count; o++)
            {
                if (lightData.lights[o].obj == obj[i])
                    checkexist = true;
            }
            if (!checkexist)
                lightData.lights.Add(newlight);
        }
    }
    GameObject[] getObjects()
    {
        Light[] scripts = FindObjectsOfType<Light>();
        GameObject[] objects = new GameObject[scripts.Length];
        for (int i = 0; i < objects.Length; i++)
            objects[i] = scripts[i].gameObject;
        return objects;
    }
}

[System.Serializable]
public class SceneAtmosphereEditor_Light
{
    public List<SceneAtmosphereEditor_Lights> lights = new List<SceneAtmosphereEditor_Lights>();
}

[System.Serializable]
public class SceneAtmosphereEditor_Lights
{
    public Object obj;
    public bool foldOut;
    public HDAdditionalLightData light;
}
