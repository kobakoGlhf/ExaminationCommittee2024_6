using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] int _bulletCreatAmount;
    [SerializeField] GameObject _bullet;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag!="Player"&&collision.tag!="EnemyBullet")
        for (float i = 0; i < 360; i++)
        {
            if(i %_bulletCreatAmount == 0)
            {
                Vector3 pos=transform.localEulerAngles;
                pos.z = i;
                var obj= Instantiate(_bullet, new Vector2(transform.position.x, transform.position.y),Quaternion.identity);
                obj.transform.localEulerAngles=pos;
            }
        }
    }
    //OnDestroyでInstantiateするとバグる(おそらく終了時にデストロイされるため)
}
