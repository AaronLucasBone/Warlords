using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spawns : MonoBehaviour
{
    [Header("Transforms")]
    public Vector2[] spawnVectors; //human 0, orc 1
    private float spawnOffset = 5.5f;
    
    public List<GameObject> _human, _horde;

    private void Start()
    {
        spawnVectors[0] = new Vector2(this.gameObject.transform.position.x - spawnOffset, this.gameObject.transform.position.y);
        spawnVectors[1] = new Vector2(this.gameObject.transform.position.x + spawnOffset, this.gameObject.transform.position.y);
    }
    
    public void spawn(GameObject unit, int i)
    {
        GameObject u = Instantiate(unit, spawnVectors[i], Quaternion.identity); //spawns given unit at given side. 
        if (i == 0)
        {
            _human.Add(u);
        }
        else if (i == 1)
        {
            _horde.Add(u);
        }

    }

}
