using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShot : MonoBehaviour
{
    [SerializeField] GameObject _objectQ;
    [SerializeField] Transform _player;
    [SerializeField] float _skillCoolTime;
    [SerializeField] GameObject _guardSkill;
    public GameObject _guardSkillBool;
    [SerializeField] GameObject _guard;
    [SerializeField] float _gardSkillTime=2f;
    int _skillPoint = 1;
    InGameManager _inGameManager;
    
    public float _mousePosX;
    public float _mousePosY;
    // Start is called before the first frame update
    void Start()
    {
        //_skill1 =KeyCode.Q;
        _guardSkill.SetActive(false);
        _inGameManager = GameObject.Find("InGameManager").GetComponent<InGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Crosshair();
        if (Input.GetMouseButtonDown(0))
        {
            SkillShot1();
        }
        if (_inGameManager._score >= _skillPoint)
        {
            _guardSkill.SetActive(true);
            Invoke("GuardSkillOff",_gardSkillTime);
            _skillPoint +=_skillPoint;
        }
    }
    void SkillShot1()
    {
        Instantiate(_objectQ, _player.position, this.transform.rotation);
    }
    void GuardSkillOff()
    {
        _guardSkill.SetActive(false);
    }
    void Crosshair()
    {
        _player = _player.transform;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePosX = _player.position.x - mousePos.x;
        _mousePosY = _player.position.y - mousePos.y;
        transform.up = new Vector2(_mousePosX, _mousePosY);
    }
}
