using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    public menuHandler _menuHandler;
    
    public GameObject[] enemyLoadout;
    public GameObject[] lanes;

    public int[] humanCount, hordeCount;
    public int[] difference;

    // Start is called before the first frame update
    void Start()
    {
        lanes = _menuHandler.Lanes;
        InvokeRepeating("randomSpawn", 1, 4);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

//Modes of Operation ----------------------------------
    
    /**
     * This method's sole purpose is to create a mapping of the enemy units on across all lanes. 
     * POSITIVE --> humans > horde
     * NEGATIVE --> humans < horde
     */

    void checkLanes()
    {
        for (int i = 0; i < lanes.Length; i++)
        {
            humanCount[i] = lanes[i].GetComponentInParent<spawns>()._human.Count;
            hordeCount[i] = lanes[i].GetComponentInParent<spawns>()._horde.Count;

            //Debug.Log(humanCount[i] + "-" + hordeCount[i] + ", " + "Difference: " + difference[i]);
            
            difference[i] = humanCount[i]-hordeCount[i];

        }
    }

    /**
     * Checks lanes 0 and 5 to see if there's any current units in the lane. If not, it will send a fast unit to flank said lane.
     */
    void sendScout()
    {

    }

    /**
     * Method checks across all lanes to see if there's any lanes massively outnumbered.
     */
    void sendReinforcement()
    {

    }

    /**
     * Chaos function. Chooses random lane.
     */
    void randomSpawn()
    {
        int rLane = Random.Range(0, lanes.Length);
        int rBarracks = Random.Range(0, enemyLoadout.Length);
        lanes[rLane].GetComponentInParent<spawns>().spawn(enemyLoadout[rBarracks], 1);

        checkLanes();

    }

    /**
     * 
     */
    void plusOne()
    {

    }

}
