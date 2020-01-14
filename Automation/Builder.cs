using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [MenuItem("Automation/Build")]
    static void PerformBuild()
    {
        string[] _scenes = EditorBuildSettings.scenes.Select(x => x.path).ToArray();
        BuildPipeline.BuildPlayer(_scenes, "Builds/test.apk", BuildTarget.Android, BuildOptions.None);
    }
}