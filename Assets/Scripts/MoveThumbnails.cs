using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveThumbnails : MonoBehaviour
{
    private GameObject cursor;
    private const int speed = 100;
    private bool flag = true;
    void FixedUpdate() {
        int now = (int)this.GetComponent<RectTransform>().localPosition.x;
        int purpose = -1000*GameManager.instance.stageID;

        if (now != purpose){
            this.GetComponent<RectTransform>().localPosition = new Vector3(now + Mathf.Clamp(purpose-now, -1, 1)*speed, 150, 0);
            if(!flag){
                foreach (Transform child in transform){
                    child.gameObject.GetComponent<RectTransform>().localScale = new Vector2(1.0f, 1.0f);
                }
                flag = true;
            }
        }else if(flag){
            cursor = transform.Find("thumbnail_" + GameManager.instance.stageID.ToString()).gameObject;
            cursor.GetComponent<RectTransform>().localScale = new Vector2(1.3f, 1.3f);
            flag = false;
        }
    }
}
