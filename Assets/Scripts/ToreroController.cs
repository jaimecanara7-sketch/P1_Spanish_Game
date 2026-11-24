
using UnityEngine;

public class ToreroController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidad = 6f;
    [SerializeField] private float fuerzaSalto = 12f;

    [Header("Deteccion del suelo")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float radioDeteccion = 0.15f;

    [Header("Animaciones")]
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;

    private float movimiento;
    private bool estaEnSuelo;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movimiento = Input.GetAxisRaw("Horizontal");

        estaEnSuelo = Physics2D.OverlapCircle(
            groundCheck.position,
            radioDeteccion,
            groundLayer
        );

        animator.SetFloat("MovimientoX", movimiento);

        if (Input.GetKeyDown(KeyCode.Space) && estaEnSuelo)
        {
            Saltar();
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            movimiento * velocidad,
            rb.linearVelocity.y
        );
    }

    private void Saltar()
    {
        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            fuerzaSalto
        );
    }
}
