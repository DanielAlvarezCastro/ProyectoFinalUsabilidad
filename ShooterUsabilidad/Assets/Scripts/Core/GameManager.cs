using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    //Arma inicial
    public GameObject initWeapon;

    //Todas las armas
    public GameObject[] totalWeapons;

    void Awake()
    {
        if (GameManager.instance != null) Destroy(gameObject);
        else instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject getInitWeapon()
    {
        return initWeapon;
    }
    public GameObject[] getTotalWeapons()
    {
        return totalWeapons;
    }

}
