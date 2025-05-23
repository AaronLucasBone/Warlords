using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public int totalHp;

    int takeDamage(int damage)
    {
        totalHp =-damage;
        return totalHp;
    }

}
