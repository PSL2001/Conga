using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescargaBasuraController : MonoBehaviour
{
    bool descargar;
    public GameObject basura;
    GameObject conga;
    GameObject puntoDescarga;

    private void Start()
    {
        this.GetComponent<Renderer>().material.color = Color.red;
        puntoDescarga = this.transform.GetChild(0).transform.gameObject;
        descargar = false;
        conga = null;
        StartCoroutine(DescargaBasura());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Conga"))
        {
            this.GetComponent<Renderer>().material.color = Color.green;
            descargar = true;
            conga = other.transform.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Conga"))
        {
            this.GetComponent<Renderer>().material.color = Color.red;
            descargar = false;
            conga = null;
        }
    }

    private IEnumerator DescargaBasura()
    {
        while (true)
        {
            if (descargar && conga != null)
            {
                Debug.Log("Descargando Basura");
                conga.GetComponent<Cerebro>().DescargarBasura();
                Instantiate(basura, puntoDescarga.transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
