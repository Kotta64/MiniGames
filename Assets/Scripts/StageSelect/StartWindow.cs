using Photon.Pun;

public class StartWindow : MonoBehaviourPunCallbacks
{
    public void OnClick_back() {
        photonView.RPC("setStartWindow", RpcTarget.All, false);
    }
    public void OnClick_start() {
        photonView.RPC("go2gamescene", RpcTarget.All, false);
    }
}
