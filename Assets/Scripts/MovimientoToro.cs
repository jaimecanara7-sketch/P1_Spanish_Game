
using UnityEngine;

public class MovimientoToro : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidad = 2f;
    [SerializeField] private Transform limiteIzquierdo;
    [SerializeField] private Transform limiteDerecho;

    private bool moviendoDerecha = true;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log(animator.parameters.Length);
    }

    private void Update()
    {
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
            moviendoDerecha = false;
            animator.SetBool("MirandoDerecha", false);
        }
        else if (!moviendoDerecha &&
                 transform.position.x <= limiteIzquierdo.position.x)
        {
            moviendoDerecha = true;
            animator.SetBool("MirandoDerecha", true);
        }
    }
}
