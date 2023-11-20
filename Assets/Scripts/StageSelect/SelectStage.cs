using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectStage : MonoBehaviourPunCallbacks
{
    private Text log_text;
    private List<string> log_data = new List<string>();
    private const string StageKey = "StageID";
    private const string StartWindowKey = "StartWindow";
    private const string StartFlagKey = "StartFlag";
    private bool join_flag = false;
    private GameObject window;

    void Start()
    {
        //GameManager.instance.player_name = "Kotta";
        //GameManager.instance.roomID = "zen3";

        log_text = GameObject.Find("Log").GetComponent<Text>();
        PhotonNetwork.NickName = GameManager.instance.player_name;
        PhotonNetwork.ConnectUsingSettings();

        window = GameObject.Find("StartWindow");
        window.SetActive(false);
    }

    void Update() {
        // ステージ選択処理
        if (join_flag) {
            int stage_id = PhotonNetwork.CurrentRoom.getStageID();
            if (Input.GetKeyDown(KeyCode.A) && !(PhotonNetwork.CurrentRoom.getStartWindow())){
                stage_id -= 1;
            }
            if (Input.GetKeyDown(KeyCode.D) && !(PhotonNetwork.CurrentRoom.getStartWindow())){
                stage_id += 1;
            }
            stage_id = Mathf.Clamp(stage_id, 0, GameManager.instance.game_count-1);
            if(stage_id != PhotonNetwork.CurrentRoom.getStageID()){
                PhotonNetwork.CurrentRoom.setStageID(stage_id);
            }
            if (Input.GetKeyDown(KeyCode.Return) && PhotonNetwork.PlayerList.Length >= 2 && window.activeSelf){
                OnClick_start();
            }
            if (Input.GetKeyDown(KeyCode.Return) && PhotonNetwork.PlayerList.Length >= 2 && !(window.activeSelf)){
                PhotonNetwork.CurrentRoom.setStartWindow(true);
                window.SetActive(true);
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
        PhotonNetwork.JoinOrCreateRoom(GameManager.instance.roomID, new RoomOptions(){MaxPlayers = 2}, TypedLobby.Default);
    }

    public override void OnJoinedRoom() {
        join_flag = true;
        addLog(String.Format("{0}が{1}に参加しました", GameManager.instance.player_name, GameManager.instance.roomID));
        GameManager.instance.stageID = PhotonNetwork.CurrentRoom.getStageID();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        addLog(String.Format("{0}が{1}に参加しました", newPlayer.NickName, GameManager.instance.roomID));
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        addLog(String.Format("{0}が退出しました", otherPlayer.NickName));
    }

    public override void OnCreatedRoom() {
        PhotonNetwork.CurrentRoom.setStageID(0);
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged) {
        foreach (var prop in propertiesThatChanged) {
            if (prop.Key.Equals(StageKey)) {
                int id = PhotonNetwork.CurrentRoom.getStageID();
                GameManager.instance.stageID = id;
                addLog(id.ToString());
            }
            if (prop.Key.Equals(StartWindowKey)) {
                window.SetActive(PhotonNetwork.CurrentRoom.getStartWindow());
            }
            if (prop.Key.Equals(StartFlagKey)) {
                SceneManager.LoadScene(String.Format("Game{0}Scene", GameManager.instance.stageID.ToString()));
            }
        }
    }

    public void OnClick_back() {
        PhotonNetwork.CurrentRoom.setStartWindow(false);
    }
    public void OnClick_start() {
        PhotonNetwork.CurrentRoom.setStartFlag(true);
    }
}