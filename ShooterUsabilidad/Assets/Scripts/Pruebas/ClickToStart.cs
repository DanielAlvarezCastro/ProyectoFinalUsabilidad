using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickToStart : MonoBehaviour
{

    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public Text timeText;

    private Animator anim;

    //Cuando es true debe empezar la prueba asi que desde el script de la prueba se debe comprobar esta variable
    private bool testStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = timeText.GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        //Cuando se pulsa por primera vez empieza la cuenta atrás
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            timerIsRunning = true;

            //Desactivar el animator que si no el cronometro se mueve y lia. Si al final no queremos animacion se quita esto.
            anim.enabled = false;
        }

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else if ( !testStarted )
            {
                timeRemaining = 0;
                timerIsRunning = false;
                testStarted = true;
                timeText.enabled = false;
                Tracker.getInstance().TrackEvent(new TrackerEvent(EventType.SESSION_START));
            }
        }

    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //Devuelve true si el test ha comenzado
    public bool IsTestStarted()
    {
        return testStarted;
    }

    //Coloca el texto de prueba terminada
    public void endTestText()
    {
        timeText.enabled = true;
        timeText.text = "Test finished";
    }
}
