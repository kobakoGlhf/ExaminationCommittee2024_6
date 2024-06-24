using UnityEngine;

public class Enemy : MonoBehaviour
{
    int _bulletCreatAngleSer = 30;
    int _bulletCreatCount;
    int _bulletCreatAngle;
    [SerializeField] GameObject _bullet;
    InGameManager _inGameManager;
    GameObject _player;
    SpriteRenderer _enemySpriteRenderer;
    float _attackCoolTime = 1.8f;
    float _timer;
    public bool _beam;
    AudioSource _audioSource;
    [SerializeField] AudioClip _shotAudio;
    [SerializeField] AudioClip _diedAudio;
    private void Start()
    {
        _enemySpriteRenderer = GetComponent<SpriteRenderer>();
        _player = GameObject.Find("PlayerObj");
        _inGameManager = GameObject.Find("InGameManager").GetComponent<InGameManager>();
        _audioSource = _inGameManager.GetComponent<AudioSource>();
        _bulletCreatAngleSer = Random.Range(2, 5);
        _bulletCreatAngleSer *= 10;
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
        _timer += Time.deltaTime;
        if (_attackCoolTime < _timer && _player != null)
        {
            BulletClone(3);
            _timer = 0;
            _audioSource.PlayOneShot(_shotAudio);
        }
        if (_beam)
        {
            _bulletCreatAngle = 120;
            BulletClone(3);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            _audioSource.PlayOneShot(_diedAudio);
            _bulletCreatCount=Random.Range(3,10);
            BulletClone(_bulletCreatCount);
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

    //OnDestroyでInstantiateするとバグる(おそらく終了時にデストロイされるため判定が残る)
}
