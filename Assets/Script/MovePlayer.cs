using System.Collections;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] Rigidbody2D _rb;
    public int _killSkillCoolDown;
    [SerializeField] float _blink = 2;
    [SerializeField] SpriteRenderer _PlayerSpriteRenderer;
    bool _cor;
    [SerializeField] SkillShot _shot;
    [SerializeField] PlayerHP _hp;
    [SerializeField] PlayerAnimator _plyaerAnim;
    bool _noFilp;
    //Vector3 _cameraEnd;
    //Vector3 _cameraEndMinus;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        //移動制限用　画面端の取得
        //_cameraEndMinus=Camera.main.ViewportToWorldPoint(Vector2.zero);
        //_cameraEnd=Camera.main.ViewportToWorldPoint(Vector2.one);
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizon = Input.GetAxis("Horizontal");
        float moveVerti = Input.GetAxis("Vertical");
        if (_noFilp == false)
        {
            FlipChange(moveHorizon);
        }
        MoveAnimetion(moveHorizon, moveVerti);
        if (_cor == false)
        {
            _rb.velocity = new Vector2(moveHorizon * _speed, moveVerti * _speed);
            //ブリンクの実装
            if (Input.GetMouseButtonDown(1))//いつかGetAxisに変える
            {
                //StartCoroutine(BlinkCol(horizon,verti));
                //StartCoroutine(BlinkColMouse(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y));
                StartCoroutine(BlinkColMouse());
                _plyaerAnim._animationIndex = 2;
                Invoke("AnimationReset", .3f);
            }
        }
        ////移動制限(今は物理的な壁を使用中
        //if(_cameraEnd.x<=transform.position.x||_cameraEnd.y<=transform.position.y||
        //        _cameraEndMinus.x >= transform.position.x || _cameraEndMinus.y >= transform.position.y)
    }
    IEnumerator BlinkColMouse()
    {
        _cor = true;
        _hp._invincible = true;
        Vector2 mousePos = MouseCorsolAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        _rb.velocity = mousePos * _blink;
        if (mousePos.x <= 0)
        {
            _PlayerSpriteRenderer.flipX = true;
            _noFilp = true;
        }
        yield return new WaitForSeconds(.08f);
        _cor = false;
        _hp._invincible = false;
        yield return new WaitForSeconds(.1f);
        _noFilp = false;
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
            _PlayerSpriteRenderer.flipX = false;
        }
        else
        {
            _PlayerSpriteRenderer.flipX = true;
        }
    }
    void MoveAnimetion(float x, float y)
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
