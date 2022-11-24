using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class RollerAgent : Agent
{
    Rigidbody2D rBody;
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    public Transform target;
    public override void OnEpisodeBegin()
    {
       // If the ball fell, -4 (position plataorma) its momentum
        if (target.transform.localPosition.y < -4)
        { 
            target.GetComponent<Rigidbody2D>().angularVelocity = 0f;
            target.transform.localPosition = new Vector3(0, 2.5f, 0);
            target.GetComponent<Bola>().time = 0f;
            target.GetComponent<Bola>().InitBall();
        }

        this.transform.position = new Vector2(0f, -4f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        sensor.AddObservation(rBody.velocity.x);
    }

    public float forceMultiplier = 5;
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2 DELETE
        Vector2 controlSignal = Vector2.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];

        rBody.AddForce(controlSignal * forceMultiplier);
        Debug.Log("Control signals: " + controlSignal.x.ToString() + " ");

        // Rewards
        float distanceToTarget = Vector2.Distance(this.transform.localPosition, target.localPosition);

        // Reached target
        if (distanceToTarget < 0.5f)
        {
            Debug.Log("Na sorte");
            SetReward(1.0f);
            //EndEpisode();
        }

        // Fell off platform
        else if (target.transform.localPosition.y < -4)
        {
            Debug.Log("To passando aqui");
            target.GetComponent<Rigidbody2D>().angularVelocity = 0f;
            target.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            SetReward(-2.0f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        Debug.Log("entrou");
    }


}
