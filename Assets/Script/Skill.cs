using UnityEngine;

public class Skill : MonoBehaviour
{
    //[SerializeField] float _lifeTime=2;
    [SerializeField] public float _speed = 5;
    Rigidbody2D _rb;
    SkillShot _skillShot;
    [HideInInspector]
    public SkillShot _playerS;
    float _timer;
    float _destroyTime=5;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_skillShot != null)
        {
            _skillShot = GameObject.Find("Skill").GetComponent<SkillShot>();//カーソル位置の取得
        }
        Mazzle();
    }
    private void Update()
    {
        _timer+=Time.deltaTime;
        if (_timer > _destroyTime)
        {
            Destroy(gameObject);
        }
    }
    void Mazzle()
    {
        _rb.velocity = this.transform.up * -_speed;
        //Destroy(gameObject, _lifeTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag != "PlayerBullet")
        {
            if (collision.tag == "Wall" || collision.tag == "Player")
            {
                Destroy(gameObject);
            }
        }
        if (gameObject.tag == "PlayerBullet")
        {
            if (collision.tag == "Target" || collision.tag == "Boss")
            {
                Destroy(gameObject);
                _playerS._gardSkillCoolCount += 1;
            }
        }
        //if (collision.gameObject.tag == "Wall"||collision.gameObject.tag==_hitObjectTagName) Destroy(gameObject);
        if (collision.gameObject.tag == "Target" && gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
