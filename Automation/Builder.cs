using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Builder : MonoBehaviour
{
    private static string[] _scenes;

    private static string[] _args;

    private static string _buildDir = "Builds/";

    private static string GetCommandLine(string key)
    {
        if (_args == null)
        {
            _args = System.Environment.GetCommandLineArgs();
        }

        for (int i = 0; i < _args.Length; i++)
        {
            Debug.Log("ARG " + i + ": " + _args[i]);

            if (key == _args[i])
            {
                return _args[i + 1];
            }
        }

        return "";
    }

    private static void CheckFolders(string platform)
    {
        if (!Directory.Exists(_buildDir))
        {
            Directory.CreateDirectory(_buildDir);
        }

        if (Directory.Exists(_buildDir + platform))
            Directory.Delete(_buildDir + platform, true);

        _scenes = EditorBuildSettings.scenes.Select(x => x.path).ToArray();
    }

    private static void BuildAndroid(string folder, string appendEndFile = "", bool oculusStore = false)
    {
        CheckFolders(folder);

        try
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = _scenes;
            buildPlayerOptions.locationPathName = _buildDir + folder + "/" + Application.productName + appendEndFile + ".apk";
            buildPlayerOptions.target = BuildTarget.Android;
            buildPlayerOptions.options = BuildOptions.None;

            var filePath = "Assets/Plugins/Android/AndroidManifest.xml";
            if (oculusStore)
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
                File.Copy("Assets/Plugins/Android/AndroidManifest_OculusStore.xml", filePath);
            }
            else
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }


            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
            throw;
        }
    }


    [MenuItem("Automation/Android/Build")]
    public static void PerformBuildAndroid()
    {
        BuildAndroid("Android");
    }
    [MenuItem("Automation/Oculus/Go/Build Store")]
    public static void PerformBuildOculusGoStore()
    {
        BuildAndroid("Oculus", "", true);
    }

    [MenuItem("Automation/Oculus/Go/Build")]
    public static void PerformBuildOculusGo()
    {
        BuildAndroid("Oculus", "_dev");
    }

    [MenuItem("Automation/Oculus/Quest/Build")]
    public static void PerformBuildOculusQuest()
    {
        BuildAndroid("Oculus");
    }

    [MenuItem("Automation/iOS/Build")]
    public static void PerformBuildiOS()
    {
        CheckFolders("iOS");

        try
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = _scenes;
            buildPlayerOptions.locationPathName = _buildDir + "iOS/" + Application.productName;
            buildPlayerOptions.target = BuildTarget.iOS;
            buildPlayerOptions.options = BuildOptions.None;
            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
            throw;
        }
    }


    [MenuItem("Automation/Desktop/Build Mac")]
    public static void PerformBuildMacOS()
    {
        CheckFolders("Mac");
        try
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = _scenes;
            buildPlayerOptions.locationPathName = _buildDir + "Mac/" + Application.productName + ".app";
            buildPlayerOptions.target = BuildTarget.StandaloneOSX;
            buildPlayerOptions.options = BuildOptions.None;
            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
            throw;
        }
    }

    [MenuItem("Automation/Desktop/Build Linux")]
    public static void PerformBuildLinux()
    {
        CheckFolders("Linux");
        try
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = _scenes;
            buildPlayerOptions.locationPathName = _buildDir + "Linux/" + Application.productName;
            buildPlayerOptions.target = BuildTarget.StandaloneLinuxUniversal;
            buildPlayerOptions.options = BuildOptions.None;
            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
            throw;
        }
    }

    [MenuItem("Automation/Desktop/Build Windows")]
    public static void PerformBuildWindows()
    {
        CheckFolders("Win");
        try
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = _scenes;
            buildPlayerOptions.locationPathName = _buildDir + "Win/" + Application.productName + ".exe";
            buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
            buildPlayerOptions.options = BuildOptions.None;
            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
            throw;
        }
    }

    [MenuItem("Automation/Desktop/Build HTML")]
    public static void PerformBuildHtml()
    {
        CheckFolders("HTML");
        try
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = _scenes;
            buildPlayerOptions.locationPathName = _buildDir + "HTML/" + Application.productName;
            buildPlayerOptions.target = BuildTarget.WebGL;
            buildPlayerOptions.options = BuildOptions.None;
            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
            throw;
        }
    }


    [MenuItem("Automation/Bundle/Build Desktop")]
    static void InternalBuildDesktop()
    {
        PerformBuildWindows();
        PerformBuildMacOS();
        PerformBuildLinux();
    }

    [MenuItem("Automation/Bundle/Build Mobile")]
    static void InternalBuildMobile()
    {
        PerformBuildiOS();
        PerformBuildAndroid();
    }
}