using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        // "Stage1" is the name of the first scene we created.
        // Application.LoadLevel("Stage1"); deprecated
        SceneManager.LoadScene("Stage1");
    }
}
