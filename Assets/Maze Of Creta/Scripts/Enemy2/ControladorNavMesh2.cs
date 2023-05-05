using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class ControladorNavMesh2 : MonoBehaviour {

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
		if (perseguirObjetivo != null)
    	{
        	ActualizarPuntoDestinoNavMeshAgent(perseguirObjetivo.position);
    	}
    	else
    	{
        	Debug.LogWarning("La variable perseguirObjetivo no está asignada en " + gameObject.name);
    	}
    }

    public void DetenerNavMeshAgent()
    {
        navMeshAgent.isStopped = true;
    }

	    public bool HemosLlegado()
    {
        return navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending;
    }
}