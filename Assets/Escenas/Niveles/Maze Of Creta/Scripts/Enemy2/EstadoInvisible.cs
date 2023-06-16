using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoInvisible : MonoBehaviour {

	public Color ColorEstado = Color.blue;

	private Invisible invisible;
	private MaquinaDeEstados2 maquinaDeEstados;
	private ControladorNavMesh2 controladorNavMesh;

	void Awake (){
        maquinaDeEstados = GetComponent<MaquinaDeEstados2>();
		invisible = FindObjectOfType<Invisible>();
        controladorNavMesh = GetComponent<ControladorNavMesh2>();
	}
	
	void OnEnable () {
		invisible.enabled = true;
		maquinaDeEstados.MeshRendererIndicador.material.color = ColorEstado;
		controladorNavMesh.DetenerNavMeshAgent();
	}

	public void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && enabled){
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoPersecucion);
			invisible.enabled = false;
		}
	}		
}
