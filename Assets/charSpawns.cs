using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charSpawns : MonoBehaviour
{
    public Boolean canSpawn; // boolean to be the timer for next spawn of particular unit.
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void updateCanSpawn()
    {
        canSpawn = !canSpawn;
        _animator.SetBool("isSpawned", !canSpawn);
        Debug.Log(canSpawn);
    }
}
