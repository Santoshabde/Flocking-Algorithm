using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class World : MonoBehaviour
{
    [SerializeField, Required] public FlockingSettings agentSettings;
    [SerializeField, Required] private FlockingAgent flockingAgent;
    [SerializeField] private int numberOfAgentsToSpawn;

    //Fot testing purpose only.
    public Transform targetPosition;

    private List<FlockingAgent> worldFlockingAgents = new List<FlockingAgent>(); 
    private void Awake()
    {
        if (flockingAgent == null)
        {
            Debug.LogError("Agent Not Defined");
            return;
        }

        if (agentSettings == null)
        {
            Debug.LogError("World Settings Config for Flocking not Defined");
            return;
        }

        SpawnFlockingAgents();
    }

    private void SpawnFlockingAgents()
    {
        for (int i = 0; i < numberOfAgentsToSpawn; i++)
        {
            FlockingAgent agent = Instantiate(flockingAgent, new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f)), Quaternion.identity);
            worldFlockingAgents.Add(agent);
        }
    }

    public List<FlockingAgent> GetNeighbours(FlockingAgent agent, float radius) => worldFlockingAgents.Where(t => (Vector3.Distance(t.GetPosition(), agent.GetPosition()) < radius) && t != agent).ToList();
}
