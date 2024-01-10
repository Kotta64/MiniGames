using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountryController : MonoBehaviour
{
    private const float speed = 50.0f;
    private CharacterController controller;
    private Vector3 MoveDirection = Vector3.zero;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 moveDirection = new Vector3(0, 0, 0);
        if(Input.GetKey(KeyCode.A)){
            moveDirection -= new Vector3(1f, 0, 0);
        }
        if(Input.GetKey(KeyCode.D)){
            moveDirection += new Vector3(1f, 0, 0);
        }
        controller.Move(moveDirection * Time.deltaTime);
    }
}
