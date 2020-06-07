using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator anim;
    GameObject[] weapons;
    bool acting = false;

    //Variables
    public GameObject weaponPos;
    public float randTime = 0.25f;

    float auxRandTime = 0;
    int initRand = 0;
    bool picked = false;
    bool stopped = false;
    bool pickable = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        generate();

    }
    //Genera todas las armas de base, luego sólo activa y desactiva para animarlas
    void generate()
    {
        GameObject[] auxObjs = GameManager.instance.getTotalWeapons();
        weapons = new GameObject[auxObjs.Length];
        for(int i = 0; i < auxObjs.Length; i++)
        {
            weapons[i] = Instantiate(auxObjs[i]);
            weapons[i].GetComponentInChildren<Camera>().gameObject.SetActive(false);
            weapons[i].SetActive(false);
            weapons[i].transform.parent = weaponPos.transform;
            weapons[i].transform.position = weaponPos.transform.position;
            weapons[i].transform.rotation = weaponPos.transform.rotation;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (acting && !stopped && auxRandTime < randTime)
        {
            auxRandTime += Time.deltaTime;
        }
        else if(acting && !stopped)
        {
            weapons[initRand].SetActive(false);
            initRand = (initRand+1)%weapons.Length;
            weapons[initRand].SetActive(true);
            auxRandTime = 0;
        }

    }

    //Prefiero no tocar de aquí xd
    public void Interact(Player player)
    {
        if (!acting && pickable)
        {
            pickable = false;
            picked = false;
            acting = true;
            stopped = false;
            anim.Play("Open", 0, 0);
            initRand = Random.Range(0, weapons.Length);
        }
        else if(!picked && pickable)
        {
            anim.Play("Open", 0, 0.5f);
            picked = true;
            stopped = true;
            pickable = false;
        }
        else if (stopped && acting)
        {
            anim.Play("Open", 0, 0.94f);
            weapons[initRand].SetActive(false);
            acting = false;
            player.getSpecificWeapon(initRand);
        }
    }
    public void setPickable()
    {
        pickable = true;
    }
    public void setPickableFalse()
    {
        pickable = false;
        acting = false;
    }
}
