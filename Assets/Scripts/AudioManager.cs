
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [Header("Musica")]
    [SerializeField] private AudioSource musica;
    [SerializeField] private float volumenNormal = 1f;
    [SerializeField] private float volumenReducido = 0.3f;

    [Header("Efectos")]
    [SerializeField] private AudioSource efectos;

    [Header("Atenuacion")]
    [SerializeField] private float duracionAtenuacion = 0.5f;

    private Coroutine atenuacionActual;

    private void Start()
    {
        musica.volume = volumenNormal;
        musica.Play();
    }

    public void ReproducirFx()
    {
        efectos.Play();

        if (atenuacionActual != null)
        {
            StopCoroutine(atenuacionActual);
        }

        atenuacionActual = StartCoroutine(AtenuarMusica());
    }

    private IEnumerator AtenuarMusica()
    {
        float volumenInicial = musica.volume;
        float tiempo = 0f;

        while (tiempo < duracionAtenuacion)
        {
            tiempo += Time.deltaTime;

            musica.volume = Mathf.Lerp(
                volumenInicial,
                volumenReducido,
                tiempo / duracionAtenuacion
            );

            yield return null;
        }

        musica.volume = volumenReducido;

        yield return new WaitForSeconds(1f);

        tiempo = 0f;

        while (tiempo < duracionAtenuacion)
        {
            tiempo += Time.deltaTime;

            musica.volume = Mathf.Lerp(
                volumenReducido,
                volumenNormal,
                tiempo / duracionAtenuacion
            );

            yield return null;
        }

        musica.volume = volumenNormal;
        atenuacionActual = null;
    }

    public void DetenerMusica()
    {
        if (atenuacionActual != null)
        {
            StopCoroutine(atenuacionActual);
            atenuacionActual = null;
        }

        musica.Stop();
    }
}
