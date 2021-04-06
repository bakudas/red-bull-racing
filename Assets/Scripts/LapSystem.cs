using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapSystem : MonoBehaviour
{

    public GameManager GM;

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
    }
    
}
