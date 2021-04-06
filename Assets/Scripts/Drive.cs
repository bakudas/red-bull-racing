using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Drive : MonoBehaviour
{
    public WheelCollider[] WCs;
    public GameObject[] Wheels;
    public float torque = 200;
    public float maxSteerAngle = 30;
    public float maxBrakeTorque = 7500;

    public AudioSource audioDerrapada;
    public AudioSource audioAcc;

    public ParticleSystem smokePrefab;
    public Light[] breakLight;
    ParticleSystem[] smoke = new ParticleSystem[4];

    public Rigidbody RB;
    public float GearLenght = 3f;
    public float currentSpeed
    {
        get { return RB.velocity.magnitude * GearLenght; }
    }

    public float lowPitch = 1f;
    public float highPitch = 6f;
    public int numGears = 5;
    public float maxSpeed = 400;
    private float rpm;
    private int currentGear = 1;
    private float currentGearPerc;
    
    
    public GameManager GM;
    
    // Start is called before the first frame update
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        
        breakLight[0].enabled = false;
        breakLight[1].enabled = false;

        //WC = this.GetComponent<WheelCollider>();
        for (int i = 0; i < 4; i++)
        {
            smoke[i] = Instantiate(smokePrefab);
            smoke[i].Stop();
        }

    }


    public void GO(float accel, float steering, float brake)
    {
        accel = Mathf.Clamp(accel, -1, 1);
        steering = Mathf.Clamp(steering, -1, 1) * maxSteerAngle;
        brake = Mathf.Clamp(brake, 0, 1) * maxBrakeTorque;

        float thrustTorque = 0;
        if (currentSpeed < maxSpeed)
        {
            thrustTorque = accel * torque;
        }

        if (brake != 0)
        {
            breakLight[0].enabled = true;
            breakLight[1].enabled = true;
        }
        else
        {
            breakLight[0].enabled = false;
            breakLight[1].enabled = false;
        }

        
        for (int i = 0; i < 4; i++)
        {
            

            if (i <= 1)
            {
                WCs[i].steerAngle = steering;
                WCs[i].motorTorque = thrustTorque;
                WCs[i].brakeTorque = brake;
            }
            else
            {
                WCs[i].motorTorque = thrustTorque/4;
                WCs[i].brakeTorque = brake;
            }
            
            
            Quaternion quat;
            Vector3 position;
            WCs[i].GetWorldPose(out position, out quat);
            Wheels[i].transform.position = position;
            Wheels[i].transform.localRotation = quat;

        }
    }

    public void CheckDerrapada()
    {
        int numDerrapada = 0;
        for (int i = 0; i < 4; i++)
        {
            WheelHit wheelHit;
            WCs[i].GetGroundHit(out wheelHit);

            if (Mathf.Abs(wheelHit.forwardSlip) >= 1f || Mathf.Abs(wheelHit.sidewaysSlip) >= 1f)
            {
                numDerrapada++;
                if (!audioDerrapada.isPlaying)
                {
                    //audioDerrapada.Play();
                }

                smoke[i].transform.position = WCs[i].transform.position - WCs[i].transform.up * WCs[i].radius;
                smoke[i].Emit(1);
            }
        }

        if (numDerrapada == 0 && audioDerrapada.isPlaying)
        {
            audioDerrapada.Stop();
        }
        
    }


    public void CalculateEngineSound()
    {
        float gearPercentage = (1 / (float) numGears);
        float targetGearFactor = Mathf.InverseLerp(gearPercentage * currentGear, gearPercentage * (currentGear + 1),
            Mathf.Abs(currentGear / maxSpeed));

        currentGearPerc = Mathf.Lerp(currentGearPerc, targetGearFactor, Time.deltaTime * 5f);
        
        var gearNumFactor = currentGear / (float)numGears;
        rpm = Mathf.Lerp(gearNumFactor, 1, currentGearPerc);

        float speedPercentage = Mathf.Abs(currentSpeed / maxSpeed);
        float upperGearMax = (1 / (float)numGears) * (currentGear + 1);
        float downGearMax = (1 / (float)numGears) * currentGear;

        if (currentGear > 0 && speedPercentage < downGearMax)
            currentGear--;
        if (speedPercentage > upperGearMax && (currentGear < (numGears - 1)))
            currentGear++;

        float pitch = Mathf.Lerp(lowPitch, highPitch, rpm);
        audioAcc.pitch = Mathf.Min(highPitch, pitch) * .25f;
    }


}
