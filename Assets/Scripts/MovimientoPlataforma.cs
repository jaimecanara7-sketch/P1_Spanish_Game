using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float limiteIzquierdo = -7f;
    [SerializeField] private float limiteDerecho = 7f;
    [SerializeField] private float velocidad = 2f;

    private float posicionY;
    private float posicionZ;

    private void Start()
    {
        posicionY = transform.position.y;
        posicionZ = transform.position.z;
    }

    private void Update()
    {
        float movimiento = Mathf.PingPong(
            Time.time * velocidad,
            limiteDerecho - limiteIzquierdo
        ) + limiteIzquierdo;

        transform.position = new Vector3(
            movimiento,
            posicionY,
            posicionZ
        );
    }
}