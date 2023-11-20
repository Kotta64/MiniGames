using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeftRoomButton : MonoBehaviour
{
    public void OnClick() {
        PhotonNetwork.CurrentRoom.setStartFlag(0);
        PhotonNetwork.CurrentRoom.setStartWindow(false);
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("TitleScene");
    }
}
