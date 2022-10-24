using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaunchTest : MonoBehaviour
{
    public GameObject launchObject;
    public Transform launchParent;
    public float launchForce;
    public float timeBetweenSpawn = 1f;
    private float timestamp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame && Time.time >= timestamp)
        {
            GameObject clone = Instantiate(launchObject, launchParent.position, launchParent.rotation) as GameObject;
            clone.transform.SetPositionAndRotation(launchParent.position, clone.transform.rotation);
            clone.GetComponent<Rigidbody>().velocity = launchParent.transform.forward * launchForce;
            timestamp = Time.time + timeBetweenSpawn;
        }
    }
}
