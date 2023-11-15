using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomIDButton : MonoBehaviour
{
    InputField roomIDInput;
    // Start is called before the first frame update
    void Start()
    {
        roomIDInput = GameObject.Find("RoomIDInput").GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick() {
        string id = roomIDInput.text;
        
        if (id.Length > 0) {
            GameManager.instance.roomID = id;
            SceneManager.LoadScene("SelectGameScene");
        }
    }
}
