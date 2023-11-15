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
        int stage_id = PhotonNetwork.LocalPlayer.getStageID();

        if (Input.GetKeyDown(KeyCode.A)){
            stage_id -= 1;
        }
        if (Input.GetKeyDown(KeyCode.D)){
            stage_id += 1;
        }
        stage_id = Mathf.Clamp(stage_id, 0, maxstage);
        if(stage_id != PhotonNetwork.LocalPlayer.getStageID()){
            PhotonNetwork.LocalPlayer.setStageID(stage_id);
        }
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

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        log_text.text += String.Format("\n{0}が退出しました．", otherPlayer.NickName);
    }

    public override void OnCreatedRoom() {
        PhotonNetwork.LocalPlayer.setStageID(0);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) {
        foreach (var prop in changedProps) {
            if (prop.Key.Equals(StageKey)) {
                log_text.text += "\n StageID: " + PhotonNetwork.LocalPlayer.getStageID();
            }
        }
    }
}
