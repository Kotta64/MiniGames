using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks
{
    private const float power = 100.0f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(photonView.IsMine) {
            if(Input.GetKey(KeyCode.W)){
                rb.AddForce(transform.forward * power);
            }
            if(Input.GetKey(KeyCode.S)){
                rb.AddForce(-transform.forward * power);
            }
            if(Input.GetKey(KeyCode.A)){
                rb.AddForce(-transform.right * power);
            }
            if(Input.GetKey(KeyCode.D)){
                rb.AddForce(transform.right * power);
            }
        }
    }
}
