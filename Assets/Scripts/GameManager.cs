using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum EstadoJogo {CorridaAtiva, JogoParado, Menu, AcabouCorrida}
    
    public TMP_Text uiTextLatinha;
    public TMP_Text uiCheckpoint;
    public TMP_Text uiTime;
    public TMP_Text uiVoltas;
    
    public bool _estaValendo = false;
    
    public EstadoJogo _EstadoJogo;

    public int _gameTime;
    public int numeroDeVoltas;
    private int voltasCompletadas;
    private int _latinhas = 0;
    private int _checkpointsAtivos;
    private int _checkpointsPegos;



    // Start is called before the first frame update
    void Start()
    {
        _checkpointsPegos = 0;
        _checkpointsAtivos = GameObject.FindGameObjectsWithTag("checkpoint").Length;
        uiCheckpoint.text = "Checkpoint: " + _checkpointsPegos.ToString() + "/" + _checkpointsAtivos.ToString();
        _estaValendo = false;
        _EstadoJogo = EstadoJogo.JogoParado;
        SetNumeroDeVoltas(numeroDeVoltas);
        //SetVoltasCompletadas(1);
        _OnLatinhaUpdate(_latinhas);
        uiTime.text = _gameTime.ToString();
    }


    private void Update()
    {
        // UpdateCheckpointText();
        ChecaEstadoCorrida();

        
    }


    private void ChecaEstadoCorrida()
    {
        switch (_EstadoJogo)
        {
            case EstadoJogo.CorridaAtiva:
                SetEstaValendo(true);
                break;
            case EstadoJogo.JogoParado:
                SetEstaValendo(false);
                break;
            case EstadoJogo.Menu:
                SetEstaValendo(false);
                break;
            case EstadoJogo.AcabouCorrida:
                SetEstaValendo(false);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    
    #region Checkpoint

    // seta os checkpoints coletados
    public void SetCheckpoint(int qtd)
    {
        _checkpointsPegos += qtd;
    }
    
    
    // retorna a quantidade de checkpoints coletatos
    public int GetCheckpoint() => _checkpointsPegos;

    
    // Atualizar os texto na UI dos checkpoints
    public void UpdateCheckpointText()
    {
        uiCheckpoint.text = "Checkpoint: " + _checkpointsPegos.ToString() + "/" + _checkpointsAtivos.ToString();
    }


    #endregion
    

    #region Colecionável LATINHA
    
    
    public void SetLatinhas(int qtd)
    {
        _latinhas = qtd;
        _OnLatinhaUpdate(_latinhas);
    }

    
    public void AddLatinhas(int qtd)
    {
        _latinhas = (_latinhas + qtd);
        _OnLatinhaUpdate(_latinhas);
    }

    public void SubtractLatinhas(int qtd)
    {
        _latinhas = (_latinhas - qtd);
        _OnLatinhaUpdate(_latinhas);
    }
    
    public int GetLatinhas() => _latinhas;

    
    private void _OnLatinhaUpdate(int qtd)
    {
        uiTextLatinha.text = GetLatinhas().ToString();
    }
    
    
    #endregion


    #region Está Valendo?

    public bool GetEstaValendo()
    {
        return _estaValendo;
    }


    public void SetEstaValendo(bool estado)
    {
        _estaValendo = estado;
    }

    #endregion

    
    #region Sistema de Voltas

    // retorna o numero de voltas totais
    public int GetNumeroDeVoltas()
    {
        return numeroDeVoltas;
    }

    
    // seta o numero de voltas
    public void SetNumeroDeVoltas(int qtd)
    {
        numeroDeVoltas = qtd;
        UpdateVoltasText();
    }
    
    
    // retorna as voltas completadas
    public int GetVoltasCompletadas()
    {
        
        return voltasCompletadas;
        
    }

    
    // seta o numero de voltas completadas
    public void SetVoltasCompletadas(int qtd)
    {
        voltasCompletadas += qtd;
        UpdateVoltasText();

    }

    
    // atualiza os textos da UI de volta
    public void UpdateVoltasText()
    {
        uiVoltas.text = GetVoltasCompletadas().ToString() + "/" + GetNumeroDeVoltas().ToString();
    }

    #endregion


    #region Game Time

   

    #endregion
}
