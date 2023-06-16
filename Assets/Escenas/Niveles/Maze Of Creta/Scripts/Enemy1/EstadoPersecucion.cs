using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EstadoPersecucion : MonoBehaviour {

    public Color ColorEstado = Color.red;

    //private bool jugadorTocado = false;
    private MaquinaDeEstados maquinaDeEstados;
    private ControladorNavMesh controladorNavMesh;
    private ControladorVision controladorVision;
    private bool enPersecucion = false;

    void Awake () {
        maquinaDeEstados = GetComponent<MaquinaDeEstados>();
        controladorNavMesh = GetComponent<ControladorNavMesh>();
        controladorVision = GetComponent<ControladorVision>();
    }

    private void OnCollisionEnter(Collision collision)
    {
         if (collision.collider.CompareTag("Player") && enPersecucion)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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
    
    void Update () {
        //if (jugadorTocado)
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}

        RaycastHit hit;
        if(!controladorVision.PuedeVerAlJugador(out hit, true))
        {
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoAlerta);
            return;
        }

        controladorNavMesh.ActualizarPuntoDestinoNavMeshAgent();    
		if (controladorNavMesh.perseguirObjetivo != null) {
        ObtenerVelocidadEnemigo();
		}
    }

        public float ObtenerVelocidadEnemigo()
    {
        float distanciaJugador = Vector3.Distance(transform.position, controladorNavMesh.perseguirObjetivo.position);
        
        if (distanciaJugador < 5.5f)
        {
            controladorNavMesh.velocidadRapida();
            return 3f; // Velocidad rápida
        }
        else if (distanciaJugador < 10f)
        {
            controladorNavMesh.velocidadMedia();
            return 2f; // Velocidad media
        }
        else
        {
            controladorNavMesh.velocidadLenta();
            return 1f; // Velocidad lenta
        }
    }
}