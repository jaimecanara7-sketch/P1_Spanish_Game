
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Vidas del jugador")]
    [SerializeField] private int vidasIniciales = 3;
    [SerializeField] private Image[] iconosCorazones;

    private int vidasActuales;
    private bool partidaTerminada;

    public int VidasActuales => vidasActuales;
    public bool PartidaTerminada => partidaTerminada;

    private void Start()
    {
        vidasActuales = vidasIniciales;
        partidaTerminada = false;
        ActualizarCorazones();
    }

    public void PerderVida()
    {
        if (vidasActuales <= 0 || partidaTerminada)
        {
            return;
        }

        vidasActuales--;
        ActualizarCorazones();

        Debug.Log("Vidas restantes: " + vidasActuales);

        if (vidasActuales <= 0)
        {
            FinDePartida();
        }
    }

    private void ActualizarCorazones()
    {
        for (int i = 0; i < iconosCorazones.Length; i++)
        {
            iconosCorazones[i].enabled = i < vidasActuales;
        }
    }

    private void FinDePartida()
    {
        partidaTerminada = true;

        AudioManager audioManager =
            FindFirstObjectByType<AudioManager>();

        if (audioManager != null)
        {
            audioManager.DetenerMusica();
            Debug.Log("MUSICA DETENIDA");
        }

        Time.timeScale = 0f;

        Debug.Log("¡FIN DE PARTIDA!");
    }
}
