using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]public int _hitPoint=10;
    [SerializeField] int _maxHP;
    [SerializeField] GameObject _destroyObj;
    public int _hitDamage=1;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            _hitPoint -= _hitDamage;
        }
    }
}
