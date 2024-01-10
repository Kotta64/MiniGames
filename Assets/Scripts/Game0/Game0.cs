using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Game0 : MonoBehaviourPunCallbacks
{
    private int player_num;
    private int[] scores = {0, 0};
    private GameObject player;
    private GameObject leftroom;
    private Text p1_score;
    private Text p2_score;

    private PhotonView pV;
    private const float move_power = 100;
    public static Game0 instance;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        leftroom = GameObject.Find("LeftRoom");
        leftroom.SetActive(false);
        p1_score = GameObject.Find("player1_point").GetComponent<Text>();
        p2_score = GameObject.Find("player2_point").GetComponent<Text>();
        var players = PhotonNetwork.PlayerList;

        photonView.RPC("setScore", RpcTarget.All, 0);

        if(players[0].NickName == GameManager.instance.player_name){
            player_num = 1;
            player = PhotonNetwork.Instantiate("Prefabs/Player1", new Vector3(0, 5, 40), Quaternion.Euler(0f, 180f, 0f));
            if(Random.Range(0, 2) == 1){
                PhotonNetwork.Instantiate("Prefabs/Ball", new Vector3(0, 25, -10), Quaternion.identity);
            }else{
                PhotonNetwork.Instantiate("Prefabs/Ball", new Vector3(0, 25, 10), Quaternion.identity);
            }
            photonView.RPC("getpV", RpcTarget.All);
        }else{
            player_num = 2;
            player = PhotonNetwork.Instantiate("Prefabs/Player2", new Vector3(0, 5, -40), Quaternion.identity);
            Camera.main.gameObject.transform.parent.gameObject.transform.rotation = Quaternion.Euler(180f, 0f, 180f);
        }
    }

    void FixedUpdate() {
        if(player_num == PhotonNetwork.CurrentRoom.getTurn() && pV != null){
            if(Input.GetKey(KeyCode.W)){
                pV.RPC("controllBall", RpcTarget.All, 0, player_num);
            }
            if(Input.GetKey(KeyCode.S)){
                pV.RPC("controllBall", RpcTarget.All, 1, player_num);
            }
            if(Input.GetKey(KeyCode.A)){
                pV.RPC("controllBall", RpcTarget.All, 2, player_num);
            }
            if(Input.GetKey(KeyCode.D)){
                pV.RPC("controllBall", RpcTarget.All, 3, player_num);
            }
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        PhotonNetwork.LeaveRoom();
        leftroom.SetActive(true);
    }

    public void next(int player){
        photonView.RPC("setScore", RpcTarget.All, player);
    }

    [PunRPC]
    private void getpV(){
        pV = GameObject.Find("Ball(Clone)").GetComponent<PhotonView>();
    }

    [PunRPC]
    private void setScore(int score){
        var players = PhotonNetwork.PlayerList;

        if(score != 0){
            scores[score-1] += 1;
        }

        p1_score.text =  players[0].NickName + " : " + scores[0].ToString();
        p2_score.text =  scores[1].ToString() + " : " + players[1].NickName;
    }
}
