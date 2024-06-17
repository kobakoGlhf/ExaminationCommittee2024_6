using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShot : MonoBehaviour
{
    [SerializeField] GameObject _objectQ;
    [SerializeField] Transform _player;
    [SerializeField] float _skillCoolTime;
    [SerializeField] GameObject _guardSkill;
    [SerializeField] float _gardSkillTime=1f;
    [SerializeField] int _skillPoint = 1;
    int _skillPointCount;
    InGameManager _inGameManager;
    
    public float _mousePosX;
    public float _mousePosY;
    // Start is called before the first frame update
    void Start()
    {
        //_skill1 =KeyCode.Q;
        _guardSkill.SetActive(false);
        _guardSkill.gameObject.tag = "Untagged";
        _skillPointCount = _skillPoint;
        _inGameManager = GameObject.Find("InGameManager").GetComponent<InGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Crosshair();
        if (Input.GetMouseButtonDown(0))
        {
            SkillShot1();
        }
        if (_inGameManager._score >= _skillPointCount && _guardSkill != null)
        {
            StartCoroutine(gardSkillCol(_gardSkillTime));
            _skillPointCount += _skillPoint;//ƒŠƒZƒbƒg
        }
    }
    IEnumerator gardSkillCol(float i)
    {
        GardSkill attack=_guardSkill.GetComponent<GardSkill>();
        _guardSkill.SetActive(true);
        yield return null;
        yield return new WaitForSeconds(i);
        attack._attack = true;
        yield return new WaitForSeconds(.1f);
        attack._attack = false;
        _guardSkill.SetActive(false);
        yield break;
    }
    void SkillShot1()
    {
        Instantiate(_objectQ, _player.position, this.transform.rotation);
    }
    Vector2 Crosshair()
    {
        _player = _player.transform;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePosX = _player.position.x - mousePos.x;
        _mousePosY = _player.position.y - mousePos.y;
        transform.up = new Vector2(_mousePosX, _mousePosY);
        return new Vector2(_mousePosX, _mousePosY);
    }
}
