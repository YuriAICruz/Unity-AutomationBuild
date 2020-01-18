using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Graphene.Automation
{
    public class Builder : MonoBehaviour
    {
        private static string[] _args;
         
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
        
        
        [MenuItem("Automation/Android/Build")]
        public static void PerformBuildAndroid()
        {
            var scenes = EditorBuildSettings.scenes.Select(x => x.path).ToArray();
            BuildPipeline.BuildPlayer(scenes, "Builds/Android/appVersion.apk", BuildTarget.Android, BuildOptions.None);
        }
    
        [MenuItem("Automation/Oculus/Quest/Build")]
        public static void PerformBuildOculusQuest()
        {
            GetCommandLine("fake");
            
            var scenes = EditorBuildSettings.scenes.Select(x => x.path).ToArray();
            BuildPipeline.BuildPlayer(scenes, "Builds/Oculus/questApp.apk", BuildTarget.Android, BuildOptions.None);
        }

        [MenuItem("Automation/iOS/Build")]
        public static void PerformBuildiOs()
        {
            var scenes = EditorBuildSettings.scenes.Select(x => x.path).ToArray();
            BuildPipeline.BuildPlayer(scenes, "Builds/iOS", BuildTarget.iOS, BuildOptions.None);
        }
    }
}