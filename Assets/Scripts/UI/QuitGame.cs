using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void ExitGame()
    {
#if UNITY_EDITOR
        // If running in the Unity Editor, stop playing
        EditorApplication.isPlaying = false;
        Debug.Log("QuitGame: Stopping play mode in the Unity Editor.");
#else
        // If running in a built application, quit the application
        Debug.Log("QuitGame: Application is quitting.");
        Application.Quit();
#endif
    }
}
