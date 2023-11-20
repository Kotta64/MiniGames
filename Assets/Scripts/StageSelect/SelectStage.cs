using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectStage : MonoBehaviourPunCallbacks
{
    private GameObject window;
    private GameObject leftroom;
    private const string StageKey = "StageID";
    private const string StartWindowKey = "StartWindow";
    private const string StartFlagKey = "StartFlag";

    void Start()
    {
        window = GameObject.Find("StartWindow");
        window.SetActive(false);
        leftroom = GameObject.Find("LeftRoom");
        leftroom.SetActive(false);
    }

    void Update() {
        int stage_id = PhotonNetwork.CurrentRoom.getStageID();
        if (Input.GetKeyDown(KeyCode.A) && !(window.activeSelf)){
            stage_id -= 1;
        }
        if (Input.GetKeyDown(KeyCode.D) && !(window.activeSelf)){
            stage_id += 1;
        }
        stage_id = Mathf.Clamp(stage_id, 0, GameManager.instance.game_count-1);
        if(stage_id != PhotonNetwork.CurrentRoom.getStageID()){
            PhotonNetwork.CurrentRoom.setStageID(stage_id);
        }
        if (Input.GetKeyDown(KeyCode.Return) && leftroom.activeSelf){
            SceneManager.LoadScene("TitleScene");
        }
        if (Input.GetKeyDown(KeyCode.Return) && window.activeSelf){
            photonView.RPC("go2gamescene", RpcTarget.All);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && window.activeSelf){
            photonView.RPC("setStartWindow", RpcTarget.All, false);
        }
        if (Input.GetKeyDown(KeyCode.Return) && PhotonNetwork.PlayerList.Length >= 2 && !(window.activeSelf)){
            photonView.RPC("setStartWindow", RpcTarget.All, true);
        }
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged) {
        foreach (var prop in propertiesThatChanged) {
            if (prop.Key.Equals(StageKey)) {
                int id = PhotonNetwork.CurrentRoom.getStageID();
                GameManager.instance.stageID = id;
            }
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        leftroom.SetActive(true);
    }

    [PunRPC]
    private void setStartWindow(bool state) {
        window.SetActive(state);
    }

    [PunRPC]
    private void go2gamescene() {
        SceneManager.LoadScene(String.Format("Game{0}Scene", GameManager.instance.stageID.ToString()));
    }
}
