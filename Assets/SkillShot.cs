using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShot : MonoBehaviour
{
    [SerializeField] GameObject _objectQ;
    [SerializeField] Transform _player;
    [SerializeField] float _skillCoolTime;
    
    float _mousePosX;
    float _mousePosY;
    // Start is called before the first frame update
    void Start()
    {
        //_skill1 =KeyCode.Q;
        
    }

    // Update is called once per frame
    void Update()
    {
        Crosshair();
        if (Input.GetMouseButtonDown(0))
        {
            SkillShot1();
        }
    }
    void SkillShot1()
    {
        Instantiate(_objectQ, _player.position, this.transform.rotation);
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
