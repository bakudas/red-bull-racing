using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiRollBar : MonoBehaviour
{

    public float antiRool = 5000.0f;
    public WheelCollider wheelLFront;
    public WheelCollider wheelRFront;
    public WheelCollider wheelLBack;
    public WheelCollider wheelRBack;
    public GameObject centerOfMass;

    private Rigidbody RB;
    
    // Start is called before the first frame update
    void Start()
    {
        RB = this.GetComponent<Rigidbody>();
        RB.centerOfMass = centerOfMass.transform.localPosition;
    }

    private void GroundWheels(WheelCollider WL, WheelCollider WR)
    {
        WheelHit hit;
        float travelL = 1f;
        float travelR = 1f;

        bool groundedL = WL.GetGroundHit(out hit);
        if (groundedL)
        {
            travelL = (-WL.transform.InverseTransformPoint(hit.point).y - WL.radius) / WL.suspensionDistance;
        }
        
        bool groundedR = WR.GetGroundHit(out hit);
        if (groundedR)
        {
            travelR = (-WR.transform.InverseTransformPoint(hit.point).y - WR.radius) / WR.suspensionDistance;
        }

        float AntiRollForce = (travelL - travelR) * antiRool;

        if (groundedL)
        {
            RB.AddForceAtPosition(WL.transform.up * -AntiRollForce, WL.transform.position);
        }
        
        if (groundedR)
        {
            RB.AddForceAtPosition(WR.transform.up * AntiRollForce, WR.transform.position);
        }
    }
    
    void FixedUpdate()
    {
        GroundWheels(wheelLFront, wheelRFront);
        GroundWheels(wheelLBack, wheelRBack);
    }
}
