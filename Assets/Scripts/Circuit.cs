using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circuit : MonoBehaviour
{

    public GameObject[] waypoints;

    private void OnDrawGizmos()
    {
        DrawGizmos(false);
    }

    private void OnDrawGizmosSelected()
    {
        DrawGizmos(true);
    }

    void DrawGizmos(bool selecionado)
    {
        if (!selecionado) return;

        if (waypoints.Length > 1)
        {
            Vector3 anterior = waypoints[0].transform.position;
            for (int i = 0; i < waypoints.Length; i++)
            {
                Vector3 prox = waypoints[i].transform.position;
                Gizmos.DrawLine(anterior, prox);
                anterior = prox;
            }
            Gizmos.DrawLine(anterior, waypoints[0].transform.position);
        }
    }
}
