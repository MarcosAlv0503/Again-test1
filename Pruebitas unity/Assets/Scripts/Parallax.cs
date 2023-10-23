using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector2 velMovimiento;
    private Vector2 offset;
    private Material material;
    private Rigidbody2D rbPlayer;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        rbPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        offset = (rbPlayer.velocity.x / 100) * velMovimiento * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
    /*[SerializeField] private Transform[] layers; // Capas del fondo
    [SerializeField] private float[] parallaxFactors; // Factores de parallax para cada capa
    [SerializeField] private float smoothing = 1f; // Suavizado del movimiento parallax

    private Vector3[] initialPositions; // Posiciones iniciales de las capas

    private void Start()
    {
        initialPositions = new Vector3[layers.Length];

        // Guardar las posiciones iniciales de las capas
        for (int i = 0; i < layers.Length; i++)
        {
            initialPositions[i] = layers[i].position;
        }
    }

    private void Update()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            float parallax = (initialPositions[i].x - transform.position.x) * parallaxFactors[i];

            Vector3 targetPosition = new Vector3(initialPositions[i].x + parallax, layers[i].position.y, layers[i].position.z);
            layers[i].position = Vector3.Lerp(layers[i].position, targetPosition, smoothing * Time.deltaTime);
        }
    }
    */
}