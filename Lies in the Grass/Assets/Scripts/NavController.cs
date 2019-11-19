using UnityEngine;
using UnityEngine.AI;

public class NavController : MonoBehaviour {

    private NavMeshAgent agent;
    private bool isMomma;

    private Vector3[] mommasTargets;
    private int nextTarget;


    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        isMomma = gameObject.CompareTag("BigMomma");
        nextTarget = 0;

        if (isMomma) {
            mommasTargets = WaypointList.OuterPoints;
            SelectNextTarget();
        } else {
            mommasTargets = null;
            SelectPatrolTarget();
        }
    }

    private void Update() {
        if (agent.isOnNavMesh && agent.remainingDistance < agent.stoppingDistance) {
            if (isMomma) {
                SelectNextTarget();
            } else {
                SelectPatrolTarget();
            }
        }
    }

    public void SelectPatrolTarget(Vector3 newTarget = default) {
        if (newTarget == default) {
            if (agent.isOnNavMesh) {
                agent.SetDestination(WaypointList.RandomPosition);
            }
        } else {
            agent.SetDestination(newTarget);
        }

    }

    private void SelectNextTarget() {
        agent.SetDestination(mommasTargets[nextTarget]);
        if (++nextTarget >= mommasTargets.Length) {
            nextTarget = 0;
        }
    }
}
