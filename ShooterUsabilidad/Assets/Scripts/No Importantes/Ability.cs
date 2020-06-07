using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    Animator anim;
    GameObject abilities;
    GameObject weapon;
    public GameObject hab;
    public AudioSource src;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        abilities = transform.Find("Abilities").gameObject;
        abilities.SetActive(false);
        weapon = transform.Find("Weapon").gameObject;
        weapon.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        ability();
    }

    void stopHab1()
    {
        abilities.SetActive(false);
        weapon.SetActive(true);
    }
    void ability()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            weapon.SetActive(false);
            abilities.SetActive(true);
            anim.Play("Ability1");
            hab.transform.position = transform.parent.transform.parent.position;
            hab.transform.eulerAngles = new Vector3(0,transform.eulerAngles.y, 0);
            Instantiate(hab);
            src.PlayOneShot(clip);
        }
    }
}
