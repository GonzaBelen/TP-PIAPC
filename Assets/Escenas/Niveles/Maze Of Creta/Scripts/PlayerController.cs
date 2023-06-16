using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public Camera cam;
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;
    public Transform cameraTransform; 

    void Start()
    {
        agent.updateRotation = false;
    }

    void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }

        cameraTransform.position = transform.position + new Vector3(0, 9, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Esfera"))
        {
            SceneManager.LoadScene("Nivel 2");
        }

        if (collision.gameObject.CompareTag("Esfera1")){
            SceneManager.LoadScene("Ganar");
        }

    }
}