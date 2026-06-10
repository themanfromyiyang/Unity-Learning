using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//[System.Serializable]
//public class EventVector3 : UnityEvent<Vector3> { }

public class Mousemanager : MonoBehaviour
{
    public static Mousemanager instance;
    public Texture2D point, doorway, attack, target, arrow;
    RaycastHit hitInfo;
    public event Action<Vector3> OnMouseClicked;
    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
    }   

    void Update()
    {
        SetCursorTexture();
        MouseControl();
    }
    void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo))
        {
            switch (hitInfo.collider.gameObject.tag)
            {
                case "Ground":
                    Cursor.SetCursor(target, new Vector2(16,16), CursorMode.Auto);
                    break;
                case "Doorway":
                    Cursor.SetCursor(doorway, new Vector2(16,16), CursorMode.Auto);
                    break;
                case "Enemy":
                    Cursor.SetCursor(attack, new Vector2(16,16), CursorMode.Auto);
                    break;
                case "Target":
                    Cursor.SetCursor(target, new Vector2(16,16), CursorMode.Auto);
                    break;
                default:
                    Cursor.SetCursor(arrow, new Vector2(16,16), CursorMode.Auto);
                    break;
            }
        }
    }
    void MouseControl()
    {
        if (Input.GetMouseButtonDown(0) && hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.tag == "Ground")
            {
                OnMouseClicked?.Invoke(hitInfo.point);
            }
        }
    }
}
