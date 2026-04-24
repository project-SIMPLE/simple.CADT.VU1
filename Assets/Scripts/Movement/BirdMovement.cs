using UnityEngine;

public class BirdOrbit : MonoBehaviour
{
    [Header("Orbit Settings")]
    public float orbitSpeed = 30f;
    public float orbitRadius = 2f;

    [Header("Bobbing Settings")]
    public float bobbingAmount = 0.3f;
    public float bobbingSpeed = 1f;

    [Header("Optional")]
    public bool randomDirection = true; 
    public bool fixModelForward = true; 

    float mySpeed;
    float myRadius;
    float myAngle;
    float myBobbingAmount;
    float myBobbingSpeed;
    Vector3 orbitPoint;

    void Start()
    {
        float direction = randomDirection ? (Random.Range(0, 2) == 0 ? 1f : -1f) : 1f;
        mySpeed = (orbitSpeed + Random.Range(-2f, 2f)) * direction;

        myRadius = orbitRadius + Random.Range(-0.5f, 0.5f);

        myBobbingSpeed = Mathf.Abs(bobbingSpeed + Random.Range(-0.5f, 0.5f));
        myBobbingAmount = bobbingAmount + Random.Range(-0.1f, 0.1f);

        myAngle = Random.Range(0f, 360f);

        Vector2 rand = Random.insideUnitCircle;
        orbitPoint = transform.position + new Vector3(rand.x, 0f, rand.y);
    }

    void Update()
    {
        myAngle += mySpeed * Time.deltaTime;
        myAngle %= 360f;

        float angleRad = myAngle * Mathf.Deg2Rad;

        float x = Mathf.Cos(angleRad) * myRadius;
        float z = Mathf.Sin(angleRad) * myRadius;

        float y = Mathf.Sin(Time.time * myBobbingSpeed) * myBobbingAmount;

        Vector3 offset = new Vector3(x, y, z);
        transform.position = orbitPoint + offset;

        Vector3 dir = (transform.position - orbitPoint).normalized;

        Vector3 tangent = new Vector3(-dir.z, 0f, dir.x);

        if (mySpeed < 0)
        {
            tangent = -tangent;
        }

        Quaternion rotation = Quaternion.LookRotation(tangent);

        if (fixModelForward)
        {
            rotation *= Quaternion.Euler(0, 180f, 0);
        }

        transform.rotation = rotation;
    }
}