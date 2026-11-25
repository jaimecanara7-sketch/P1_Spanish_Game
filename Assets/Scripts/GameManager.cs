using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Vidas del jugador")]
    [SerializeField] private int vidasIniciales = 3;
    [SerializeField] private Image[] iconosCorazones;

    private int vidasActuales;

    public int VidasActuales => vidasActuales;

    private void Start()
    {
        vidasActuales = vidasIniciales;
        ActualizarCorazones();
    }

    public void PerderVida()
    {
        if (vidasActuales <= 0)
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
        Debug.Log("¡FIN DE PARTIDA!");
    }
}