using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayMachine : MonoBehaviour
{
    public GameObject hayBalePrefab; //Reference to the Hay Bale prefab.
    public Transform haySpawnpoint; //The point from which the hay will to be shot.
    public float shootInterval; //The smallest amount of time between shots
    private float shootTimer; 
    public float movementSpeed;
    public float horizontalBoundary = 22;
    public Transform modelParent; // 1

    // 2
    public GameObject blueModelPrefab;
    public GameObject yellowModelPrefab;
    public GameObject redModelPrefab;
    // Start is called before the first frame update
    private void LoadModel() {
        Destroy(modelParent.GetChild(0).gameObject); // 1

        switch (GameSettings.hayMachineColor) // 2
        {
            case HayMachineColor.Blue:
                Instantiate(blueModelPrefab, modelParent);
            break;

            case HayMachineColor.Yellow:
                Instantiate(yellowModelPrefab, modelParent);
            break;

            case HayMachineColor.Red:
                Instantiate(redModelPrefab, modelParent);
            break;
        }
    }
    void Start()    {
        LoadModel();
    }

    // Update is called once per frame
    void Update()    {
        UpdateMovement();
        UpdateShooting();
    }
    private void UpdateMovement()        {
        Debug.Log(transform.position.x);
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // 1
      
        if (horizontalInput < 0 && transform.position.x > -horizontalBoundary) // 1
        {
            Debug.Log("left");
            transform.Translate(transform.right * -movementSpeed * Time.deltaTime);
        }
        else if (horizontalInput > 0 && transform.position.x < horizontalBoundary) // 2
        {
            Debug.Log("right");
            transform.Translate(transform.right * movementSpeed * Time.deltaTime);
        }
    }
    private void UpdateShooting() {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0 && Input.GetKey(KeyCode.Space)) {
            shootTimer = shootInterval;
            ShootHay();
        }
    }
    private void ShootHay()    {
        SoundManager.Instance.PlayShootClip();
        Instantiate(hayBalePrefab, haySpawnpoint .position, Quaternion.identity);
    }


}
