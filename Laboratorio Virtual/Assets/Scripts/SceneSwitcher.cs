using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string scene;

    public void SwitchScene()
    {
        if (scene.Equals("quit"))
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
    }

    public void VisualizacaoScene()
    {
        SceneManager.LoadScene("visualizacao");
    }

    public void EspecialScene()
    {
        SceneManager.LoadScene("especial");
    }
}
