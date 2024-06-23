using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int _bulletCreatAngleSer = 30;
    [SerializeField] int _bulletCreatCount;
    int _bulletCreatAngle;
    [SerializeField] GameObject _bullet;
    InGameManager _inGameManager;
    GameObject _player;
    SpriteRenderer _enemySpriteRenderer;
    [SerializeField] float _attackCoolTime=3;
    float _timer;
    private void Start()
    {
        _enemySpriteRenderer = GetComponent<SpriteRenderer>();
        _player = GameObject.Find("PlayerObj");
        _inGameManager = GameObject.Find("InGameManager").GetComponent<InGameManager>();
        if (_bulletCreatAngleSer == 0)
        {
            _bulletCreatAngle = 360 / 1;
        }
        else
        {
            _bulletCreatAngle = 360 / _bulletCreatAngleSer;
        }
    }
    private void Update()
    {
        if (_player != null)
        {
            var enemyFlipX = _player.gameObject.transform.position.x - transform.position.x > 0;
            if (enemyFlipX)
            {
                _enemySpriteRenderer.flipX = false;
            }
            else
            {
                _enemySpriteRenderer.flipX = true;
            }
        }
        //obj.transform.localEulerAngles = pos;
        if (Input.GetKeyUp(KeyCode.Y))
        {
            BulletClone(_bulletCreatCount, false);
        }
        _timer+=Time.deltaTime;
        if (_attackCoolTime < _timer && _player!=null)
        {
            BulletClone(3, true);
            _timer = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            BulletClone(_bulletCreatCount, true);
        }
    }
    private float PlayerAngle()
    {
        float dig = 0;
        if (_player != null)
        {
            Vector2 angle = transform.position - _player.gameObject.transform.position;
            dig = Mathf.Atan2(angle.x, angle.y) * Mathf.Rad2Deg;
        }
        return -dig;
    }
    private void OnDestroy()
    {
        _inGameManager._score += 1;
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
                    var obj = Instantiate(_bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    obj.transform.localEulerAngles = pos;
                }
                else if (j % 2 == 0 && j != 0)
                {
                    Vector3 pos = transform.localEulerAngles;
                    pos.z = PlayerAngle() + i;
                    var obj = Instantiate(_bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    obj.transform.localEulerAngles = pos;
                }
                else
                {

                    Vector3 pos = transform.localEulerAngles;
                    pos.z = PlayerAngle() + -i;
                    var obj = Instantiate(_bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    obj.transform.localEulerAngles = pos;
                    i -= _bulletCreatAngle;
                }
                j++;
            }

        }
        else
        {
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
