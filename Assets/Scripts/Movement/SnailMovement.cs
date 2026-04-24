using UnityEngine;

public class SnailMovement : MonoBehaviour
{
    public float moveSpeed = 0.01f;
    public float moveDistance = 0.5f;
    private float distanceMoved = 0f;

    void Update()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.back * step, Space.Self);
        distanceMoved += step;

        if (distanceMoved >= moveDistance)
        {
            transform.Rotate(Vector3.up, -90f); // turn left
            distanceMoved = 0f;
        }
    }
}
