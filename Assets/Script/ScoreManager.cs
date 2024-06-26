using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int _highScore;
    public static int _playerHitPoint;
    [SerializeField]InGameManager _gameManager;
    [SerializeField]Text _text;
    [SerializeField]PlayerHP _playerHP;

    private void Awake()
    {
        enabled=false;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (_highScore < _gameManager._scoreCountResult&&_playerHP._hitPoint>0)
        {
            _highScore= _gameManager._scoreCountResult;
            _playerHitPoint=_playerHP._hitPoint;
        }
        _text.text= "HighScore : " + _highScore+"  HP : "+_playerHitPoint;
    }
}
