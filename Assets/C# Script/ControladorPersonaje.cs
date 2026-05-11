using UnityEngine;

public class ControladorPersonaje : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float velocidad = 5f;
    public float fuerzaSalto = 12f;

    [Header("Detección de Suelo")]
    public bool estaEnSuelo;
    public Transform verificadorSuelo;
    public float radioSuelo = 0.2f;
    public LayerMask capaSuelo;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private float movimientoX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
        rb.freezeRotation = true;
    }

    void Update()
    {
        movimientoX = Input.GetAxisRaw("Horizontal");

        if (movimientoX > 0) sprite.flipX = false;
        else if (movimientoX < 0) sprite.flipX = true;

        // Verificamos que el transform de los pies no sea nulo antes de usarlo
        if (verificadorSuelo != null)
        {
            estaEnSuelo = Physics2D.OverlapCircle(verificadorSuelo.position, radioSuelo, capaSuelo);
        }

        if (Input.GetButtonDown("Jump") && estaEnSuelo)
        {
            // En Unity 6 usamos linearVelocity
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
        }

        if (anim != null)
        {
            anim.SetFloat("velocidad", Mathf.Abs(movimientoX));
        }
    }

    void FixedUpdate()
    {
        // En Unity 6 usamos linearVelocity
        rb.linearVelocity = new Vector2(movimientoX * velocidad, rb.linearVelocity.y);
    }

    private void OnDrawGizmos()
    {
        if (verificadorSuelo != null)
        {
            Gizmos.color = Color.red;
            // Corregido: DrawWireSphere con D mayúscula
            Gizmos.DrawWireSphere(verificadorSuelo.position, radioSuelo);
        }
    }
}