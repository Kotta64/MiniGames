using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Game0 : MonoBehaviourPunCallbacks
{
    private int player_num;
    public Rigidbody ball;
    private GameObject player;
    private const float move_power = 100;
    // Start is called before the first frame update
    void Start()
    {
        var players = PhotonNetwork.PlayerList;
        if(players[0].NickName == GameManager.instance.player_name){
            player_num = 1;
            player = PhotonNetwork.Instantiate("Prefabs/Player1", new Vector3(0, 5, 40), Quaternion.Euler(0f, 180f, 0f));
            if(Random.Range(0, 2) == 1){
                PhotonNetwork.Instantiate("Prefabs/Ball", new Vector3(0, 25, -10), Quaternion.identity);
            }else{
                PhotonNetwork.Instantiate("Prefabs/Ball", new Vector3(0, 25, 10), Quaternion.identity);
            }
        }else{
            player_num = 2;
            player = PhotonNetwork.Instantiate("Prefabs/Player2", new Vector3(0, 5, -40), Quaternion.identity);
            Camera.main.gameObject.transform.parent.gameObject.transform.rotation = Quaternion.Euler(180f, 0f, 180f);
        }
    }

    void FixedUpdate() {
        if(player_num == PhotonNetwork.CurrentRoom.getTurn()){
            if(Input.GetKey(KeyCode.W)){
                ball.AddForce(player.transform.forward * move_power / 2f);
            }
            if(Input.GetKey(KeyCode.S)){
                ball.AddForce(-player.transform.forward * move_power / 2f);
            }
            if(Input.GetKey(KeyCode.A)){
                ball.AddForce(-player.transform.right * move_power);
            }
            if(Input.GetKey(KeyCode.D)){
                ball.AddForce(player.transform.right * move_power);
            }
        }
    }
}
