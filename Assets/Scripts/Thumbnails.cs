using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thumbnails : MonoBehaviour
{
    private GameObject thumbnail_prefab;
    private GameObject Obj;
    private Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        thumbnail_prefab = Resources.Load<GameObject>("Prefabs/thumbnail");

        for (int i=0; i<GameManager.instance.game_count; i++){
            Obj = (GameObject)Instantiate (thumbnail_prefab, this.transform.position, Quaternion.identity);
            Obj.transform.parent = this.transform;
            Obj.GetComponent<RectTransform>().localPosition = new Vector3(800*i, 0, 0);
            Obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/Thumbnail/game_"+i.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        int now = (int)this.GetComponent<RectTransform>().localPosition.x;
        int purpose = -800*GameManager.instance.stageID;

        if (now > purpose){
            this.GetComponent<RectTransform>().localPosition = new Vector3(now - 100, 150, 0);
        }else if(now < purpose){
            this.GetComponent<RectTransform>().localPosition = new Vector3(now + 100, 150, 0);
        }
    }
}
