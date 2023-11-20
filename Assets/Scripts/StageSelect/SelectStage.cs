using Photon.Pun;
using ExitGames.Client.Photon;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectStage : MonoBehaviourPunCallbacks
{
    private GameObject window;
    private const string StageKey = "StageID";
    private const string StartWindowKey = "StartWindow";
    private const string StartFlagKey = "StartFlag";

    void Start()
    {
        window = GameObject.Find("StartWindow");
        window.SetActive(false);
    }

    void Update() {
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
        if (Input.GetKeyDown(KeyCode.Return) && window.activeSelf){
            PhotonNetwork.CurrentRoom.setStartWindow(false);
            PhotonNetwork.CurrentRoom.setStartFlag(2);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && window.activeSelf){
            PhotonNetwork.CurrentRoom.setStartWindow(false);
        }
        if (Input.GetKeyDown(KeyCode.Return) && PhotonNetwork.PlayerList.Length >= 2 && !(window.activeSelf)){
            PhotonNetwork.CurrentRoom.setStartWindow(true);
        }
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged) {
        foreach (var prop in propertiesThatChanged) {
            if (prop.Key.Equals(StageKey)) {
                int id = PhotonNetwork.CurrentRoom.getStageID();
                GameManager.instance.stageID = id;
            }
            if (prop.Key.Equals(StartWindowKey)) {
                window.SetActive(PhotonNetwork.CurrentRoom.getStartWindow());
            }
            if (prop.Key.Equals(StartFlagKey) && PhotonNetwork.CurrentRoom.getStartFlag() == 2) {
                SceneManager.LoadScene(String.Format("Game{0}Scene", GameManager.instance.stageID.ToString()));
            }
        }
    }
}
