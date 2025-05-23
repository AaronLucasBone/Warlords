using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class damageDealt : MonoBehaviour
{
    public float damage;
    public Animator _anim;
    
    private void Start()
    {
        
    }

    private void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "backwall")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponentInParent<movement>().isHuman)
        {
            if (collision.gameObject.tag == "horde")
            {
                landHit(collision.gameObject);
            }
            
        }
        else if (GetComponentInParent<movement>().isHuman == false)
        {
            if (collision.gameObject.tag == "humans")
            {
                landHit(collision.gameObject);
            }
        }
         
        /*
        if (collision.gameObject.tag == "horde" || collision.gameObject.tag == "humans")
        {
            dealDamage(collision.gameObject);
            Debug.Log(this.gameObject.name);
            Destroy(this.gameObject);
        }*/
    }

    void dealDamage(GameObject collision)
    {
        _anim = collision.GetComponent<Animator>();
        collision.gameObject.GetComponent<movement>().hp = collision.gameObject.GetComponent<movement>().hp - damage;
        _anim.SetBool("isHurt", true);
        if (collision.gameObject.GetComponent<movement>().hp <= 0)
        {
            resetBools(_anim);

            _anim.SetBool("isDead", true);
        }

        _anim.SetBool("isHurt", false);

    }

    void landHit(GameObject collision)
    {
        dealDamage(collision.gameObject);
        //Debug.Log(this.gameObject.name);
        Destroy(this.gameObject);
    }

    void resetBools(Animator _anim)
    {
        _anim.SetBool("isMarching", false);
        _anim.SetBool("isAttacking", false);
        _anim.SetBool("isHurt", false);
        _anim.SetBool("isDead", false);
    }
  
}
