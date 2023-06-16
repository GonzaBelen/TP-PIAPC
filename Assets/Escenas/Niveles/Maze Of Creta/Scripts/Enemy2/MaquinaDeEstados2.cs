using UnityEngine;
using System.Collections;

public class MaquinaDeEstados2 : MonoBehaviour {

    public EstadoPatrulla2 EstadoPatrulla;
    public EstadoPersecucion2 EstadoPersecucion;
    public MonoBehaviour EstadoInicial;
    public EstadoInvisible EstadoInvisible;

    public MeshRenderer MeshRendererIndicador;

    private MonoBehaviour estadoActual;


    void Start () {
        ActivarEstado(EstadoInicial);
    }

    //void Awake () {
    //    invisible = GetComponent<Invisible>();
    //}
    
    public void ActivarEstado(MonoBehaviour nuevoEstado)
    {
        if (estadoActual != null) estadoActual.enabled = false;
        estadoActual = nuevoEstado;
        estadoActual.enabled = true;
    }
}