using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]public int _hitPoint=10;
    [SerializeField] int _maxHP;
    [SerializeField] GameObject _destroyObj;
    [SerializeField]Effector2D _effector;
    [SerializeField]GameObject _movePlayer;
    [SerializeField]SkillShot _skillShot;
    [SerializeField] float _knockBack=100;//damageのノックバック100ぐらいがちょうどいい
    [HideInInspector]
    public bool _invincible;
    [SerializeField] float _invincibleTime=0.75f;
    public int _hitDamage=1;
    Rigidbody2D _rb;
    void Start()
    {
        _rb = _movePlayer.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(_hitPoint<=0)Destroy(_destroyObj);
    }
    public void HealHP(int heal)
    {
        _hitPoint += heal;
        if(_hitPoint > _maxHP)
        {
            _hitPoint=_maxHP;
        }
    }
    IEnumerator Damage(float x,float y)
    {
        _movePlayer.GetComponent<MovePlayer>().enabled = false;
        _skillShot.enabled = false;
        _rb.AddForce(new Vector2(x*_knockBack,y*_knockBack),ForceMode2D.Impulse);
        yield return new WaitForSeconds(.1f);
        
        _movePlayer.GetComponent<MovePlayer>().enabled=true;
        _skillShot.enabled=true;
        _invincible = true;
        yield return new WaitForSeconds(_invincibleTime);
        _invincible=false;
        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet"&&_invincible==false)
        {
            _hitPoint -= _hitDamage;
            float x;
            float y;
            x=transform.position.x-collision.transform.position.x;
            y=transform.position.y-collision.transform.position.y;

            StartCoroutine(Damage(x,y));
        }
    }
}
