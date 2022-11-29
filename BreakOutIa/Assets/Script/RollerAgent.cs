using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class RollerAgent : Agent
{
    Rigidbody2D rBody;

    public bool bateu;

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        bateu = false;
    }

    public Transform target;
    public override void OnEpisodeBegin()
    {
        bateu = false;
       // If the ball fell, -4 (position plataorma) its momentum
        if (target.transform.localPosition.y < -5)
        { 
            target.GetComponent<Rigidbody2D>().angularVelocity = 0f;
            target.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            target.transform.localPosition = new Vector3(0, 2.5f, 0);
            target.GetComponent<Bola>().time = 0f;
            target.GetComponent<Bola>().InitBall();
        }

        this.transform.localPosition = new Vector2(0f, -4f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(target.GetComponent<Rigidbody2D>().velocity);
        sensor.AddObservation(target.transform.rotation);

    }

    public float forceMultiplier = 5;
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2 DELETE
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];

        transform.localPosition += controlSignal * Time.deltaTime * forceMultiplier;

        //rBody.AddForce(controlSignal * forceMultiplier);
        Debug.Log("Control signals: " + controlSignal.x.ToString() + " ");

        // Rewards
        float distance = Vector3.Distance(new Vector3(this.transform.localPosition.x, 0f, 0f), new Vector3(target.localPosition.x, 0f, 0f));

        if (distance > 0.3f || distance < -0.3f)
        {
            SetReward(0.2f);
        }
        // Reached target
        if (bateu)
        {
            Debug.Log("Na sorte");
            SetReward(1.0f);
            EndEpisode();
        }
        // Fell off platform
        else if (target.transform.localPosition.y < -5)
        {
            Debug.Log("To passando aqui");
            SetReward(-1.0f);
            EndEpisode();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Bola"))
        {
            bateu = true;
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        Debug.Log("entrou");
    }


}
