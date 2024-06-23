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
    [SerializeField] GameObject _spawnObject;
    [SerializeField]GameObject _gameOvarUI;
    GameObject _hpChild;
    int _scoreSub = 0;
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
        if (_score > _scoreSub)
        {
            CreatorArea();
            _scoreSub = _score;
        }
        _scoreText.text = _score.ToString();
        if (_hpText != null)
        {
            _hpText.text = _playerHp._hitPoint.ToString();
        }

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
    void CreatorArea()
    {
        float randomX = Random.Range(-_spawnArea.size.x, _spawnArea.size.x) * .5f;
        float randomY = Random.Range(-_spawnArea.size.y, _spawnArea.size.y) * .5f;
        Instantiate(_spawnObject, new Vector2(randomX + _spawnArea.gameObject.transform.position.x, randomY + _spawnArea.gameObject.transform.position.y), Quaternion.identity).tag="Target";
    }
    void GameOver()
    {
        _gameOvarUI.SetActive(true);
        _hpSlider.gameObject.SetActive(true);
    }
}
