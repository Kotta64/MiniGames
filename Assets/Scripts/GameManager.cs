using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public string player_name;
    public string roomID;
    public int stageID;
    public int game_count = 3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        
    }
}
