using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    Vector3 moveVec;

    //Variables de player
    public float speed = 1;
    public float jumpForce = 1000;
    public GameObject arm;

    //GAMEMANAGER
    GameObject[] weapons;
    GameObject weapon;

    //Auxs
    GameObject interactable = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawn();

    }

    // Update is called once per frame
    void Update()
    {
        move();
        interact();
    }

    //Se coloca aleatoriamente entre los puntos de spawn
    //Luego coge el arma inicial del GameManager
    void spawn()
    {
        GameObject auxPoint = GameObject.Find("SpawnPoints").gameObject;
        getInitWeapon();
        int randChild = Random.Range(0, auxPoint.transform.childCount);
        transform.position = auxPoint.transform.GetChild(randChild).transform.position;
        arm.transform.rotation = auxPoint.transform.GetChild(randChild).transform.rotation;
    }

    //Movimiento simple y normalizado en 8 direcciones
    void move()
    {
        moveVec = Vector3.zero;

        if(Input.GetKey(KeyCode.W))
        {
            moveVec += new Vector3(arm.transform.forward.x, 0, arm.transform.forward.z).normalized;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVec -= new Vector3(arm.transform.forward.x, 0, arm.transform.forward.z).normalized;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveVec += new Vector3(arm.transform.right.x, 0, arm.transform.right.z).normalized;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveVec -= new Vector3(arm.transform.right.x, 0, arm.transform.right.z).normalized;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up*jumpForce);
        }

        moveVec = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * moveVec;
        //Mueve al jugador
        moveVec = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * moveVec;
        transform.Translate(moveVec * speed * Time.deltaTime);
    }

    //Para cambiar la velocidad de movimiento mientras apuntas
    public void aiming(float mult)
    {
        speed = speed * mult;
    }

    public void getInitWeapon()
    {
        GameObject aux = Instantiate(GameManager.instance.getInitWeapon());
        getWeapon(aux);
    }

    public void getSpecificWeapon(int id)
    {
        weapons = GameManager.instance.getTotalWeapons();
        GameObject aux = Instantiate(weapons[id]);
        getWeapon(aux);
    }

    public void getRandomWeapon()
    {
        weapons = GameManager.instance.getTotalWeapons();
        GameObject aux = Instantiate(weapons[Random.Range(0, weapons.Length)]);
        getWeapon(aux);
    }

    //Establece el arma elegida y lo coloca en el player
    void getWeapon(GameObject weap)
    {
        Transform auxT = gameObject.transform.Find("Body").Find("Arm").transform;
        if (auxT.Find("Weapon") != null) Destroy(auxT.Find("Weapon").gameObject);
        weapon = weap;
        weapon.transform.parent = auxT.transform;
        weapon.transform.name = "Weapon";
        weapon.transform.rotation = auxT.transform.rotation;
        weapon.transform.position = auxT.transform.position;
    }

    //No necesario, para el baúl de armas
    void interact()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactable != null)
        {
            try
            {
                interactable.GetComponent<Chest>().Interact(this);
            }
            catch { }
        }
    }

    //Para interacción
    private void OnTriggerEnter(Collider other)
    {
        interactable = other.gameObject;
    }

    //Para interacción
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == interactable)
        {
            interactable = null;
        }
    }
}
