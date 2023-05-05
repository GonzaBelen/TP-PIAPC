using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public Camera cam;
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;
    public Transform cameraTransform; 

    private bool seGano = false; 

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

        if (Input.GetKeyDown(KeyCode.R) && seGano)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Time.timeScale = 1f;
            }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Esfera"))
        {
            seGano = true;
            Time.timeScale = 0f;
            Debug.Log("Felicitaciones, has ganado! Presiona R para reiniciar");
        }
    }
}