using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] string _sceneName;
    [SerializeField] GameObject[] _setFalse;
    private void Start()
    {
        foreach (GameObject obj in _setFalse)
        {
            obj.SetActive(false);
        }
    }
    public void SceneChanges()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void SceneChanges(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ActiveGameObjectTrue(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void ActiveGameObjectFalse(GameObject obj)
    {
        obj.SetActive(false);
    }
}
