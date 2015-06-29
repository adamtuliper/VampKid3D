using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

/// <summary>
/// ****  PUT THIS SCRIPT IN A FOLDER NAMED EDITOR  ****
/// </summary>
[InitializeOnLoad]
public class OnUnityLoad
{
    static OnUnityLoad()
    {
        EditorApplication.playmodeStateChanged = () =>
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
            {
                EditorApplication.SaveScene();
                EditorApplication.SaveAssets();
            }
        };
    }

    
}
