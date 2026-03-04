using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

public class CreepCloser : MonoBehaviour
{
    public int iterations;
    public int maxIterations;
    public float iterationAmount;
    public float startingDis;
    public bool started;
    public Transform player;
    public bool reset;
    public bool xAxis;
    public bool pos;

    private void Update()
    {
        if (started && iterations < maxIterations)
        {
            if (!reset)
            {
                if (Vector3.Dot(player.forward, transform.forward) >= 0.86)
                {
                    reset = true;
                    // maxIterations = 3, iterations = 0, player transform = 5, this transform = 10;
                }
            }
            else
            {
                if (Vector3.Dot(player.forward, transform.forward) <= -0.86)
                {
                    reset = false;
                    Vector3 startingVector = transform.position;
                    transform.position = Vector3.MoveTowards(transform.position, player.position, startingDis / maxIterations);
                    if (xAxis)
                    {
                        //transform.position = new Vector3(transform.position.x, startingVector.y, startingVector.z);
                        if (pos) transform.position = new Vector3(transform.position.x - 2, startingVector.y, startingVector.z);
                        else transform.position = new Vector3(transform.position.x + 2, startingVector.y, startingVector.z);
                    }
                    else
                    {
                        //transform.position = new Vector3(startingVector.x, startingVector.y, transform.position.z);
                        if (pos) transform.position = new Vector3(startingVector.x, startingVector.y, transform.position.z - 1);
                        else transform.position = new Vector3(startingVector.x, startingVector.y, transform.position.z + 1);
                    }
                    iterations++;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !started)
        {
            if (Mathf.Abs(player.position.x - transform.position.x) >= Mathf.Abs(player.position.z - transform.position.z))
            {
                startingDis = Mathf.Abs(player.position.x - transform.position.x);
                xAxis = true;
            }

            else
            {
                startingDis = Mathf.Abs(player.position.z - transform.position.z);
                xAxis = false;
            }

            if (player.position.x > transform.position.x) pos = true;
            else pos = false;

            started = true;
        }
    }
}
