using UnityEngine;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    [SerializeField] SkillShot _playerSkill = default;

    [SerializeField] public int _score = 0;
    [SerializeField] Text _scoreText;
    [SerializeField] PlayerHP _playerHp;
    [SerializeField] Slider _hpSlider;
    GameObject _gardSkillUI;
    [SerializeField] SkillShot _skillShot;
    [SerializeField] BoxCollider2D _spawnArea;
    [SerializeField] GameObject _spawnPrefab;
    [SerializeField] GameObject _bossEnemy;
    [SerializeField] GameObject[] _targetObjects;
    [SerializeField] GameObject _gameOvarUI;
    [SerializeField] Text _scoreResult;
    [SerializeField] Text _timerInGame;
    [SerializeField] Text _resultTextMethod;
    [SerializeField] Text _resultText;
    [SerializeField] float _timeLimit=60;
    float _timer;
    int _TimeOverPlayerHP;
    string _gameOverMethod;
    [HideInInspector]public bool _bossReSpawn;
    float _bossReSpawnTimer;
    GameObject _bossClone;
    ScoreManager _scoreManager;
    public int _scoreCountResult;
    bool _gameOver;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        _hpSlider.value = 1;
        _timer = _timeLimit;
        _bossReSpawn=true;
        _scoreManager=GetComponent<ScoreManager>();
        _gameOver=false;
    }
    void Update()
    {
        if (_score < 0)
        {
            _score = 0;
        }
        if (_gameOverMethod != "TimeOver")
        {
            EnemyReSpown();
        }
        _scoreText.text = "SCORE : " + _score.ToString();
        if (_playerSkill == null&&_gameOver==false)
        {
            GameOver();
            _gameOver=true;
        }
        else if (_timer < 0&&_gameOver==false)
        {
            _gameOver=true;
            _gameOverMethod = "TimeOver";
            _TimeOverPlayerHP = _playerHp._hitPoint;
            Destroy(_bossClone);
            GameOver();
            foreach(var obj in _targetObjects)
            {
                Destroy(obj);
            }
            if (_playerSkill != null)
            {
                _playerSkill.GetComponent<SkillShot>().enabled = false;
            }
        }
        else
        {
            _timer -= Time.deltaTime;
            _timerInGame.text = _timer.ToString("N");
            _hpSlider.value = (float)_playerHp._hitPoint / _playerHp._maxHP;
        }
        CreatBoss();
        if (_bossReSpawn == false)
        {
            _bossReSpawnTimer += Time.deltaTime;
            if (_bossReSpawnTimer > 5)
            {
                _bossReSpawn = true;
                _bossReSpawnTimer = 0;
            }
        }
    }
    GameObject CreatorTarget()
    {
        float randomX = Random.Range(-_spawnArea.size.x, _spawnArea.size.x) * .5f;
        float randomY = Random.Range(-_spawnArea.size.y, _spawnArea.size.y) * .5f;
        Vector2 spawnArea = new Vector2(randomX + _spawnArea.gameObject.transform.position.x, randomY + _spawnArea.gameObject.transform.position.y);
        GameObject target = Instantiate(_spawnPrefab, spawnArea, Quaternion.identity);
        target.tag = "Target";
        return target;
    }
    void GameOver()
    {
        _gameOvarUI.SetActive(true);
        _hpSlider.gameObject.SetActive(false);
        _scoreText.gameObject.SetActive(false);
        _timerInGame.gameObject.SetActive(false);
        if (_gameOverMethod == "TimeOver")
        {
            _resultTextMethod.text = "LIFE : " + _TimeOverPlayerHP.ToString();
            _resultText.text = "you survived";
            _scoreCountResult = _score+_TimeOverPlayerHP*8;
            _scoreResult.text = "SCORE : " + _scoreCountResult.ToString();
        }
        else
        {
            _resultTextMethod.text = "TIME : "+_timer.ToString("N");
            _scoreCountResult = _score;
            _scoreResult.text = "SCORE : " + _scoreCountResult.ToString();
        }
        CreatBoss();
    }
    void EnemyReSpown()
    {
        for (int i = 0; i < _targetObjects.Length; i++)
        {
            if (_targetObjects[i] == null)
            {
                _targetObjects[i] = CreatorTarget();
                _score += 1;
            }
        }
    }
    void CreatBoss()
    {
        if ((_score > 30 || _timer < _timeLimit/2) && _bossClone == null &&_bossReSpawn==true && _gameOverMethod != "TimeOver")
        {
            _bossClone = Instantiate(_bossEnemy);
        }
    }
}
