using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStage : MonoBehaviourPunCallbacks
{
    private Text log_text;
    private List<string> log_data = new List<string>();
    private const int maxstage = 5;
    private const string StageKey = "StageID";
    private bool join_flag = false;

    void Start()
    {
        // デバック用
        // GameManager.instance.player_name = "Kotta";
        // GameManager.instance.roomID = "pacapaca";

        log_text = GameObject.Find("Log").GetComponent<Text>();
        PhotonNetwork.NickName = GameManager.instance.player_name;
        PhotonNetwork.ConnectUsingSettings();
    }

    void Update() {
        // ステージ選択処理
        if (join_flag) {
            int stage_id = PhotonNetwork.CurrentRoom.getStageID();
            if (Input.GetKeyDown(KeyCode.A)){
                stage_id -= 1;
            }
            if (Input.GetKeyDown(KeyCode.D)){
                stage_id += 1;
            }
            stage_id = Mathf.Clamp(stage_id, 0, maxstage);
            if(stage_id != PhotonNetwork.CurrentRoom.getStageID()){
                PhotonNetwork.CurrentRoom.setStageID(stage_id);
            }
        }
    }

    private void addLog(string data) {
        log_data.Add(data);

        if(log_data.Count > 5) {
            log_data.RemoveAt(0);
        }

        log_text.text = "";
        foreach(string str in log_data) {
            log_text.text += "\n" + str;
        }
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinOrCreateRoom(GameManager.instance.roomID, new RoomOptions(), TypedLobby.Default);
    }

    public override void OnJoinedRoom() {
        join_flag = true;
        addLog(String.Format("{0}が{1}に参加しました．", GameManager.instance.player_name, GameManager.instance.roomID));
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        addLog(String.Format("{0}が{1}に参加しました．", newPlayer.NickName, GameManager.instance.roomID));
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        addLog(String.Format("{0}が退出しました．", otherPlayer.NickName));
    }

    public override void OnCreatedRoom() {
        PhotonNetwork.CurrentRoom.setStageID(0);
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged) {
        foreach (var prop in propertiesThatChanged) {
            if (prop.Key.Equals(StageKey)) {
                int id = PhotonNetwork.CurrentRoom.getStageID();
                addLog("StageID: " + id);
                GameManager.instance.stageID = id;
            }
        }
    }
}
