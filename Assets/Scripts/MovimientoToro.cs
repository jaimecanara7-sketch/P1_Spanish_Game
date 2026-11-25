
using UnityEngine;

public class MovimientoToro : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidad = 2f;
    [SerializeField] private Transform limiteIzquierdo;
    [SerializeField] private Transform limiteDerecho;

    private bool moviendoDerecha = true;
    private Animator animator;
    private GameManager gameManager;
    private AudioManager audioManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = FindFirstObjectByType<GameManager>();
        audioManager = FindFirstObjectByType<AudioManager>();

        animator.SetBool("MirandoDerecha", true);
    }

    private void Update()
    {
        if (gameManager.PartidaTerminada)
        {
            return;
        }

        MoverToro();
    }

    private void MoverToro()
    {
        float direccion = moviendoDerecha ? 1f : -1f;

        transform.Translate(
            Vector2.right * direccion * velocidad * Time.deltaTime
        );

        if (moviendoDerecha &&
            transform.position.x >= limiteDerecho.position.x)
        {
            CambiarDireccion();
        }
        else if (!moviendoDerecha &&
                 transform.position.x <= limiteIzquierdo.position.x)
        {
            CambiarDireccion();
        }
    }

    private void CambiarDireccion()
    {
        moviendoDerecha = !moviendoDerecha;
        animator.SetBool("MirandoDerecha", moviendoDerecha);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        float direccionEmpuje = moviendoDerecha ? -1f : 1f;

        ToreroController torero =
            collision.gameObject.GetComponent<ToreroController>();

        if (torero != null)
        {
            torero.RecibirGolpe(direccionEmpuje);
        }

        gameManager.PerderVida();
        audioManager.ReproducirFx();

        CambiarDireccion();
    }
}
