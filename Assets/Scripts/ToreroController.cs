
using UnityEngine;
using System.Collections;

public class ToreroController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidad = 6f;
    [SerializeField] private float fuerzaSalto = 7f;

    [Header("Golpe")]
    [SerializeField] private float fuerzaEmpuje = 9f;
    [SerializeField] private float fuerzaVertical = 3f;
    [SerializeField] private float tiempoSinControl = 0.3f;

    [Header("Deteccion del suelo")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float radioDeteccion = 0.15f;

    [Header("Animaciones")]
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private float movimiento;
    private bool estaEnSuelo;
    private bool recibiendoGolpe;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (recibiendoGolpe)
        {
            return;
        }

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
        if (recibiendoGolpe)
        {
            return;
        }

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

    public void RecibirGolpe(float direccion)
    {
        if (recibiendoGolpe)
        {
            return;
        }

        StartCoroutine(AnimarGolpe(direccion));
    }

    private IEnumerator AnimarGolpe(float direccion)
    {
        recibiendoGolpe = true;
        movimiento = 0f;

        animator.SetFloat("MovimientoX", 0f);

        rb.linearVelocity = Vector2.zero;

        rb.AddForce(
            new Vector2(direccion * fuerzaEmpuje, fuerzaVertical),
            ForceMode2D.Impulse
        );

        yield return new WaitForSeconds(tiempoSinControl);

        recibiendoGolpe = false;
    }
}
