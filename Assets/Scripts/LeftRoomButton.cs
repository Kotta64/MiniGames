using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeftRoomButton : MonoBehaviour
{
    public void OnClick() {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("TitleScene");
    }
}
