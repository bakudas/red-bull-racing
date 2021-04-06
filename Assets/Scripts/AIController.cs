using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    public Circuit Circuit;
    
    public float steeringSensitivity = 0.01f;
    public float thresholdWP = 2;
    
    private Drive _drive;
    private Vector3 target;
    private int currentWP = 0;
    private GameManager GM;
    
    // Start is called before the first frame update
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        //GM._estaValendo = false;
        
        _drive = this.GetComponent<Drive>();
        target = Circuit.waypoints[currentWP].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localTarget = _drive.RB.gameObject.transform.InverseTransformPoint(target);
        float distanceToTarget = Vector3.Distance(target, _drive.RB.gameObject.transform.position);

        float targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

        float steer = 0;
        float accel = 0;
        float brake = 0;

        
        
        if (GM._EstadoJogo == GameManager.EstadoJogo.CorridaAtiva)
        {
            steer = Mathf.Clamp(targetAngle * steeringSensitivity, -1, 1) * Mathf.Sign(_drive.currentSpeed);
            accel = 1f;
            brake = 0;
            
            if (distanceToTarget < 5)
            {
                brake = .5f;
                accel = .1f;
            }
            
            _drive.CheckDerrapada();
            _drive.CalculateEngineSound();
        }
        
        _drive.GO(accel, steer, brake);

        if (distanceToTarget < thresholdWP) // ajustar caso o carro comece a andar em circulos perto dos waypoints
        {
            currentWP++;
            
            if (currentWP >= Circuit.waypoints.Length)
            {
                currentWP = 0;
            }

            target = Circuit.waypoints[currentWP].transform.position;
        }

    }
}
