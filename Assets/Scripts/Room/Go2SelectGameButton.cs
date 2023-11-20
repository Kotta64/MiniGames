using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Go2SelectGameButton : MonoBehaviour
{
    private Text log_text;

    void Start() {
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            OnClick();
        }
    }

    public void OnClick() {
        if (PhotonNetwork.PlayerList.Length > 1) {
            SceneManager.LoadScene("SelectGameScene");
        }
    } 
}
