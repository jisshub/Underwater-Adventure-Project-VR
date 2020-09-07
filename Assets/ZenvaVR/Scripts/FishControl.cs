using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FishControl: MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float reverseSpeed;
    Rigidbody rb;

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
                
    }
    void Reverse()
    {
        transform.forward *= -1;
        rb.velocity = reverseSpeed * transform.forward;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Fish"))
        {
            Reverse();
        }
    }
}
