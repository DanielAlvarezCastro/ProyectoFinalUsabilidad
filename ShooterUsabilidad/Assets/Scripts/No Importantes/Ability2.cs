using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability2 : MonoBehaviour
{
    public float force = 1000;
    GameObject[] rocks;
    public GameObject rock;
    Animator anim;
    GameObject abilities;
    GameObject weapon;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        abilities = transform.Find("Abilities").gameObject;
        abilities.SetActive(false);
        weapon = transform.Find("Weapon").gameObject;
        weapon.SetActive(true);


        GameObject rockObj = transform.Find("Abilities").transform.Find("Rocks").gameObject;
        rocks = new GameObject[rockObj.transform.childCount];
        for(int i = 0; i< rocks.Length; i++)
        {
            rocks[i] = rockObj.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            weapon.SetActive(false);
            abilities.SetActive(true);
            anim.Play("Ability2");
        }
        if (Input.GetMouseButtonDown(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Ability2_Static"))
        {
            anim.Play("Ability2_2");
        }
    }

    void stopHab2()
    {
        abilities.SetActive(false);
        weapon.SetActive(true);
    }
    public void launch()
    {
        for(int i = 0; i < rocks.Length; i++)
        {
            GameObject auxRock = Instantiate(rock);
            auxRock.transform.position = rocks[i].transform.position;
            auxRock.transform.localScale = rocks[i].transform.lossyScale;
            auxRock.transform.rotation = rocks[i].transform.rotation;
            auxRock.GetComponent<Rigidbody>().AddForce(transform.forward*force);
        }
    }
}
