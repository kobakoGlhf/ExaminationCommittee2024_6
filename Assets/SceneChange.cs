using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] string _sceneName;
    public void SceneChanges()
    {
        SceneManager.LoadScene(_sceneName);
    }
    public void SceneChanges(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
