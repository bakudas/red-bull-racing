using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCar : MonoBehaviour
{

    private Rigidbody RB;
    private float LastTimeChecked;
    
    // Start is called before the first frame update
    void Start()
    {
        RB = this.GetComponent<Rigidbody>();
    }

    void TurnRightCar()
    {
       this.transform.position += Vector3.up;
       this.transform.rotation = Quaternion.LookRotation(this.transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.up.y > 0.5f || RB.velocity.magnitude > 1)
        {
            LastTimeChecked = Time.time;
        }

        if (Time.time > LastTimeChecked + 3)
        {
            TurnRightCar();
        }
    }
}
