using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardSkill : MonoBehaviour
{
    [SerializeField] SkillShot _skillShot;
    public bool _attack;
    private void Start()
    {
        _skillShot.gameObject.GetComponent<SkillShot>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
        }
        if(collision.tag == "Target" && _attack)
        {
            Destroy(collision.gameObject);
        }
    }
}
