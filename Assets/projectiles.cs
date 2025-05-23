using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectiles : MonoBehaviour
{
    [Header("ProjectileType")]
    public bool magic;


    [Header("Generics")]
    public float speed;
    private Rigidbody2D rb;
    public int polarity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * speed * polarity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
