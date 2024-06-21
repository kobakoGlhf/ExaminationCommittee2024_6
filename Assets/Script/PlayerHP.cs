using System.Collections;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [HideInInspector] public int _hitPoint;
    [SerializeField] public int _maxHP;
    [SerializeField] GameObject _destroyObj;
    [SerializeField] GameObject _movePlayer;
    [SerializeField] SkillShot _skillShot;
    [SerializeField] float _knockBack = 100;//damageのノックバック100ぐらいがちょうどいい
    [HideInInspector] public bool _invincible;
    [SerializeField] float _invincibleTime = 0.75f;
    public int _hitDamage = 1;
    Rigidbody2D _rb;
    void Start()
    {
        _rb = _movePlayer.GetComponent<Rigidbody2D>();
        _hitPoint = _maxHP;
    }
    private void Update()
    {
        if (_hitPoint <= 0) Destroy(_destroyObj);
    }
    public void HealHP(int heal)
    {
        _hitPoint += heal;
        if (_hitPoint > _maxHP)
        {
            _hitPoint = _maxHP;
        }
    }
    IEnumerator Damage(float x, float y)
    {
        _movePlayer.GetComponent<MovePlayer>().enabled = false;
        _skillShot.enabled = false;
        _invincible = true;
        _rb.AddForce(new Vector2(x * _knockBack, y * _knockBack), ForceMode2D.Impulse);
        yield return new WaitForSeconds(.1f);
        _movePlayer.GetComponent<MovePlayer>().enabled = true;
        _skillShot.enabled = true;
        yield return new WaitForSeconds(_invincibleTime);
        _invincible = false;
        yield break;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet" && _invincible == false)
        {
            _hitPoint -= _hitDamage;
            float x;
            float y;
            x = transform.position.x - collision.transform.position.x;
            y = transform.position.y - collision.transform.position.y;
            StartCoroutine(Damage(x, y));
        }
    }
}
