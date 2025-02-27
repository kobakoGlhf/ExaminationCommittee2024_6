using System.Collections;
using UnityEngine;

public class SkillShot : MonoBehaviour
{
    [SerializeField] GameObject _objectQ;
    [SerializeField] Transform _player;
    [SerializeField] float _skillCoolTime;
    [SerializeField] GameObject _guardSkill;
    [SerializeField] float _gardSkillTime = 1f;

    [HideInInspector] public int _gardSkillCoolCount;
    [SerializeField] int _skillPoint = 1;
    int _skillPointCount;
    [HideInInspector] public bool _gardActive;
    [SerializeField]AudioSource _audioSource;
    [SerializeField]AudioClip _shotAudio;
    public float _mousePosX;
    public float _mousePosY;
    float _timer;
    float _cooltime=0.3f;
    // Start is called before the first frame update
    void Start()
    {
        //_skill1 =KeyCode.Q;
        _guardSkill.SetActive(false);
        _guardSkill.gameObject.tag = "Untagged";
        _skillPointCount = _skillPoint;
        _timer=_cooltime;
    }

    // Update is called once per frame
    void Update()
    {
        Crosshair();
        _timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0)&&_cooltime<_timer)
        {
            SkillShot1();
            _audioSource.PlayOneShot(_shotAudio);
            _timer = 0;
        }
        if (_gardSkillCoolCount >= _skillPointCount)
        {
            _gardActive = true;
            //if (_guardSkill != null && Input.GetKeyDown(KeyCode.E))
            //{
            //    StartCoroutine(gardSkillCol(_gardSkillTime));
            //}
        }
    }
    IEnumerator gardSkillCol(float i)
    {
        GardSkill attack = _guardSkill.GetComponent<GardSkill>();
        _guardSkill.SetActive(true);
        attack._attack = true;
        yield return null;
        attack._attack = false;
        yield return new WaitForSeconds(i);
        attack._attack = true;
        yield return new WaitForSeconds(.1f);
        attack._attack = false;
        _guardSkill.SetActive(false);
        _skillPointCount += _skillPoint;
        _gardActive = false;
        yield break;
    }
    void SkillShot1()
    {
        GameObject newObj = Instantiate(_objectQ, _player.position, this.transform.rotation);
        newObj.GetComponent<Skill>()._playerS = this;
    }
    public Vector2 Crosshair()
    {
        _player = _player.transform;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePosX = _player.position.x - mousePos.x;
        _mousePosY = _player.position.y - mousePos.y;
        transform.up = new Vector2(_mousePosX, _mousePosY);
        return new Vector2(_mousePosX, _mousePosY);
    }
}
