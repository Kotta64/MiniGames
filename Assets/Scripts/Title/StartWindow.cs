using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWindow : MonoBehaviourPunCallbacks
{
    public void OnClick_back() {
        PhotonNetwork.CurrentRoom.setStartWindow(false);
    }
    public void OnClick_start() {
        PhotonNetwork.CurrentRoom.setStartWindow(false);
        PhotonNetwork.CurrentRoom.setStartFlag(true);
    }
}
