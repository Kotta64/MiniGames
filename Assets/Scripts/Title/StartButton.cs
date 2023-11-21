using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    InputField nameInput;
    GameObject RoomIDWindow;
    // Start is called before the first frame update
    void Start()
    {
        nameInput = GameObject.Find("PlayerNameInput").GetComponent<InputField>();
        nameInput.ActivateInputField();
        RoomIDWindow = GameObject.Find("RoomIDWindow");
        RoomIDWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            OnClick();
        }
    }

    public void OnClick() {
        string name = nameInput.text;
        
        if (name.Length > 0 && name.Length < 15) {
            GameManager.instance.player_name = name;
            RoomIDWindow.SetActive(true);
        }
    } 
}
