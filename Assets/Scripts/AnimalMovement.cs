using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class AnimalMovement : MonoBehaviour
{
    public float wanderRadius = 5f;
    public float wanderTimer = 3f;

    private NavMeshAgent agent;
    private XRGrabInteractable grab;
    private Rigidbody rb;

    private float timer;
    private bool isGrabbed = false;
    public float rotationSpeed = 120f;

    private Vector3 targetPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        grab = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        timer = wanderTimer;
        agent.updateRotation = false;

        // XR events
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    void Update()
    {
        if (isGrabbed) return;

        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);

            // Store target position
            targetPosition = newPos;

            // Stop movement first
            agent.isStopped = true;

            timer = 0;
        }

        // If we have a target, rotate first
        if (targetPosition != Vector3.zero)
        {
            Vector3 direction = (targetPosition - transform.position);
            direction.y = 0;

            if (direction.magnitude > 0.1f)
            {
                Quaternion targetRot = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRot,
                    rotationSpeed * Time.deltaTime
                );

                // Check if facing target
                float angle = Quaternion.Angle(transform.rotation, targetRot);

                if (angle < 5f)
                {
                    // Start moving AFTER rotation
                    agent.SetDestination(targetPosition);
                    agent.isStopped = false;

                    targetPosition = Vector3.zero;
                }
            }
        }

        // Only rotate when NOT moving
        if (agent.isStopped && targetPosition != Vector3.zero)
        {
            Vector3 direction = (targetPosition - transform.position);
            direction.y = 0;

            if (direction.magnitude > 0.1f)
            {
                Quaternion targetRot = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRot,
                    rotationSpeed * Time.deltaTime
                );

                float angle = Quaternion.Angle(transform.rotation, targetRot);

                if (angle < 5f)
                {
                    // Start moving AFTER rotation is done
                    agent.SetDestination(targetPosition);
                    agent.isStopped = false;

                    targetPosition = Vector3.zero;
                }
            }
        }
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;

        // Stop NavMesh
        agent.ResetPath();
        agent.enabled = false;

        // Enable physics so hand can move it
        rb.isKinematic = false;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;

        // Disable physics again
        rb.isKinematic = true;

        // Re-enable NavMesh
        agent.enabled = true;

        // Fix position sync (IMPORTANT)
        agent.Warp(transform.position);

        // Reset timer so it doesn’t instantly jump
        timer = 0;
    }

    Vector3 RandomNavSphere(Vector3 origin, float dist, int layerMask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layerMask);

        return navHit.position;
    }
}