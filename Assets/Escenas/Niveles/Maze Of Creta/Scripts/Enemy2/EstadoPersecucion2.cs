using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EstadoPersecucion2 : MonoBehaviour {

    public Color ColorEstado = Color.red;
    public float timeSinceExit = 0f;

    private MaquinaDeEstados2 maquinaDeEstados;
    private ControladorNavMesh2 controladorNavMesh;
    private bool enPersecucion = false;

    void Awake () {
        maquinaDeEstados = GetComponent<MaquinaDeEstados2>();
        controladorNavMesh = GetComponent<ControladorNavMesh2>();
    }

    void OnEnable()
    {
        enPersecucion = true;
        maquinaDeEstados.MeshRendererIndicador.material.color = ColorEstado;
    }

    void OnDisable()
    {
        enPersecucion = false;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && enPersecucion)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void Update () {
        timeSinceExit += Time.deltaTime;
        if (timeSinceExit >= 5f) { 
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoPatrulla);                
            timeSinceExit = 0f; 
        }
        
        controladorNavMesh.ActualizarPuntoDestinoNavMeshAgent();   

        if (controladorNavMesh.perseguirObjetivo != null) {
        ObtenerVelocidadEnemigo();
        }
    }

    public void OnTriggerExit(Collider other) {
        timeSinceExit = 0f;
    }

    public float ObtenerVelocidadEnemigo()
    {
        float distanciaJugador = Vector3.Distance(transform.position, controladorNavMesh.perseguirObjetivo.position);
        
        if (distanciaJugador < 4f)
        {
            controladorNavMesh.velocidadRapida();
            return 3f; 
        }
        else if (distanciaJugador < 10f)
        {
            controladorNavMesh.velocidadMedia();
            return 2f;
        }
        else
        {
            controladorNavMesh.velocidadLenta();
            return 1f;
        }
    }
}