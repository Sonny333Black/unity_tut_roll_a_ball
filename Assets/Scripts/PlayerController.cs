using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;

    private int count;

    public float speed;
    public static event Action<int> countA;

    private void Start()
    {
        count = 0;    
        rb = GetComponent<Rigidbody>();        
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal,0.0f,moveVertical);

        rb.AddForce(movement*speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            GameObject.Destroy(other.gameObject);   
            if(countA != null)
            {
                count++;
                countA(count);
            }
        }
    }

    public void Reset()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity =Vector3.zero; 
        count = 0;
        countA(count);
        this.transform.position = Vector3.zero;
    }

}