using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EstadoPersecucion2 : MonoBehaviour {

    public Color ColorEstado = Color.red;
	public float timeSinceExit = 0f;

    private bool jugadorTocado = false;
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
        enPersecucion = false; // Establecer fuera de persecución al salir del estado
    }
	
	private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && enPersecucion)
        {
            jugadorTocado = true;
            Destroy(collision.collider.gameObject);
			Time.timeScale = 0f; // Pausar la escena            
        }
    }

	void Update () {
		timeSinceExit += Time.deltaTime;
    		if (timeSinceExit >= 5f) { 
				maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoPatrulla);        		
				timeSinceExit = 0f; 
    		}
        controladorNavMesh.ActualizarPuntoDestinoNavMeshAgent();   

		if (jugadorTocado && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f; // Reanudar la escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } 
	}

	public void OnTriggerExit(Collider other) {
		timeSinceExit = 0f;
	}
}