using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Renderer CarMesh;
    public GameObject Rodas;
    public GameObject Aviao;
    public GameObject Camera;
    
    private bool _isPlaneActive;
    private GameManager GM;
    private Drive _drive;
    private Quaternion camdefault;


    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        GM._estaValendo = false;

        _drive = this.GetComponent<Drive>();
        camdefault = Camera.transform.localRotation;
    }


    private void CheckRetrovisor(bool enable)
    {
        if (enable)
        {
            Camera.transform.localRotation = new Quaternion(0f, 180f, 0.001f, 0f);
        }
        else
        {
                
            Camera.transform.localRotation = camdefault;
        }
    }



    private void _TransformToPlane()
    {
        CarMesh.enabled = false;
        Rodas.SetActive(false);
        Aviao.SetActive(true);
        _isPlaneActive = true;
        //PlayEffectTransfomation(); TODO
        //PlaySoundTransfomation(); TODO
    }
    
    
    private void _TransformToCar()
    {
        CarMesh.enabled = true;
        Rodas.SetActive(true);
        Aviao.SetActive(false);
        _isPlaneActive = false;
        //PlayEffectTransfomation() TODO
        //PlaySoundTransfomation(); TODO
        
    }

    
    //TODO MÉTODO EFEITO FX DE TRANSFORMAÇÃO 
    // private void PlayEffectTransfomation();
    // {
    //     
    // }
    
    
    //TODO MÉTODO TOCAR EFEITO SOM DE TRANSFORMAÇÃO 
    // private void PlaySoundTransfomation();
    // {
    //     
    // }
    
    // Update is called once per frame
    void Update()
    {
        float accel = 0;
        float steer = 0;
        float brake = 0;
        
        if (GM._EstadoJogo == GameManager.EstadoJogo.CorridaAtiva)
        {
            accel = Input.GetAxis("Accelerate");
            steer = Input.GetAxis("Horizontal");
            brake = Input.GetAxis("Brake");
            
            if (Input.GetButton("Jump") && GM.GetLatinhas() >= 3)
            {
                if (!_isPlaneActive)
                {
                    _TransformToPlane();
                    GM.SubtractLatinhas(3);
                }
                else
                {
                    _TransformToCar();
                }
            
            }

            if (Input.GetKey(KeyCode.Q)) 
                CheckRetrovisor(true);
            else
                CheckRetrovisor(false);
            
            
            _drive.GO(accel, steer, brake);
            _drive.CheckDerrapada();
            _drive.CalculateEngineSound();
        }


    }
}
