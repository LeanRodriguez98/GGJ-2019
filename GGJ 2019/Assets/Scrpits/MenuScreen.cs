using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScreen : MonoBehaviour
{
    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void EnableGameObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void DisableGameObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
