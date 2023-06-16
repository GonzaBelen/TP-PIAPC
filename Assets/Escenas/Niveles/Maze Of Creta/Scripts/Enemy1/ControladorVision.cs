using UnityEngine;
using System.Collections;

public class ControladorVision : MonoBehaviour {

    public Transform Ojos;
    public float rangoVision = 20f;
    public float oscilacionAngulo = 30f;

    private ControladorNavMesh controladorNavMesh;
    private float anguloActual = 0f;

    void Awake()
    {
        controladorNavMesh = GetComponent<ControladorNavMesh>();
    }

    public bool PuedeVerAlJugador(out RaycastHit hit, bool mirarHaciaElJugador = false)
    {
        Vector3 vectorDireccion;
        if (mirarHaciaElJugador)
        {
            vectorDireccion = (controladorNavMesh.perseguirObjetivo.position) - Ojos.position;
        }else
        {
            anguloActual += Time.deltaTime * 40f;
            float oscilacion = Mathf.Sin(anguloActual) * oscilacionAngulo;
            vectorDireccion = Quaternion.Euler(0f, oscilacion, 0f) * Ojos.forward;
        }

        Debug.DrawRay(Ojos.position, vectorDireccion * rangoVision, Color.red);
        return Physics.Raycast(Ojos.position, vectorDireccion, out hit, rangoVision) && hit.collider.CompareTag("Player");
    }
}