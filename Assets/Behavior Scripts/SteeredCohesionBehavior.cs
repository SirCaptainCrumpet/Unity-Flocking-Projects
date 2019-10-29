using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
public class SteeredCohesionBehavior : FlockBehavior
{
	Vector3 currentVelocity;
	[Range(0f, 5f)]
	public float agentSmoothTime = 0.5f;

	public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
	{
		// If no neighbors, return no adjustment
		if(context.Count == 0)
		{
			return Vector3.zero;
		}

		// Add all points and average
		Vector3 cohesionMove = Vector3.zero;
		foreach(Transform item in context)
		{
			cohesionMove += item.position;
		}
		cohesionMove /= context.Count;

		// Create offset from agent position
		cohesionMove -= agent.transform.position;
		cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, agentSmoothTime);
		return cohesionMove;
	}
}
