using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coletavel : MonoBehaviour
{
    

    public enum Colecionavel
    {
        Checkpooint,
        Latinhas,
        Moedas,
        Chegada
    };
    
    private GameManager GM;
    public Colecionavel _Colecionavel;
    public GameObject VfxSpawner;
    public AudioClip SfxColetavel;
    
    
    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        
        
        if (other.CompareTag("Player"))
        {
            
            switch (_Colecionavel)
            {
                case Colecionavel.Checkpooint:
                    // GM.SetCheckpoint(1);
                    break;
                
                case Colecionavel.Latinhas:
                    Destroy(gameObject);
                    GM.AddLatinhas(1);
                    break;
                
                case Colecionavel.Moedas:
                    break;
                
                case Colecionavel.Chegada:
                    GM.SetVoltasCompletadas(1);
                    
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
    }
    
}
