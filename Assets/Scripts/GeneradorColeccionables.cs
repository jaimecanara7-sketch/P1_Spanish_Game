
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GeneradorColeccionables : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private GameObject prefabJamon;
    [SerializeField] private Transform[] puntosSpawn;
    [SerializeField] private int cantidadInicial = 9;
    [SerializeField] private int objetivoJamones = 50;

    [Header("Interfaz")]
    [SerializeField] private TMP_Text textoJamones;

    private int jamonesRecogidos;
    private bool[] puntosOcupados;

    private void Start()
    {
        puntosOcupados = new bool[puntosSpawn.Length];

        ActualizarContador();
        GenerarJamonesIniciales();
    }

    private void GenerarJamonesIniciales()
    {
        for (int i = 0; i < cantidadInicial; i++)
        {
            GenerarJamonAleatorio();
        }
    }

    public void RecogerJamon(int indicePunto)
    {
        if (jamonesRecogidos >= objetivoJamones)
        {
            return;
        }

        jamonesRecogidos++;

        if (indicePunto >= 0 && indicePunto < puntosOcupados.Length)
        {
            puntosOcupados[indicePunto] = false;
        }

        ActualizarContador();

        Debug.Log("Jamones recogidos: " + jamonesRecogidos);

        if (jamonesRecogidos >= objetivoJamones)
        {
            Debug.Log("¡Has conseguido los 50 jamones!");
            return;
        }

        if (jamonesRecogidos % 2 == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                GenerarJamonAleatorio();
            }
        }
    }

    private void ActualizarContador()
    {
        textoJamones.text = "Jamones: " + jamonesRecogidos + " / " + objetivoJamones;
    }

    private void GenerarJamonAleatorio()
    {
        List<int> puntosLibres = new List<int>();

        for (int i = 0; i < puntosSpawn.Length; i++)
        {
            if (!puntosOcupados[i])
            {
                puntosLibres.Add(i);
            }
        }

        if (puntosLibres.Count == 0)
        {
            Debug.LogWarning("No hay puntos libres para generar jamones.");
            return;
        }

        int indiceAleatorio = Random.Range(0, puntosLibres.Count);
        int indicePunto = puntosLibres[indiceAleatorio];

        puntosOcupados[indicePunto] = true;

        GameObject nuevoJamon = Instantiate(
            prefabJamon,
            puntosSpawn[indicePunto].position,
            Quaternion.identity
        );

        nuevoJamon.GetComponent<Coleccionables>()
            .ConfigurarPunto(this, indicePunto);
    }
}
