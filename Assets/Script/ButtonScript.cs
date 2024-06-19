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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void SceneChanges(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Invoke("LoadSceneEfect", 1f);
        ActiveGameObjectTrue(_panel);
        Image panelColor=_panel.GetComponent<Image>();
        //SpanelColor.color = new Color(255, 0, 0, 1);
    }
    public IEnumerator SceneChanges(Scene scene)
    {
        _sceneName = scene.name;
        SceneManager.LoadScene(_sceneName);
        yield return new WaitForSeconds(1) ;
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
