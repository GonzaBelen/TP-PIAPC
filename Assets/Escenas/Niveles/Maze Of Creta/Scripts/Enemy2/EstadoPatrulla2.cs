using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoPatrulla2 : MonoBehaviour {

    public Transform[] WayPoints;
    public Color ColorEstado = Color.green;
    public bool estaInvisible = false;
    public int wayPointInvisible;

    private MaquinaDeEstados2 maquinaDeEstados;
    private ControladorNavMesh2 controladorNavMesh;
    private int wayPointActual;
    private int wayPointAnterior;
    private int contadorWayPoint;
  

    void Awake () {
        maquinaDeEstados = GetComponent<MaquinaDeEstados2>();
        controladorNavMesh = GetComponent<ControladorNavMesh2>();
    }
    
    void OnEnable() {
        wayPointAnterior = 0;
        contadorWayPoint = 0;
        estaInvisible = false;
        maquinaDeEstados.MeshRendererIndicador.material.color = ColorEstado;
        ActualizarWayPointDestino();        
    }

    void Update () {
        if (contadorWayPoint == wayPointInvisible){
            estaInvisible = true;
        }
        
		if (controladorNavMesh.perseguirObjetivo != null) {
            ObtenerVelocidadEnemigo();
		}

        if (estaInvisible == true) {
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoInvisible);
            return;
        }

        if (controladorNavMesh.HemosLlegado()){
            wayPointAnterior = wayPointActual;
            wayPointActual = Random.Range(0, WayPoints.Length);
            if (wayPointActual != wayPointAnterior ){            
            contadorWayPoint = contadorWayPoint + 1;
            }
            ActualizarWayPointDestino();
        }
    }

    void ActualizarWayPointDestino() {
        controladorNavMesh.ActualizarPuntoDestinoNavMeshAgent(WayPoints[wayPointActual].position);
    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && enabled){
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoPersecucion);
		}
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