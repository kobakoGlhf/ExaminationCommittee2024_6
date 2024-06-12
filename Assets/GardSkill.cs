using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardSkill : MonoBehaviour
{
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
