using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] float _speed=5f;
    [SerializeField] int _maxHealth = default;
    [SerializeField]Rigidbody2D _rb;
    Vector3 _cameraEnd;
    Vector3 _cameraEndMinus;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        ////�ړ������p�@��ʒ[�̎擾
        //_cameraEndMinus=Camera.main.ViewportToWorldPoint(Vector2.zero);
        //_cameraEnd=Camera.main.ViewportToWorldPoint(Vector2.one);
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizon=Input.GetAxis("Horizontal");
        float moveVerti = Input.GetAxis("Vertical");
        _rb.velocity = new Vector2(moveHorizon * _speed, moveVerti * _speed);

        ////�ړ�����(���͕����I�ȕǂ��g�p��
        //if(_cameraEnd.x<=transform.position.x||_cameraEnd.y<=transform.position.y||
        //        _cameraEndMinus.x >= transform.position.x || _cameraEndMinus.y >= transform.position.y)
        //{

        //}

    }
    private void Health(int damage)
    {
        _maxHealth += damage;
    }
}
