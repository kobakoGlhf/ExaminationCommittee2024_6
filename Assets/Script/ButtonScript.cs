using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] string _sceneName;
    [SerializeField] GameObject[] _setFalse;
    [SerializeField] GameObject _panel;
    private void Start()
    {
        foreach (GameObject obj in _setFalse)
        {
            obj.SetActive(false);
        }
        ActiveGameObjectFalse(_panel);
    }
    public void SceneChanges()
    {
        StartCoroutine(ScnenLoader());
    }
    public void SceneChanges(string sceneName)
    {
        StartCoroutine(ScnenLoader(sceneName));
    }
    private IEnumerator ScnenLoader()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        yield break;
    }
    private IEnumerator ScnenLoader(string sceneName)
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(sceneName);
        yield break;
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
