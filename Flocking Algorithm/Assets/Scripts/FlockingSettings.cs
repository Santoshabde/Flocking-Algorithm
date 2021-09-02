using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Flocking Agent Settings", menuName = "ScriptableObjects/Flocking Agent Settings", order = 1)]
public class FlockingSettings : ScriptableObject
{
    [Header("Cohention :")]
    public bool activateCohention;
    public float cohentionRadius;
    public float cohentionFactor;

    [Header("Seperation :")]
    public bool activateSeperation;
    public float seperationRadius;
    public float seperationFactor;

    [Header("Alignment :")]
    public bool activateAlignment;
    public float alignmentRadius;
    public float alignmentFactor;

    [Header("Target Reach Desire :")]
    public bool activateTagetReachedDesire;
    public float targetReachFactor;

    [Header("Agent Speed and Acceleration :")]
    public float maxAcceleration;
    public float maxVelocity;
}
