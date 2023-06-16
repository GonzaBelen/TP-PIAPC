using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class ControladorNavMesh : MonoBehaviour {

    [HideInInspector]
    public Transform perseguirObjetivo;

    private NavMeshAgent navMeshAgent;

	void Awake () {
        navMeshAgent = GetComponent<NavMeshAgent>();
	}
	
	public void ActualizarPuntoDestinoNavMeshAgent(Vector3 puntoDestino) {
        navMeshAgent.destination = puntoDestino;
        navMeshAgent.isStopped = false;
    }

    public void ActualizarPuntoDestinoNavMeshAgent()
    {
        ActualizarPuntoDestinoNavMeshAgent(perseguirObjetivo.position);
    }

    public void DetenerNavMeshAgent()
    {
        navMeshAgent.isStopped = true;
    }

	public bool HemosLlegado()
    {
        return navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending;
    }

    public void velocidadRapida()
    {
        Debug.Log("rapido");
        navMeshAgent.speed = 15;
    }

    public void velocidadMedia()
    {
        Debug.Log("medio");
        navMeshAgent.speed = 8;
    }

    public void velocidadLenta()
    {
        Debug.Log("lento");
        navMeshAgent.speed = 5;
    }
}