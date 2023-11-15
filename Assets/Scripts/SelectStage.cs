using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectStage : MonoBehaviourPunCallbacks
{
    Text log_text;
    private const int maxstage = 5;
    private const string StageKey = "StageID";
    private bool join_flag = false;

    void Start()
    {
        // デバック用
        GameManager.instance.player_name = "Kotta";
        GameManager.instance.roomID = "pacapaca";

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

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinOrCreateRoom(GameManager.instance.roomID, new RoomOptions(), TypedLobby.Default);
    }

    public override void OnJoinedRoom() {
        join_flag = true;
        log_text.text += String.Format("\n{0}が{1}に参加しました．", GameManager.instance.player_name, GameManager.instance.roomID);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        log_text.text += String.Format("\n{0}が{1}に参加しました．", newPlayer.NickName, GameManager.instance.roomID);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        log_text.text += String.Format("\n{0}が退出しました．", otherPlayer.NickName);
    }

    public override void OnCreatedRoom() {
        PhotonNetwork.CurrentRoom.setStageID(0);
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged) {
        foreach (var prop in propertiesThatChanged) {
            if (prop.Key.Equals(StageKey)) {
                int id = PhotonNetwork.CurrentRoom.getStageID();
                log_text.text += "\n StageID: " + id;
                GameManager.instance.stageID = id;
            }
        }
    }
}
