using Photon.Pun;

public class StartWindow : MonoBehaviourPunCallbacks
{
    public void OnClick_back() {
        PhotonNetwork.CurrentRoom.setStartWindow(false);
    }
    public void OnClick_start() {
        PhotonNetwork.CurrentRoom.setStartWindow(false);
        PhotonNetwork.CurrentRoom.setStartFlag(2);
    }
}
