using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class movement : MonoBehaviour
{
    [Header("Movement")]
    private Rigidbody2D rb;     //rb for movement
    public Boolean enemyFound;  //check for in combat
    private Animator _animator;

    [Header("Stats")]
    public float hp;            //current health
    public float pace;          //pace
    public float vision;        //vision

    public bool isHuman;        //details alignment

    [Header("Polarity")]
    public int polarity;        //polarity describes whether or not something is facing right/left.

    [Header("Raycast Hits")]
    public float offset;                //offset for Raycasts, etc so they don't self collide.
    public Vector3 transformOffset;
    public String tagToFind;            //needs to be horde or humans
    [SerializeField] LayerMask mask;

    [Header("Attack GameObjects")]
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject physical;
    [SerializeField] GameObject currentEnemy;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        assignAlignment();

        startMarch();

    }//end start

    private void Update()
    {
        sight();

    }//end update

    void startMarch()
    {
        InvokeRepeating("march", 1, 1);
    }

    void march()
    {
        if (enemyFound == false)
        {
            _animator.SetBool("isMarching", true);
            _animator.SetBool("isAttacking", false);

            rb.velocity = new Vector2(pace*polarity, 0);
        }
        
    }//end march
    
    void sight()
    {
        offsetAdjust();
        
        RaycastHit2D hit = Physics2D.Raycast(transformOffset, new Vector2(polarity, 0), vision);
        Debug.DrawLine(transformOffset, new Vector3(transformOffset.x + (vision*polarity), transformOffset.y, transformOffset.z), Color.red);
        
        if (hit == true && hit.collider.gameObject.tag==tagToFind)
        {
            Debug.Log(hit.collider.gameObject.tag + ", " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.tag == tagToFind) //raycast
            {
                rb.velocity = Vector2.zero;
                enemyFound = true;

                currentEnemy = hit.collider.gameObject;

                _animator.SetBool("isMarching", false);
                _animator.SetBool("isAttacking", true);
                
            }

        }
        else if(currentEnemy==null)
        {
            enemyFound = false;
            _animator.SetBool("isMarching", true);
            //pace = 1.05f;
        }

    }//end sight

    void offsetAdjust()
    {
        transformOffset = new Vector3((this.gameObject.transform.position.x + (offset * polarity)), this.gameObject.transform.position.y, this.transform.position.z);
    }

    IEnumerator Delay(int n)
    {
        yield return new WaitForSeconds(n);
    }

    void generateAttack()
    {
      
        if (projectile != null)
        {
            GameObject pro = Instantiate(projectile, transformOffset, Quaternion.identity, this.transform);
            pro.transform.localScale = new Vector3 (.75f,.75f,.75f);
        }
        if (physical != null)
        {
            Instantiate(physical, transformOffset, Quaternion.identity);
        }
        
    }

    public void deleteEntity()
    {
        Destroy(this.gameObject);
    }

    public void assignAlignment()
    {
        if (isHuman)
        {
            polarity = 1;
            tagToFind = "horde";
            mask = LayerMask.GetMask("horde");
        }
        else
        {
            polarity = -1;
            tagToFind = "humans";
            mask = LayerMask.GetMask("humans");

        }
    }


}
