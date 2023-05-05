using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EstadoPersecucion : MonoBehaviour {

    public Color ColorEstado = Color.red;

    private bool jugadorTocado = false;
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
            jugadorTocado = true;
            Destroy(collision.collider.gameObject);
            Time.timeScale = 0f;
            Debug.Log("Haz perdido :( Presiona R para reiniciar");
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
        if (jugadorTocado && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        RaycastHit hit;
        if(!controladorVision.PuedeVerAlJugador(out hit, true))
        {
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoAlerta);
            return;
        }

        controladorNavMesh.ActualizarPuntoDestinoNavMeshAgent();    
    }
}