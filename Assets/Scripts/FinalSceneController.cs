using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneController : MonoBehaviour
{

    void Update()
    {
        if (Input.GetButton("Escape"))
        {
            Debug.Log("Escape");
            QuitGame();
        }

        if (Input.GetButton("Enter"))
        {
            LoadGameScene();
        }
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
