using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResultadosManager : MonoBehaviour
{
    TextMeshProUGUI nombreText;
    TextMeshProUGUI precisionText;
    TextMeshProUGUI tiempoApuntadoText;
    TextMeshProUGUI velReaccionText;
    TextMeshProUGUI trackingText;
    TextMeshProUGUI notaText;
    // Start is called before the first frame update
    void Start()
    {
        if (GameSessionManager.Instance != null)
        {
            nombreText.text = GameSessionManager.Instance.GetPlayerName();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Exit()
    {
        GameSessionManager.Instance.EndGame();
    }
}
