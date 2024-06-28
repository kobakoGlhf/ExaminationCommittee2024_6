using System.Collections;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    Rigidbody2D _rb;
    public int _killSkillCoolDown;
    [SerializeField] float _blinkRange = 40;
    [SerializeField] SpriteRenderer _playerSpriteRenderer;
    bool _cor;
    [SerializeField] SkillShot _shot;
    [SerializeField] PlayerHP _hp;
    [SerializeField] PlayerAnimator _plyaerAnim;
    [SerializeField] float _skillCoolTimerBring=1f;
    float _timer;
    bool _cantFilp;
    [SerializeField] AudioSource _audioSource;
    [SerializeField]AudioClip _blinkAudio;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _timer = _skillCoolTimerBring;
    }
    void Update()
    {
        _timer+=Time.deltaTime;
        float moveHorizon = Input.GetAxis("Horizontal");
        float moveVerti = Input.GetAxis("Vertical");
        if (_cantFilp == false)
        {
            FlipChange(moveHorizon);
        }
        MoveAnimetion();
        if (_cor == false)
        {
            _rb.velocity = new Vector2(moveHorizon * _speed, moveVerti * _speed);
            if (Input.GetMouseButtonDown(1) && _timer > _skillCoolTimerBring)//‚¢‚Â‚©GetAxis‚É•Ï‚¦‚é
            {
                StartCoroutine(BlinkCoroutineMousePos());
                _plyaerAnim._animationIndex = 2;
                Invoke("AnimationReset", .3f);
                _timer = 0;
                if (_blinkAudio != null)
                {
                    _audioSource.PlayOneShot(_blinkAudio);
                }
            }
        }
    }
    IEnumerator BlinkCoroutineMousePos()
    {
        _cor = true;
        _hp._invincible = true;
        Vector2 mousePos = MouseCorsolAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        _rb.velocity = mousePos * _blinkRange;
        if (mousePos.x <= 0)
        {
            _playerSpriteRenderer.flipX = true;
            _cantFilp = true;
        }
        else
        {
            _playerSpriteRenderer.flipX = false;
            _cantFilp = true;
        }
        yield return new WaitForSeconds(.08f);
        _cor = false;
        _hp._invincible = false;
        yield return new WaitForSeconds(.1f);
        _cantFilp = false;
        yield break;
    }
    Vector2 MouseCorsolAngle(Vector3 CorsolPos)
    {
        float rad = 0;
        if (CorsolPos != null)
        {
            Vector2 a = transform.position - CorsolPos;
            rad = Mathf.Atan2(-a.x, -a.y);
        }
        return new Vector2(Mathf.Sin(rad), Mathf.Cos(rad)).normalized;
    }
    void FlipChange(float x)
    {
        if (0 <= x)
        {
            _playerSpriteRenderer.flipX = false;
        }
        else
        {
            _playerSpriteRenderer.flipX = true;
        }
    }
    void MoveAnimetion()//GetAxis‚¾‚ÆƒL[‚ð—£‚µ‚Ä‚àŠ®‘S‚É’âŽ~‚·‚é‚Ü‚Åanimation‚ªÄ¶‚³‚ê‚é‚½‚ßGetKey
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D)) && _plyaerAnim._animationIndex != 2)
        {
            _plyaerAnim._animationIndex = 1;
        }
        else AnimationReset();
    }
    void AnimationReset()
    {
        _plyaerAnim._animationIndex = 0;
    }
}
