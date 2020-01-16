using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [MenuItem("Automation/Android/Build")]
    static void PerformBuildAndroid()
    {
        string[] _scenes = EditorBuildSettings.scenes.Select(x => x.path).ToArray();
        BuildPipeline.BuildPlayer(_scenes, "Builds/test.apk", BuildTarget.Android, BuildOptions.None);
    }

    [MenuItem("Automation/iOS/Build")]
    static void PerformBuildiOS()
    {
        string[] _scenes = EditorBuildSettings.scenes.Select(x => x.path).ToArray();
        BuildPipeline.BuildPlayer(_scenes, "Builds/iOS", BuildTarget.iOS, BuildOptions.None);
    }
}