using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : MonoBehaviour
{
    //[SerializeField] float _lifeTime=2;
    [SerializeField] float _speed=5;
    [SerializeField] string _hitObjectTagName="Player";
    Rigidbody2D _rb;
    SkillShot _skillShot;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_skillShot != null)
        {
            _skillShot = GameObject.Find("Skill").GetComponent<SkillShot>();//カーソル位置の取得
        }
        Mazzle();
    }
    void Mazzle()
    {
        
        _rb.velocity = this.transform.up * -_speed;
        //Destroy(gameObject, _lifeTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall"||collision.gameObject.tag==_hitObjectTagName) Destroy(gameObject);
        if (collision.gameObject.tag == "Target" && gameObject.tag == "PlayerBullet") 
        {
            Destroy(collision.gameObject);
        }
    }
}
