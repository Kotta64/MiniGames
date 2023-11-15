using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetWork : MonoBehaviourPunCallbacks
{
    Text log_text;
    // Start is called before the first frame update
    void Start()
    {
        log_text = GameObject.Find("Log").GetComponent<Text>();
        PhotonNetwork.NickName = GameManager.instance.player_name;
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinOrCreateRoom(GameManager.instance.roomID, new RoomOptions(), TypedLobby.Default);
    }

    public override void OnJoinedRoom() {
        log_text.text += String.Format("\n{0}が{1}に参加しました．", GameManager.instance.player_name, GameManager.instance.roomID);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        log_text.text += String.Format("\n{0}が{1}に参加しました．", newPlayer.NickName, GameManager.instance.roomID);
    }
}
