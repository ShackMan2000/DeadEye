using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SpawnFactory
{
    [InitializeOnLoad]
    public static class AssetChecker
    {
        public const string VSPDEFINE = "VEGETATION_STUDIO_PRO";
        public const string MECDEFINE = "MORE_EFFICIENT_COROUTINES";

        public const string VSPNAMESPACE = "AwesomeTechnologies.VegetationSystem.Biomes";
        public const string MECNAMESPACE = "MEC";

        // Runs on unity editor reload
        static AssetChecker()
        {
            HandleIntegrationDefines();
            MoveGizmosIcon();
        }

        private static void HandleIntegrationDefines()
        {
            if (!CheckIfInDefines(VSPDEFINE) && NamespaceExists(VSPNAMESPACE))
                AddDefineIfNeeded(VSPDEFINE);
            else
                RemoveVSPCompatibility();

            if (!CheckIfInDefines(MECDEFINE) && NamespaceExists(MECNAMESPACE))
                AddDefineIfNeeded(MECDEFINE);
            else
                RemoveMECCompatibility();
        }

        public static bool NamespaceExists(string desiredNamespace)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.Namespace == desiredNamespace)
                        return true;
                }
            }
            return false;
        }

        static void AddDefineIfNeeded(string define)
        {
            // Get defines.
            BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);

            // Append only if not defined already.
            if (defines.Contains(define))
                return;

            // Append.
            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, (defines + ";" + define));
            Debug.LogWarning("<b>" + define + "</b> added to <i>Scripting Define Symbols</i> for selected build target (" + EditorUserBuildSettings.activeBuildTarget.ToString() + ").");
        }

        static bool CheckIfInDefines(string define)
        {
            BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);

            if (defines.Contains(define))
                return true;
            else
                return false;
        }

        static void RemoveDefineIfNeeded(string define)
        {
            // Get defines.
            BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);

            // Append only if not defined already.
            if (defines.Contains(define))
            {
                string newDefines = defines.Replace(";" + define, "");
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, newDefines);
            }
            else
            {
                return;
            }
        }

        private static void RemoveVSPCompatibility()
        {
            if (CheckIfInDefines(VSPDEFINE))
            {
                if (!NamespaceExists(VSPNAMESPACE))
                {
                    RemoveDefineIfNeeded(VSPDEFINE);
                }
            }
        }

        private static void RemoveMECCompatibility()
        {
            if (CheckIfInDefines(MECDEFINE))
            {
                if (!NamespaceExists(MECNAMESPACE))
                {
                    RemoveDefineIfNeeded(MECDEFINE);
                }
            }
        }

        public static void MoveGizmosIcon()
        {
            if (!File.Exists(Application.dataPath + "/Gizmos/Spawn Factory/Node_Cog.png"))
            {
                string iconPath = Application.dataPath + "/Spawn Factory/Node_Cog.png";
                if (File.Exists(iconPath))
                {
                    string gizmosPath = Application.dataPath + "/Gizmos";
                    if (Directory.Exists(gizmosPath))
                    {
                        MoveFileToGizmos(gizmosPath, iconPath);
                    }
                    else
                    {
                        Directory.CreateDirectory(gizmosPath);
                        MoveFileToGizmos(gizmosPath, iconPath);
                    }
                }
            }
        }

        private static void MoveFileToGizmos(string gizmosPath, string iconPath)
        {
            string sfGizmosDir = gizmosPath + "/Spawn Factory";
            if (Directory.Exists(sfGizmosDir))
            {
                File.Move(iconPath, sfGizmosDir + "/Node_Cog.png");
            }
            else
            {
                Directory.CreateDirectory(sfGizmosDir);
                File.Move(iconPath, sfGizmosDir + "/Node_Cog.png");
            }
        }
    }
}