using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks
{
    private const float move_power = 100.0f;
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
                rb.AddForce(transform.forward * move_power);
            }
            if(Input.GetKey(KeyCode.S)){
                rb.AddForce(-transform.forward * move_power);
            }
            if(Input.GetKey(KeyCode.A)){
                rb.AddForce(-transform.right * move_power);
            }
            if(Input.GetKey(KeyCode.D)){
                rb.AddForce(transform.right * move_power);
            }
        }
    }
}
