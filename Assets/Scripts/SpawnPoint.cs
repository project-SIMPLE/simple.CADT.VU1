using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject xrOrigin;

    void Start()
    {
        if (spawnPoint != null && xrOrigin != null)
        {
            Invoke("MovePlayer", 0.2f); 
        }
        else
        {
            Debug.LogError("SpawnPoint or XROrigin not assigned!");
        }
    }

    void MovePlayer()
    {
        xrOrigin.transform.SetPositionAndRotation(
            spawnPoint.position,
            spawnPoint.rotation
        );

        Debug.Log("Spawn at: " + spawnPoint.position);
    }
}