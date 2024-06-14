using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] int _bulletCreatAmount;
    [SerializeField] GameObject _bullet;
    InGameManager _inGameManager;
    private void Start()
    {
        _inGameManager = GameObject.Find("InGameManager").GetComponent<InGameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag =="PlayerBullet")
        {
            for (float i = 0; i < 360; i++)
            {
                if (i % _bulletCreatAmount == 0)
                {
                    Vector3 pos = transform.localEulerAngles;
                    pos.z = i;
                    var obj = Instantiate(_bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    obj.transform.localEulerAngles = pos;
                }
            }
        }
    }
    private void OnDestroy()
    {
        _inGameManager._score += 1;
    }
    //OnDestroyでInstantiateするとバグる(おそらく終了時にデストロイされるため判定が残る)
}
