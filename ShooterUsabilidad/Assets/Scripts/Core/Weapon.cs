using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    Aim aimComp;
    Player player;

    //Variables de arma
    public bool automatic = false;
    public bool hitScan = true;

    [Header("Values")]
    public float fireRate = 0.25f;
    public float damagePerBullet = 10;
    public int bulletsPerShot = 1;
    public int magazine = 10;
    public float desviation = 30;
    public float apuntadoDesviation = 10;

    //Desviacion nuevo
    [Header("Recoil")]
    public bool usesRecoil = true;
    public float maxRecoilTime = 2;
    public float recoilTime = 0.3f;
    public float stabilityMultiplicator = 1;
    float actualRecoil = 0;

    //Apuntado
    [Header("Apuntado")]
    public float sensivityApuntado = 0.5f;
    public float aimingMult = 0.25f;

    [HideInInspector]
    public float actualDesviation = 0;
    Animator anim;
    bool apuntando = false;
    //Esto está guarro, hace un find de "Mira" nada mas empezar
    GameObject centerSprite;

    [Space(20)]
    //Variables necesarias
    public GameObject bullet;
    public GameObject shootPos;
    public GameObject shootEffect;
    public GameObject bulletHit;
    public GameObject cameraObj;
    public AudioClip shootSound;

    GameObject auxBul;
    AudioSource src;
    Vector3 forwardVector;

    //auxs
    bool reloading = false;
    int actualBullets = 0;
    float shootInit;
    float shootObj;

    string shootName = "";
    string apuntadoShootName = "";

    Slider recoilSlider;

    // Start is called before the first frame update
    void Start()
    {
        startWeapon();

        //Caca
        recoilSlider = GameObject.FindObjectOfType<Slider>();
        if(recoilSlider !=null)
            recoilSlider.maxValue = maxRecoilTime;
    }

    //Coge y establece variables necesarias
    public void startWeapon()
    {
        actualBullets = magazine;
        player = GetComponentInParent<Player>();
        aimComp = transform.parent.GetComponent<Aim>();
        src = transform.parent.GetComponentInChildren<AudioSource>();
        anim = transform.GetComponentInChildren<Animator>();
        centerSprite = GameObject.Find("Mira").gameObject;
        actualDesviation = desviation;
    }

    // Update is called once per frame
    void Update()
    {
        recoil();
        reload();
        if (reloading) return;

        //Si no está recargando, puedes apuntar y disparar
        shootInput();
        aim();
    }
    void recoil()
    {
        if (actualRecoil > 0) actualRecoil -= Time.deltaTime*stabilityMultiplicator;
        else actualRecoil = 0;

        if(recoilSlider != null)
            recoilSlider.value = actualRecoil;
    }
    void reload()
    {
        //Recargas cuando no tienes balas o pulsas R, siempre que el arma no esté reproduciendo otras animaciones
        if ((actualBullets <= 0 || Input.GetKeyDown(KeyCode.R)) && (anim.GetCurrentAnimatorStateInfo(0).IsName("Static")))
        {
            anim.speed = 1;
            anim.Play("Reload");
            actualBullets = magazine;
            reloading = true;
        }
        //Si estás apuntando y te quedas sin balas o pulsas R, te desapunta automáticamente para poder realizar el código de arriba
        else if ((actualBullets <= 0 || Input.GetKeyDown(KeyCode.R)) && (anim.GetCurrentAnimatorStateInfo(0).IsName("Apuntando")))
        {
            anim.speed = 1;
            anim.Play("Desapuntado");
            apuntando = false;
            actualDesviation = desviation;
            centerSprite.SetActive(true);
            aimComp.aiming(1 / sensivityApuntado);
            player.aiming(1 / aimingMult);
            reloading = true;
        }
        //Si ha acabado la animación de recargar
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Static"))
        {
            anim.speed = 1;
            reloading = false;
        }
    }

    //Controla las animaciones de apuntado y los cambios de movimiento que producen
    void aim()
    {
        //Apuntado, siempre que no estés haciendo otra cosa (o estés desapuntando, lo que reproduce la animación desde ese punto)
        if (!apuntando && Input.GetMouseButton(1) && (anim.GetCurrentAnimatorStateInfo(0).IsName("Static") || anim.GetCurrentAnimatorStateInfo(0).IsName("Desapuntado")))
        {
            StopAllCoroutines();
            apuntando = true;

            //Desactiva la mira
            centerSprite.SetActive(false);

            //Reproduce animación en el punto exacto
            float auxTime = 1;
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Desapuntado"))
                auxTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            anim.Play("Apuntado", 0, 1-auxTime);

            //Reduce la velocidad de mira y de movimiento
            actualDesviation = apuntadoDesviation;
            aimComp.aiming(sensivityApuntado);
            player.aiming(aimingMult);

            anim.speed = 1;

        }
        //Desapuntado, siempre que estés apuntando (o sacando el arma, lo que reproduce la animación desde ese punto)
        else if(apuntando && !Input.GetMouseButton(1) && (anim.GetCurrentAnimatorStateInfo(0).IsName("Apuntando") || anim.GetCurrentAnimatorStateInfo(0).IsName("Apuntado")))
        {
            StopAllCoroutines();
            apuntando = false;

            //Activa la mira
            centerSprite.SetActive(true);

            //Reproduce animación en el punto exacto
            float auxTime = 1;
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Apuntado"))
                auxTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            anim.Play("Desapuntado", 0, 1-auxTime);

            //Devuelve la velocidad de mira y de movimiento
            actualDesviation = desviation;
            aimComp.aiming(1/sensivityApuntado);
            player.aiming(1/aimingMult);

            anim.speed = 1;
        }
        else if(apuntando && anim.GetCurrentAnimatorStateInfo(0).IsName("Apuntando")) anim.speed = 1;
    }
    //Controla el input para disparar
    void shootInput()
    {
        if(((Input.GetMouseButtonDown(0) && !automatic) || (Input.GetMouseButton(0) && automatic)) && (anim.GetCurrentAnimatorStateInfo(0).IsName("Static") || anim.GetCurrentAnimatorStateInfo(0).IsName("Apuntando")))
        {
            actualBullets--;

            //player.commandShoot(shotgun, numBullets, damagePerBullet, actualDesviation, bullet, bulletHit, shootPos, cameraObj, auxBul);
            realShoot();

            //Disparo normal de cadera
            if (!apuntando)
            {
                anim.Play("Shoot");
                //anim.speed = (1 / fireRate) * shootAnimLen;
            }

            //Disparo apuntando
            else
            {
                anim.Play("ApuntadoShoot");
                //anim.speed = (1 / fireRate) * apuntadoShootAnimLen;
            }

            StartCoroutine(ShowCurrentClipLength());

            //Sonido y efecto de disparo
            shootEffects();

        }
    }
    IEnumerator ShowCurrentClipLength()
    {
        yield return new WaitForEndOfFrame();
        float animLen = anim.GetCurrentAnimatorStateInfo(0).length;
        anim.speed = (1 / fireRate)*animLen;
    }
    void shootEffects()
    {
        //Efecto visual
        GameObject auxEffect = Instantiate(shootEffect);
        auxEffect.transform.parent = shootPos.transform;
        auxEffect.transform.position = shootPos.transform.position;
        auxEffect.transform.rotation = shootPos.transform.rotation;

        //Sonido
        src.PlayOneShot(shootSound);
    }

    void realShoot()
    {
        float deviation = 0;
        float angle = 0;

        Ray ray;
        for (int x = 0; x < bulletsPerShot; x++)
        {
            //Genera una dirección aleatoria entre el radio de desviación actual
            //Esto lo vi en internet no preguntéis
            forwardVector = Vector3.forward;
            if(usesRecoil)
                deviation = Random.Range(0f, actualDesviation*(actualRecoil/maxRecoilTime));
            else
                deviation = Random.Range(0f, actualDesviation);
            angle = Random.Range(0f, 360f);
            forwardVector = Quaternion.AngleAxis(deviation, Vector3.up) * forwardVector;
            forwardVector = Quaternion.AngleAxis(angle, Vector3.forward) * forwardVector;
            forwardVector = cameraObj.transform.rotation * forwardVector;

            //Solo generamos el rayo si hay hitscan (evita el código para cohetes p.ej)
            if (hitScan)
            {
                //Raycast (HITSCAN)
                ray = new Ray(cameraObj.transform.position, forwardVector);
                RaycastHit hit;

                //Si chocamos contra algo NO TRIGGER
                if (Physics.Raycast(ray, out hit, 100))
                {
                    //Mensaje de daño CUTRE
                    if (hit.collider.GetComponent<Enemy>() != null)
                        hit.collider.GetComponent<Enemy>().getDamage(damagePerBullet);
                    if (hit.collider.GetComponent<Diana>() != null)
                        hit.collider.GetComponent<Diana>().hitPoint(hit.point);

                    bulletHitInst(hit);
                }
            }

            //Por último, instanciamos la bala física (sólo visual en caso de hitscan)
            bulletInst();

            //Recoil
            actualRecoil += recoilTime;
            if (actualRecoil > maxRecoilTime) actualRecoil = maxRecoilTime;
        }
    }

    //Instancia y coloca la bala física
    void bulletInst()
    {
        GameObject auxBul1 = Instantiate(bullet);
        auxBul1.transform.forward = forwardVector;
        auxBul1.transform.position = shootPos.transform.position;
        if (!hitScan) auxBul1.GetComponent<Bullet>().setDamage(damagePerBullet);
    }

    //Instancia y coloca el efecto de choque de la bala
    void bulletHitInst(RaycastHit hit)
    {
        auxBul = Instantiate(bulletHit);

        //Estos cálculos son para meter el choque un poco dentro de la pared, valdría con "= hit.point;"
        auxBul.transform.position = hit.point - hit.normal * (auxBul.transform.lossyScale.x / 2.5f);

        auxBul.transform.eulerAngles = hit.normal;
        //Escala del objeto, por si el choque no es escala (1,1)
        Vector3 auxScale = auxBul.transform.lossyScale;
        auxBul.transform.localScale = auxScale;
        auxBul.transform.parent = hit.collider.gameObject.transform.parent;
    }

}
