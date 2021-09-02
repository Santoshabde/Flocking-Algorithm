using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingAgent : MonoBehaviour
{
    private World agentWorld;
    private Vector3 agentVelocity = Vector3.zero;
    private Vector3 agentAcceleration;

    #region Unity Methods
    private void Awake()
    {
        agentWorld = FindObjectOfType<World>();

        agentVelocity = new Vector3(Random.Range(0.1f, -0.1f), Random.Range(0.1f, -0.1f), Random.Range(0.1f, -0.1f));
    }


    private void Update()
    {
        agentAcceleration = (agentWorld.agentSettings.alignmentFactor * Alignment())
                          + (agentWorld.agentSettings.cohentionFactor * Cohention())
                          + (agentWorld.agentSettings.seperationFactor * Separation())
                          + (agentWorld.agentSettings.targetReachFactor * FaceTargetposition());

        agentAcceleration = Vector3.ClampMagnitude(agentAcceleration, agentWorld.agentSettings.maxAcceleration);
       
        agentVelocity += agentAcceleration * Time.deltaTime;
        agentVelocity = Vector3.ClampMagnitude(agentVelocity, agentWorld.agentSettings.maxVelocity);

        transform.position += agentVelocity * Time.deltaTime;

        transform.forward = agentVelocity;
    }

    #endregion

    #region Flocking Calculations
    private Vector3 Cohention()
    {
        if(!agentWorld.agentSettings.activateCohention)
            return Vector3.zero;

        //Calculate the center of agents
        List<FlockingAgent> neighbours = agentWorld.GetNeighbours(this, agentWorld.agentSettings.cohentionRadius);

        if (neighbours.Count == 0)
            return Vector3.zero;

        //Averaging positions of all neighbor agents to calculate center
        Vector3 sumOfNeightbourPositions = Vector3.zero;
        foreach (var item in neighbours)
        {
            sumOfNeightbourPositions += item.GetPosition();
        }

        Vector3 center = sumOfNeightbourPositions / neighbours.Count;

        return (center - GetPosition()).normalized;
    }

    private Vector3 Separation()
    {
        if (!agentWorld.agentSettings.activateSeperation)
            return Vector3.zero;

        Vector3 separationVector = new Vector3();

        //Calculate neighbors of an agent
        List<FlockingAgent> neighbours = agentWorld.GetNeighbours(this, agentWorld.agentSettings.seperationRadius);

        if (neighbours.Count == 0)
            return Vector3.zero;

        foreach (var item in neighbours)
        {
            //Individual seperation vector from a neighbor agent
            Vector3 direction = this.GetPosition() - item.GetPosition();

            if(direction.magnitude > 0)
            {
                //Calculating resultant separation Vector.
                separationVector += (direction.normalized) / (direction.magnitude);
            }
        }

        return separationVector.normalized;
    }

    private Vector3 Alignment()
    {
        if (!agentWorld.agentSettings.activateAlignment)
            return Vector3.zero;

        Vector3 alignment = new Vector3();

        List<FlockingAgent> neighbours = agentWorld.GetNeighbours(this, agentWorld.agentSettings.alignmentRadius);

        if (neighbours.Count == 0)
            return Vector3.zero;

        foreach (var item in neighbours)
        {
            alignment += item.agentVelocity;
        }

        return alignment.normalized;
    }

    public Vector3 FaceTargetposition()
    {
        if (!agentWorld.agentSettings.activateTagetReachedDesire)
            return Vector3.zero;

        return (agentWorld.targetPosition.position - this.GetPosition()).normalized;
    }

    #endregion


    #region Agent Helper Function
    public Vector3 GetPosition() => transform.position;

    #endregion

}
