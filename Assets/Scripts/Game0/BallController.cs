using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviourPunCallbacks
{
    private const float throw_power = 15f;
    private const float move_power = 100;
    private Rigidbody rb;
    private int[] reverse = {2, 1};

    private void Start() {
        rb = GetComponent<Rigidbody>();
        if(photonView.IsMine){
            PhotonNetwork.CurrentRoom.setTurn(0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var turn = PhotonNetwork.CurrentRoom.getTurn();
        if(photonView.IsMine){
            if(collision.gameObject.name=="Player1(Clone)"){
                rb.velocity = new Vector3(0, 1f, -1f)*throw_power;
                PhotonNetwork.CurrentRoom.setTurn(1);
            }else if(collision.gameObject.name=="Player2(Clone)"){
                rb.velocity = new Vector3(0, 1f, 1f)*throw_power;
                PhotonNetwork.CurrentRoom.setTurn(2);
            }else if(collision.gameObject.name=="Field" && turn != 0){
                PhotonNetwork.CurrentRoom.setTurn(0);
                Game0.instance.next(turn);
                setBall(reverse[turn-1]);
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

    [PunRPC]
    private void controllBall(int direction, int player){
        int branch = player * 2 - 3;
        if(photonView.IsMine){
            switch(direction){
                case 0:
                    rb.AddForce(new Vector3(0, 0, 1*branch) * move_power / 2f);
                    break;
                case 1:
                    rb.AddForce(new Vector3(0, 0, -1*branch) * move_power / 2f);
                    break;
                case 2:
                    rb.AddForce(new Vector3(-1*branch, 0, 0) * move_power);
                    break;
                case 3:
                    rb.AddForce(new Vector3(1*branch, 0, 0) * move_power);
                    break;
            }
        }
    }
}
