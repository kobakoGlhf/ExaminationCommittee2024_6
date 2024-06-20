using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class Enemy : MonoBehaviour
{
    [SerializeField] int _bulletCreatAngleSer=30;
    [SerializeField] int _bulletCreatCount;
    int _bulletCreatAngle;
    [SerializeField] GameObject _bullet;
    InGameManager _inGameManager;
    GameObject _player;
    private void Start()
    {
        _player = GameObject.Find("PlayerObj");
        _inGameManager = GameObject.Find("InGameManager").GetComponent<InGameManager>();
        if (_bulletCreatAngleSer == 0) _bulletCreatAngle = 360 / 1;
        else _bulletCreatAngle =360/_bulletCreatAngleSer;
        Debug.Log(_bulletCreatAngle);
    }
    private void Update()
    {
        //var obj = Instantiate(_bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        //Vector3 pos = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -PlayerAngle());
        //obj.transform.localEulerAngles = pos;
        if(Input.GetKeyUp(KeyCode.Y))
        {
            BulletClone(_bulletCreatCount, false);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag =="PlayerBullet")
        {
            BulletClone(_bulletCreatCount,true);
        }
    }
    private float PlayerAngle()
    {
        float dig = 0;
        if (_player != null)
        {
            Vector2 a = transform.position - _player.gameObject.transform.position;
            dig = Mathf.Atan2(a.x, a.y) * Mathf.Rad2Deg;
        }
        return -dig;
    }
    private void OnDestroy()
    {
        _inGameManager._score += 1;
    }
    void BulletClone(int count)
    {
        float j = 0;
        for (float i = 0; i < 360 && count > 0; i += _bulletCreatAngle)
        {
            count--;
            if (j == 0)
            {
                Vector3 pos = transform.localEulerAngles;
                pos.z = PlayerAngle();
                Debug.Log(pos.z);
                var obj = Instantiate(_bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                obj.transform.localEulerAngles = pos;
            }
            else if (j % 2 == 0 && j != 0)
            {
                Vector3 pos = transform.localEulerAngles;
                pos.z = PlayerAngle() + i;
                Debug.Log(pos.z);
                var obj = Instantiate(_bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                obj.transform.localEulerAngles = pos;
            }
            else
            {

                Vector3 pos = transform.localEulerAngles;
                pos.z = PlayerAngle() + -i;
                Debug.Log(pos.z);
                var obj = Instantiate(_bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                obj.transform.localEulerAngles = pos;
                i -= _bulletCreatAngle;
            }
            j++;
        }

    }
        void BulletClone(int count, bool playerTarget)
    {
        if (playerTarget)
        {
            float j = 0;
            for (float i = 0; i < 360 && count > 0; i += _bulletCreatAngle)
            {
                count--;
                if (j == 0)
                {
                    Vector3 pos = transform.localEulerAngles;
                    pos.z = PlayerAngle();
                    Debug.Log(pos.z);
                    var obj = Instantiate(_bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    obj.transform.localEulerAngles = pos;
                }
                else if (j % 2 == 0 && j != 0)
                {
                    Vector3 pos = transform.localEulerAngles;
                    pos.z = PlayerAngle() + i;
                    Debug.Log(pos.z);
                    var obj = Instantiate(_bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

                    obj.transform.localEulerAngles = pos;
                }
                else
                {

                    Vector3 pos = transform.localEulerAngles;
                    pos.z = PlayerAngle() + -i;
                    Debug.Log(pos.z);
                    var obj = Instantiate(_bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    obj.transform.localEulerAngles = pos;
                    i -= _bulletCreatAngle;
                }
                j++;
            }

        }
        else {
            var harding = _player.transform.position - this.gameObject.transform.position;
            var dis = harding.magnitude;
            var dire = harding / dis;
            for (float i = 0; i < 360; i++)
            {
                if (i % _bulletCreatAngle == 0)
                {
                    Vector3 pos = transform.localEulerAngles;
                    pos.z = i + dire.z;
                    var obj = Instantiate(_bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    obj.transform.localEulerAngles = pos;
                }
            }
        }
    }
    //OnDestroyでInstantiateするとバグる(おそらく終了時にデストロイされるため判定が残る)
}
