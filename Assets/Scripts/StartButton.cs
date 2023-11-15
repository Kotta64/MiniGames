using System.Collections;
using System.Collections.Generic;
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
        RoomIDWindow = GameObject.Find("RoomIDWindow");
        RoomIDWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick() {
        string name = nameInput.text;
        
        if (name.Length > 0) {
            GameManager.instance.player_name = name;
            RoomIDWindow.SetActive(true);
        }
    } 
}
