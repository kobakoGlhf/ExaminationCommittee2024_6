using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class BossEnemy : MonoBehaviour
{
    GameObject _player;
    Enemy _enemy;
    [SerializeField] GameObject _prefab;
    GameObject _beem;
    [SerializeField] int _hitPoint=5;
    InGameManager _ingameManager;
    Tween _beemTween;
    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _player = GameObject.Find("PlayerObj");
        _ingameManager=GameObject.FindObjectOfType<InGameManager>();
        float dig = 0;
        if (_player != null)
        {
            Vector2 angleP = transform.position - _player.gameObject.transform.position;
            dig = Mathf.Atan2(angleP.x, angleP.y) * Mathf.Rad2Deg;
        }
        float angle = dig;
        _beem  = Instantiate(_prefab, transform.position, Quaternion.identity);
        _beem.transform.localEulerAngles = new Vector3(0, 0, dig + 90);
        _beemTween = _beem.transform.DORotate(new Vector3(0, 0, dig+90+180), 7f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }
    private void Update()
    {
        _enemy.PlayerFlip();
        if (_hitPoint <= 0)
        {
            _ingameManager._score += 5;
            _ingameManager._bossReSpawn = false;
            Destroy(_beem);
            Destroy(this.gameObject);
            _beemTween.Kill();
        }
        if (_player == null)
        {
            _beemTween.Kill();
            Destroy(_beem);
        }
    }
    private void OnDestroy()
    {
        _beemTween.Kill();
        Destroy(_beem);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            _hitPoint--;
            _enemy.BulletClone(3);
        }
    }
}
