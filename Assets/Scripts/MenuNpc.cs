using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MenuNpc : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private float waypointDistanceMax = 10f;
    [SerializeField]
    private float waypointDistanceMin = 1f;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float idleProbability = 0.5f;
    [SerializeField]
    private float tripProbability = 0.05f;
    [SerializeField]
    private float getUpProbability = 0.05f;
    private float characterEventRate = 5f;
    private Vector3 currentDestination;
    private Animator anim;
    private float elapsed = 0f;
    private bool tripped = false;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        navMeshAgent.SetDestination(SelectRandomWayPoint());
        navMeshAgent.speed = speed;
    }

    private Vector3 SelectRandomWayPoint()
    {
        currentDestination = new Vector3(
            gameObject.transform.position.x + Random.Range(-waypointDistanceMax, waypointDistanceMax),
            gameObject.transform.position.y,
            gameObject.transform.position.z + Random.Range(-waypointDistanceMax, waypointDistanceMax));
        return currentDestination;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        // navMeshAgent.transform.LookAt(currentDestination);
        elapsed += Time.deltaTime;
        if(elapsed >= characterEventRate)
        {
            Debug.Log("Menu npc assessing next action");
            //should they trip?
            if (tripped)
            {
                if(Random.value <= getUpProbability)
                {
                    Debug.Log("Menu npc getting up");
                    anim.SetTrigger("GetUp");
                    navMeshAgent.isStopped = false;
                    elapsed = 0;
                    return;
                }
            }
            else
            {
                if (Random.value <= tripProbability)
                {
                    Debug.Log("Menu npc falling down");
                    navMeshAgent.isStopped = true;
                    anim.SetTrigger("Fall");
                    tripped = true;
                    elapsed = 0;
                    return;
                }
            }
            //should they just stand still?
            if (Random.value <= idleProbability)
            {
                Debug.Log("Menu npc idle");
                navMeshAgent.isStopped = true;
            }
            else
            {
                //should they find another place to wander?
                Debug.Log("Menu npc selecting waypoint");
                navMeshAgent.isStopped = false;
                if (navMeshAgent.remainingDistance < 1)
                {
                    navMeshAgent.SetDestination(SelectRandomWayPoint());
                }
            }
            elapsed = 0;
        }

    }
}
