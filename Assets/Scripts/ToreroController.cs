
using UnityEngine;

public class ToreroController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidad = 6f;
    [SerializeField] private float fuerzaSalto = 12f;

    [Header("Sprites")]
    [SerializeField] private Sprite spriteIdle;
    [SerializeField] private Sprite spriteCaminarDerecha;
    [SerializeField] private Sprite spriteCaminarIzquierda;

    [Header("Deteccion del suelo")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float radioDeteccion = 0.15f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float movimiento;
    private bool estaEnSuelo;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        movimiento = Input.GetAxisRaw("Horizontal");

        estaEnSuelo = Physics2D.OverlapCircle(
            groundCheck.position,
            radioDeteccion,
            groundLayer
        );

        if (Input.GetKeyDown(KeyCode.Space) && estaEnSuelo)
        {
            Saltar();
        }

        ActualizarSprite();
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

    private void ActualizarSprite()
    {
        if (movimiento > 0)
        {
            spriteRenderer.sprite = spriteCaminarDerecha;
        }
        else if (movimiento < 0)
        {
            spriteRenderer.sprite = spriteCaminarIzquierda;
        }
        else
        {
            spriteRenderer.sprite = spriteIdle;
        }
    }
}
