using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoPatrulla2 : MonoBehaviour {

	public Transform[] WayPoints;
	public Color ColorEstado = Color.green;

	private MaquinaDeEstados2 maquinaDeEstados;
	private ControladorNavMesh2 controladorNavMesh;
	private int siguienteWayPoint;

	void Awake () {
		maquinaDeEstados = GetComponent<MaquinaDeEstados2>();
		controladorNavMesh = GetComponent<ControladorNavMesh2>();
	}
	

	void Update () {
		if (controladorNavMesh.HemosLlegado()){
			siguienteWayPoint = (siguienteWayPoint + 1) % WayPoints.Length;
			ActualizarWayPointDestino();
		}
	}

	void OnEnable() {
		maquinaDeEstados.MeshRendererIndicador.material.color = ColorEstado;
		ActualizarWayPointDestino();
	}

	void ActualizarWayPointDestino() {
    	siguienteWayPoint = Random.Range(0, WayPoints.Length);
    	controladorNavMesh.ActualizarPuntoDestinoNavMeshAgent(WayPoints[siguienteWayPoint].position);
	}

	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Player") && enabled){
			maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoPersecucion);
		}
	}
}
