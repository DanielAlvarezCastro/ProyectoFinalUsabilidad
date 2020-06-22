using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class TrackingTest : MonoBehaviour
{
    public float speed = 1;
    public float maxHoriz = 5;
    public float maxVert = 3;

    public float minRandTime = 2;
    public float maxRandTime = 5;
    float actualRandTime = 0;


    bool started = false;
    Vector3 dirVec;
    Vector3 initPos;
    MeshRenderer mesh;
    Material mat;
    public GameObject textStart;
    TextMeshProUGUI text;
    Animator anim;
    int lastInt = 4;
    // Start is called before the first frame update
    void Start()
    {
        anim = textStart.GetComponent<Animator>();
        textStart.SetActive(false);
        text = textStart.GetComponent<TextMeshProUGUI>();

        mesh = GetComponent<MeshRenderer>();
        mat = mesh.material;
    }
    public void startTest()
    {
        initPos = transform.position;
        changeDir();
    }
    private void Update()
    {
        move();
        limits();
    }

    public void startTimeTest(float maxTime, float actualTime)
    {
        if (actualTime < maxTime && actualTime > 0)
        {
            textStart.SetActive(true);
            int actualInt = (Mathf.RoundToInt(actualTime + 0.5f));
            text.text = actualInt.ToString();
            if(actualInt < lastInt)
            {
                lastInt = actualInt;
                anim.Play("Contador", 0, 0);
            }
        }
        else
        {
            lastInt = 4;
            textStart.SetActive(false);
        }
    }
    public void enterRay()
    {
        mat.color = Color.green;
    }
    public void exitRay()
    {
        mat.color = Color.red;
    }
    void invokeChange()
    {
        actualRandTime = Random.Range(minRandTime, maxRandTime);
        Invoke("changeDir", actualRandTime);
    }
    void changeDir()
    {
        dirVec = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 0f), 0).normalized;
        invokeChange();
    }

    void move()
    {
        transform.Translate(dirVec*Time.deltaTime*speed);
    }
    void limits()
    {
        if (transform.position.x > initPos.x+maxHoriz)
        {
            dirVec.x = -Mathf.Abs(dirVec.x);
            CancelInvoke();
            invokeChange();
        }
        if (transform.position.x < initPos.x-maxHoriz)
        {
            dirVec.x = Mathf.Abs(dirVec.x);
            CancelInvoke();
            invokeChange();
        }
        if (transform.position.y > initPos.y+maxVert)
        {
            dirVec.y = -Mathf.Abs(dirVec.y);
            CancelInvoke();
            invokeChange();
        }
        if (transform.position.y < initPos.y-maxVert)
        {
            dirVec.y = Mathf.Abs(dirVec.y);
            CancelInvoke();
            invokeChange();
        }
    }
}

