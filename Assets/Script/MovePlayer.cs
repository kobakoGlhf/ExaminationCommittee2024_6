using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] float _speed=5f;
    [SerializeField]Rigidbody2D _rb;
    public int _killSkillCoolDown;
    [SerializeField] float _blink = 2;
    bool _cor;
    [SerializeField]SkillShot _shot;
    [SerializeField]PlayerHP _hp;
    //Vector3 _cameraEnd;
    //Vector3 _cameraEndMinus;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        //移動制限用　画面端の取得
        //_cameraEndMinus=Camera.main.ViewportToWorldPoint(Vector2.zero);
        //_cameraEnd=Camera.main.ViewportToWorldPoint(Vector2.one);
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizon=Input.GetAxis("Horizontal");
        float moveVerti = Input.GetAxis("Vertical");
        int horizon;
        int verti = 0;
        if (moveHorizon >= 0) horizon = 1;
        else horizon = -1;
        if (moveVerti > 0) verti = 1;
        else if (moveVerti < 0) verti = -1;
        if (_cor == false)
        {
            _rb.velocity = new Vector2(moveHorizon * _speed, moveVerti * _speed);

            //ブリンクの実装
            if (Input.GetMouseButtonDown(1))//いつかGetAxisに変える
            {
                StartCoroutine(BlinkCol(horizon,verti));
            }
        }


        ////移動制限(今は物理的な壁を使用中
        //if(_cameraEnd.x<=transform.position.x||_cameraEnd.y<=transform.position.y||
        //        _cameraEndMinus.x >= transform.position.x || _cameraEndMinus.y >= transform.position.y)
        //{

        //}
    }


    IEnumerator BlinkCol(float x,float y)
    {
        if ((x == 1 || x == -1) && (y == 1 || y == -1))
        {
            x *= 0.75f;
            y *= 0.75f;
        }
        _cor = true;
        _rb.velocity =new Vector2(x*_blink,y*_blink);
        _hp._invincible=true;
        yield return new WaitForSeconds(.1f);
        _cor=false;
        _hp._invincible=false;
        yield break;
    }
    IEnumerator BlinkColMouse(float x, float y)
    {
        if ((x == 1 || x == -1) && (y == 1 || y == -1))
        {
            x *= 0.75f;
            y *= 0.75f;
        }
        float Ax = x / y;
        float Ay = y / x;
        Debug.Log(-_shot._mousePosX);
        Debug.Log(-_shot._mousePosY);
        _cor = true;
        _rb.velocity = new Vector2(Ax, Ay);
        yield return new WaitForSeconds(.1f);
        _cor = false;
        yield break;
    }
}
