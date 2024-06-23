using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    [SerializeField]PlayerHP _player=default;

    [SerializeField] public int _score = 0;
    [SerializeField] Text _scoreText;
    [SerializeField] Text _hpText;
    [SerializeField]PlayerHP _playerHp;
    [SerializeField]Slider _hpSlider;
    [SerializeField]GameObject _gardSkillUI;
    [SerializeField]SkillShot _skillShot;
    [SerializeField] BoxCollider2D _spawnArea;
    [SerializeField] GameObject _spawnPrefab;
    [SerializeField] GameObject[] _targetObjects;
    [SerializeField]GameObject _gameOvarUI;
    [SerializeField] Text _scoreResult;
    GameObject _hpChild;
    // Start is called before the first frame update
    void Start()
    {
        _hpChild = _hpSlider.transform.GetChild(1).gameObject;
        Cursor.lockState = CursorLockMode.Confined;
        _hpSlider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < _targetObjects.Length; i++)
        {
            if (_targetObjects[i] == null)
            {
                _targetObjects[i]=CreatorTarget();
            }
        }
        _scoreText.text = "SCORE : " + _score.ToString();

        if (_player == null) GameOver();
        if(_skillShot._gardActive)
        {
            _gardSkillUI.GetComponent<Image>().color=Color.red;
        }
        if(_skillShot._gardActive==false)
        {
            _gardSkillUI.GetComponent<Image>().color = Color.white;
        }
        _hpSlider.value=(float)_playerHp._hitPoint/_playerHp._maxHP;
        if (_hpSlider.value == 0)
        {
            _hpChild.SetActive(false);
        }
    }
    GameObject CreatorTarget()
    {
        float randomX = Random.Range(-_spawnArea.size.x, _spawnArea.size.x) * .5f;
        float randomY = Random.Range(-_spawnArea.size.y, _spawnArea.size.y) * .5f;
        Vector2 spawnArea = new Vector2(randomX + _spawnArea.gameObject.transform.position.x, randomY + _spawnArea.gameObject.transform.position.y);
        GameObject target = Instantiate(_spawnPrefab, spawnArea, Quaternion.identity);
        target.tag="Target";
        return target;
    }
    void GameOver()
    {
        _gameOvarUI.SetActive(true);
        _hpSlider.gameObject.SetActive(false);
        _scoreText.gameObject.SetActive(false);
        _scoreResult.text="SCORE : "+_score.ToString();
    }
}
