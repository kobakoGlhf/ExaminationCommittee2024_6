using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class Enemy : MonoBehaviour
{
    [SerializeField] int _bulletCreatAmount;
    [SerializeField] GameObject _bullet;
    InGameManager _inGameManager;
    GameObject _player;
    private void Start()
    {
        _player = GameObject.Find("PlayerObj");
        _inGameManager = GameObject.Find("InGameManager").GetComponent<InGameManager>();
    }
    private void Update()
    {
        var obj = Instantiate(_bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        Vector3 pos = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -Angule());
        obj.transform.localEulerAngles = pos;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag =="PlayerBullet")
        {
            var harding=_player.transform.position-this.gameObject.transform.position;
            var dis=harding.magnitude;
            var dire=harding/dis;
            for (float i = 0; i < 360; i++)
            {
                if (i % _bulletCreatAmount == 0)
                {
                    Vector3 pos = transform.localEulerAngles;
                    pos.z = i+dire.z;
                    var obj = Instantiate(_bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    obj.transform.localEulerAngles = pos;
                }
            }
        }
    }
    private float PlayerAngle()
    {
        Vector2 a=transform.position-_player.gameObject.transform.position;
        float dig=Mathf.Atan2(a.x,a.y)*Mathf.Rad2Deg;
        return dig;
    }
    private void OnDestroy()
    {
        _inGameManager._score += 1;
    }
    //OnDestroyでInstantiateするとバグる(おそらく終了時にデストロイされるため判定が残る)
}
