using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomMain : MonoBehaviourPunCallbacks
{
    private Text log_text;
    private Text players_text;
    private List<string> log_data = new List<string>();
    private const string StartFlagKey = "StartFlag";

    void Start()
    {
        //GameManager.instance.player_name = "Kotta";
        //GameManager.instance.roomID = "zen3";

        log_text = GameObject.Find("LogText").GetComponent<Text>();
        PhotonNetwork.NickName = GameManager.instance.player_name;
        PhotonNetwork.ConnectUsingSettings();

        GameObject.Find("RoomName").GetComponent<Text>().text = "部屋名 : " + GameManager.instance.roomID;

        players_text = GameObject.Find("Players").GetComponent<Text>();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Return)){
            OnClick();
        }
    }

    private void addLog(string data) {
        log_data.Add(data);

        if(log_data.Count > 5) {
            log_data.RemoveAt(0);
        }

        log_text.text = "";
        foreach(string str in log_data) {
            log_text.text += str + "\n";
        }
    }

    private void updatePlayers() {
        var player_list = PhotonNetwork.PlayerList;
        players_text.text = player_list[0].NickName;
        if (player_list.Length > 1){
            players_text.text += "\n" + player_list[1].NickName;
        }
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinOrCreateRoom(GameManager.instance.roomID, new RoomOptions(){MaxPlayers = 2}, TypedLobby.Default);
    }

    public override void OnJoinedRoom() {
        addLog(String.Format("{0}が{1}に参加しました", GameManager.instance.player_name, GameManager.instance.roomID));
        GameManager.instance.stageID = PhotonNetwork.CurrentRoom.getStageID();
        updatePlayers();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        addLog(String.Format("{0}が{1}に参加しました", newPlayer.NickName, GameManager.instance.roomID));
        updatePlayers();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        addLog(String.Format("{0}が退出しました", otherPlayer.NickName));
        updatePlayers();
    }

    public override void OnCreatedRoom() {
        PhotonNetwork.CurrentRoom.setStageID(0);
    }

    public void OnClick() {
        if (PhotonNetwork.PlayerList.Length > 1) {
            PhotonNetwork.CurrentRoom.setStartFlag(1);
        }else{
            addLog("プレイヤー人数が足りません");
        }
    } 

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged) {
        foreach (var prop in propertiesThatChanged) {
            if (prop.Key.Equals(StartFlagKey) && PhotonNetwork.CurrentRoom.getStartFlag() == 1) {
                SceneManager.LoadScene("SelectGameScene");
            }
        }
    }
}

