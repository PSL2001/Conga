using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargaController : MonoBehaviour
{
    bool cargando;
    GameObject conga;

    private void Start()
    {
        this.GetComponent<Renderer>().material.color = Color.red;
        cargando = false;
        conga = null;
        StartCoroutine(Loading());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Conga"))
        {
            this.GetComponent<Renderer>().material.color = Color.green;
            cargando = true;
            conga = other.transform.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Conga"))
        {
            this.GetComponent<Renderer>().material.color = Color.red;
            cargando = false;
            conga = null;
        }
    }

    private IEnumerator Loading()
    {
        while (true)
        {
            if (cargando && conga != null)
            {
                conga.GetComponent<Cerebro>().CargarBateria();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
