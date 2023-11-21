using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviourPunCallbacks
{
    private const float throw_power = 15f;

    private void Start() {
        if(photonView.IsMine){
            GameObject.Find("Game0").GetComponent<Game0>().ball = GetComponent<Rigidbody>();
            PhotonNetwork.CurrentRoom.setTurn(0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(photonView.IsMine){
            if(collision.gameObject.name=="Player1(Clone)"){
                GetComponent<Rigidbody>().velocity = new Vector3(0, 1f, -1f)*throw_power;
                PhotonNetwork.CurrentRoom.setTurn(1);
            }else if(collision.gameObject.name=="Player2(Clone)"){
                GetComponent<Rigidbody>().velocity = new Vector3(0, 1f, 1f)*throw_power;
                PhotonNetwork.CurrentRoom.setTurn(2);
            }else if(collision.gameObject.name=="Field" && PhotonNetwork.CurrentRoom.getTurn() != 0){
                PhotonNetwork.CurrentRoom.setTurn(0);
                setBall(1);
            }
        }
    }

    private void setBall(int player){
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); 
        if(player == 1){
            transform.position = new Vector3(0, 25, 10);
        }else{
            transform.position = new Vector3(0, 25, -10);
        }
    }
}
