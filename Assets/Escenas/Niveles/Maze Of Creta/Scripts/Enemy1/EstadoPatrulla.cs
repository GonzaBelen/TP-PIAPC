using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoPatrulla : MonoBehaviour {

	public Transform[] WayPoints;
	public Color ColorEstado = Color.green;

	private ControladorVision controladorVision;
	private MaquinaDeEstados maquinaDeEstados;
	private ControladorNavMesh controladorNavMesh;
	private int siguienteWayPoint;

	void Awake () {
		maquinaDeEstados = GetComponent<MaquinaDeEstados>();
		controladorNavMesh = GetComponent<ControladorNavMesh>();
		controladorVision = GetComponent<ControladorVision>();
	}
	

	void Update () {
		RaycastHit hit;
		if (controladorVision.PuedeVerAlJugador(out hit)){
			controladorNavMesh.perseguirObjetivo = hit.transform;
			maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoPersecucion);
			return;
		}

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
}