using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardSkill : MonoBehaviour
{
    [SerializeField] SkillShot _skillShot;
    private void Start()
    {
        _skillShot.gameObject.GetComponent<SkillShot>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
        }
        if(collision.tag == "Target")
        {
            Destroy(collision.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
