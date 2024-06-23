using System.Collections;
using UnityEngine;
using UnityEngineInternal;

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
    [SerializeField] GameObject _diedEffect;
    public int _hitDamage = 1;
    PlayerAnimator _animator;
    Rigidbody2D _rb;
    void Start()
    {
        _rb = _movePlayer.GetComponent<Rigidbody2D>();
        _hitPoint = _maxHP;
        _animator=GetComponent<PlayerAnimator>();
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
        _animator._animationIndex = 3;
        yield return new WaitForSeconds(.05f);
        _animator._animationIndex = 0;
        yield return new WaitForSeconds(.05f);
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
            if (_hitPoint <= 0)
            {
                Instantiate(_diedEffect, transform.position,Quaternion.identity);
                Destroy(_destroyObj);
            }
            float x;
            float y;
            x = transform.position.x - collision.transform.position.x;
            y = transform.position.y - collision.transform.position.y;
            StartCoroutine(Damage(x, y));
        }
    }
}
