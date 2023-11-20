using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game0 : MonoBehaviour
{
    private int player;
    // Start is called before the first frame update
    void Start()
    {
        var players = PhotonNetwork.PlayerList;
        if(players[0].NickName == GameManager.instance.player_name){
            player = 1;
            PhotonNetwork.Instantiate("Prefabs/Player1", new Vector3(0, 5, 40), Quaternion.Euler(0f, 180f, 0f));
        }else{
            player = 2;
            PhotonNetwork.Instantiate("Prefabs/Player2", new Vector3(0, 5, -40), Quaternion.identity);
            Camera.main.gameObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
    }
}
