using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishSprawner : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody rb;
    [SerializeField] int minSchoolSize = 1;
    [SerializeField] int maxSchoolSize = 1;
    [SerializeField] GameObject fishPrefab;
    [SerializeField] float activationDistance = 5;
    [SerializeField] float deactivationDistance = 30;
    [SerializeField] float reverseSpeed;

    List<GameObject> school = new List<GameObject>();
    bool activated = false;
    Vector3 GetRandomRotation()
    {
        Vector3 rotation = Vector3.zero;
        rotation.x = Random.Range(-45, 45);
        rotation.y = Random.Range(-45, 45);
        return rotation;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.forward * speed;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) ;
        {
            rb.velocity = Camera.main.transform.forward * speed;
        }
        // Determine the distance between the FishSpawner and the player.
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);

        if (!activated && (distance <= activationDistance))
        {
            SpawnSchool();
            activated = true;
        }
        else if (activated && (distance >= deactivationDistance))
        {
            DestroySchool();
            activated = false;
        }
    }
    void SpawnSchool()
    {
        int numFish = Random.Range(minSchoolSize, maxSchoolSize + 1);
        Vector3 vecObj = GetRandomRotation();
        for (int i = 0; i < numFish; i++)
        {
            school.Add(SpawnFish(vecObj));
        }
    }
    // instantiate, return single fish near the FishSpawner object
    GameObject SpawnFish(Vector3 vecObj)
    {
        GameObject newFish = Instantiate(fishPrefab, transform);

        float x = transform.position.x + Random.Range(-5f, 5f);
        float y = transform.position.y + Random.Range(-5f, 5f);
        float z = transform.position.z + Random.Range(-5f, 5f);

        newFish.transform.position = new Vector3(x, y, z);

        newFish.transform.eulerAngles = vecObj;
        return newFish;
    }
    void DestroySchool()
    {
        foreach (GameObject fish in school)
        {
            Destroy(fish);
        }
        school = new List<GameObject>();
    }
    void Reverse()
    {
        transform.forward *= -1;
        rb.velocity = reverseSpeed * transform.forward;
    }
    void OnTriggerEvent(Collider other)
    {
        if (!other.CompareTag("Fish"))
        {
            Reverse();
        }
    }
}
