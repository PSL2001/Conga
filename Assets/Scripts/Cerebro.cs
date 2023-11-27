using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class Cerebro : MonoBehaviour
{
    private int bateria;
    private Motores motores;
    private Sensores sensores;
    private int basura;

    public GameObject estacionCarga;
    public GameObject estacionBasura;
    private Vector3 destino;

    private void Awake()
    {
        sensores = this.GetComponent<Sensores>();
        motores = this.GetComponent<Motores>();
    }

    // Start is called before the first frame update
    void Start()
    {
        bateria = 100;
        basura = 0;
        StartCoroutine(DescargarBateria());
    }

    private IEnumerator DescargarBateria()
    {
        while (true)
        {
            if (bateria >= 0) 
            {
                bateria--;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    [Task]
    private void bateriaBaja()
    {
        if (bateria < 20)
        {
            motores.parar();
            destino = estacionCarga.transform.position;
            Task.current.Succeed();
        } else
        {
            Task.current.Fail();
        }
    }

    public void CargarBateria() 
    { 
        bateria++; 
    }

    public void DescargarBasura()
    {
        basura--;
    }

    [Task]
    private void irADestino()
    {
        motores.mover(destino);
        Task.current.Succeed();
    }

    [Task]
    private void llegarDestino() 
    {
        if (!motores.seMueve())
        {
            destino = Vector3.zero;
            Task.current.Succeed();
        }
    }

    [Task]
    private void destinoRandom()
    {
        motores.parar();
        destino = new Vector3(UnityEngine.Random.Range(-4.0f, 4.0f), 0, UnityEngine.Random.Range(-4.0f, 4.0f));
        Task.current.Succeed();
    }

    [Task]
    private void bateriaCargada()
    {
        if (bateria == 100)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    private void basuraEncontrada()
    {
        if(sensores.objetivoEncontrado())
        {
            motores.parar();
            destino = sensores.getDestino();
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    private void BasuraDescargada()
    {
        if (basura == 0)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    private void BasuraLlena()
    {
        if (basura > 10)
        {
            motores.parar();
            destino = estacionBasura.transform.position;
            Task.current.Succeed();
        } else
        {
            Task.current.Fail();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Basura"))
        {
            basura++;
            Destroy(other.transform.gameObject);
        }
    }
}
