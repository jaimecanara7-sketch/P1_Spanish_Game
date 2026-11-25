
using UnityEngine;

public class Coleccionables : MonoBehaviour
{
    private GeneradorColeccionables generador;
    private int indicePunto = -1;
    private bool recogido;

    public void ConfigurarPunto(
        GeneradorColeccionables nuevoGenerador,
        int nuevoIndice)
    {
        generador = nuevoGenerador;
        indicePunto = nuevoIndice;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (recogido || !collision.CompareTag("Player"))
        {
            return;
        }

        recogido = true;

        if (generador == null)
        {
            generador = FindFirstObjectByType<GeneradorColeccionables>();
        }

        if (generador != null)
        {
            generador.RecogerJamon(indicePunto);
        }

        Destroy(gameObject);
    }
}
