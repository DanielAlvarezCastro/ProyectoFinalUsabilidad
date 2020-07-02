using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum stat {precision, aimTime, reactionTime, tracking};
public class AnalysisManager : MonoBehaviour
{
    List<float> precisionValues;
    List<float> aimTimeValues;
    List<float> reactionTimeValues;
    List<float> trackingValues;

    float mediaPrecision = 0;
    float mediaAimTime = 0;
    float mediaReactionTime = 0;
    float mediaTracking = 0;

    float notaFinal = 0;

    //TRACKING
    [Header("Tracking")]
    [Range(0, 100)]
    public float trackingTrackPond = 0;

    //DISPARO A OBJETOS MÓVILES
    [Header("Disparo a objetos móviles")]
    [Range(0, 100)]
    public float precisionMovPond = 0;
    [Range(0, 100)]
    public float aimTimeMovPond = 0;
    [Range(0, 100)]
    public float reactionTimeMovPond = 0;
    [Range(0, 100)]
    public float trackingMovPond = 0;

    //REFLEJOS
    [Header("Reflejos")]
    [Range(0, 100)]
    public float reactionTimeReflexPond = 0;

    //PRECISION
    [Header("Precision")]
    [Range(0, 100)]
    public float precisionPrecPond = 0;
    [Range(0, 100)]
    public float aimTimePrecPond = 0;
    [Range(0, 100)]
    public float reactionTimePrecPond = 0;

    //VELOCIDAD
    [Header("Velocidad")]
    [Range(0, 100)]
    public float precisionVelPond = 0;
    [Range(0, 100)]
    public float aimTimeVelPond = 0;
    [Range(0, 100)]
    public float reactionTimeVelPond = 0;


    //NOTA FINAL
    [Header("Nota final")]
    [Range(0, 100)]
    public float precisionTotalPond = 0;
    [Range(0, 100)]
    public float aimTimeTotalPond = 0;
    [Range(0, 100)]
    public float reactionTimeTotalPond = 0;
    [Range(0, 100)]
    public float trackingTotalPond = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindObjectOfType<AnalysisManager>() && GameObject.FindObjectOfType<AnalysisManager>().gameObject != gameObject) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);

        precisionValues = new List<float>();
        aimTimeValues = new List<float>();
        reactionTimeValues = new List<float>();
        trackingValues = new List<float>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addStadistic(stat category, float value)
    {
        switch(category)
        {
            case stat.precision:
                precisionValues.Add(value);
                break;
            case stat.aimTime:
                aimTimeValues.Add(value);
                break;
            case stat.reactionTime:
                reactionTimeValues.Add(value);
                break;
            case stat.tracking:
                trackingValues.Add(value);
                break;
        }
    }

    public void ponderateStatsAndFinalScore()
    {
        //Ponderar cada estadística
        ponderateStat(stat.precision, precisionValues.ToArray());
        ponderateStat(stat.aimTime, aimTimeValues.ToArray());
        ponderateStat(stat.reactionTime, reactionTimeValues.ToArray());
        ponderateStat(stat.tracking, trackingValues.ToArray());

        //Nota Final
        generateFinalScore();
    }

    void ponderateStat(stat category, float[] stats)
    {
        float media = 0;
        if (category == stat.precision)
        {
            stats[0] *= precisionMovPond;
            stats[1] *= precisionPrecPond;
            stats[2] *= precisionVelPond;

            for (int i = 0; i < stats.Length; i++)
            {
                media += stats[i];
            }
            media /= stats.Length;
            mediaPrecision = media;
        }
        else if(category == stat.aimTime)
        {
            stats[0] *= aimTimeMovPond;
            stats[1] *= aimTimePrecPond;
            stats[2] *= aimTimeVelPond;

            for (int i = 0; i < stats.Length; i++)
            {
                media += stats[i];
            }
            media /= stats.Length;
            mediaAimTime = media;
        }
        else if (category == stat.tracking)
        {
            stats[0] *= trackingTrackPond;
            stats[1] *= trackingMovPond;

            for (int i = 0; i < stats.Length; i++)
            {
                media += stats[i];
            }
            media /= stats.Length;
            mediaTracking = media;
        }
        else if (category == stat.reactionTime)
        {
            stats[0] *= reactionTimeMovPond;
            stats[1] *= reactionTimeReflexPond;
            stats[2] *= reactionTimePrecPond;
            stats[3] *= reactionTimeVelPond;

            for (int i = 0; i < stats.Length; i++)
            {
                media += stats[i];
            }
            media /= stats.Length;
            mediaReactionTime = media;
        }

    }

    public void generateFinalScore()
    {
        notaFinal = 0;
        notaFinal += mediaPrecision*precisionTotalPond;
        notaFinal += mediaAimTime * aimTimeTotalPond;
        notaFinal += mediaReactionTime * reactionTimeTotalPond;
        notaFinal += mediaTracking * trackingTotalPond;


        //Ya la tenemos
        print(notaFinal);
    }
}
