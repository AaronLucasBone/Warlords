using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class menuHandler : MonoBehaviour
{
    [Header("Lanes")]
    public GameObject[] Lanes;
    public int currentSelection; //Lane

    [Header("UnitSelection")]
    public GameObject[] barracks;
    public int unitSelection;
    [SerializeField] GameObject[] spawnTimers;

    [Header("Unit Icons")]
    public GameObject[] borderIcons;

    [Header("Highlight")]
    public Sprite unitHighlight;
    public Sprite borderSprite;

    private void Start()
    {
        borderIcons[0].gameObject.GetComponentInParent<SpriteRenderer>().sprite = unitHighlight;
        borderIcons[0].gameObject.GetComponentInParent<SpriteRenderer>().sortingOrder++;
        updateIcons();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) ^ (Input.GetKeyDown(KeyCode.UpArrow)))
        {
            for (int i = 0; i < Lanes.Length; i++)
            {
                Lanes[i].SetActive(false);
            }

            currentSelection++;
            currentSelection %= Lanes.Length;

            Lanes[currentSelection].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.S) ^ (Input.GetKeyDown(KeyCode.DownArrow)))
        {
            for (int i = 0; i < Lanes.Length; i++)
            {
                Lanes[i].SetActive(false);
            }

            currentSelection--;

            currentSelection = ((currentSelection % Lanes.Length) + Lanes.Length) % Lanes.Length;

            Lanes[currentSelection].SetActive(true);
        }


        //Changing menu units, spawning

        if (Input.GetKeyDown(KeyCode.A) ^ (Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            unitSelection--;

            unitSelection = ((unitSelection % barracks.Length) + barracks.Length) % barracks.Length;
            for(int i = 0; i < borderIcons.Length; i++)
            {
                borderIcons[i].gameObject.GetComponentInParent<SpriteRenderer>().sprite = borderSprite;
                borderIcons[i].gameObject.GetComponentInParent<SpriteRenderer>().sortingOrder = 3;

            }

            borderIcons[unitSelection].gameObject.GetComponentInParent<SpriteRenderer>().sprite = unitHighlight;
            borderIcons[unitSelection].gameObject.GetComponentInParent<SpriteRenderer>().sortingOrder++;


        }//end a

        if (Input.GetKeyDown(KeyCode.D) ^ (Input.GetKeyDown(KeyCode.RightArrow)))
        {
            unitSelection++;
            unitSelection %= barracks.Length;

            for (int i = 0; i < borderIcons.Length; i++)
            {
                borderIcons[i].gameObject.GetComponentInParent<SpriteRenderer>().sprite = borderSprite;
            }

            borderIcons[unitSelection].gameObject.GetComponentInParent<SpriteRenderer>().sprite = unitHighlight;
            borderIcons[unitSelection].gameObject.GetComponentInParent<SpriteRenderer>().sortingOrder++;

        }//end d

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (spawnTimers[unitSelection].GetComponent<charSpawns>().canSpawn)
            {
                spawnTimers[unitSelection].GetComponent<charSpawns>().canSpawn = false;

                Lanes[currentSelection].GetComponentInParent<spawns>().spawn(barracks[unitSelection], 0);
                spawnTimers[unitSelection].GetComponent<Animator>().SetBool("isSpawned", true);
                lockOut();
            }


        }

        
    }
    void updateIcons()
    {
        for (int i = 0; i < barracks.Length; i++)
        {
            borderIcons[i].GetComponent<Image>().sprite = barracks[i].gameObject.GetComponent<SpriteRenderer>().sprite;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);

    }

    float buttonPress;

    void lockOut()
    {
        if (buttonPress == -1)
        {
            buttonPress = Time.time;
        }

        if (Time.time > buttonPress + 4)
        {
            spawnTimers[unitSelection].GetComponent<Animator>().SetBool("isSpawned", false);
            spawnTimers[unitSelection].GetComponent<charSpawns>().updateCanSpawn();
            buttonPress = -1;
        }
    }
}
