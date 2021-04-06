using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    
    private int _countdown = 3;
    public TMP_Text uiCountdown;
    public GameManager GM;
    

        
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Countdown");
    }

    IEnumerator Countdown()
    {
        uiCountdown.text = "Preparem os Motores!";
        yield return new WaitForSeconds(1);
        uiCountdown.text = "3";
        yield return new WaitForSeconds(1);
        uiCountdown.text = "2";
        yield return new WaitForSeconds(1);
        uiCountdown.text = "1";
        yield return new WaitForSeconds(1);
        uiCountdown.text = "VAI!";
        GM._EstadoJogo = GameManager.EstadoJogo.CorridaAtiva; // começa a corrida!
        yield return new WaitForSeconds(1);
        uiCountdown.enabled = false;
        yield return null;
    }
}
