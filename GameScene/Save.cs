using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    public void SaveSceneNumber()
    {
        PlayerPrefs.SetInt("SceneIndex", SceneManager.GetActiveScene().buildIndex + 1);

        // Debug.Log(PlayerPrefs.GetInt("SceneIndex"));

        PlayerPrefs.Save();
    }

    public void ResetSceneNumber()
    {
        PlayerPrefs.SetInt("SceneIndex", 1);

        // Debug.Log(PlayerPrefs.GetInt("SceneIndex"));

        PlayerPrefs.Save();
    }
}
