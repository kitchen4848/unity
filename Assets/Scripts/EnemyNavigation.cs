using UnityEngine;

public class EnemyNavigation : MonoBehaviour {

	public Transform target;	
	NavMeshAgent agent;			

	void Start()
	{
		agent = GetComponent<NavMeshAgent> ();
    }

	void Update()
	{
		if (target && !agent.hasPath)
		{
			agent.SetDestination(target.position);
		}
	}
}
