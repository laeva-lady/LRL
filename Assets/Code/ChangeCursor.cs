using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{

    [SerializeField] Texture2D tex_cursor_default;
    [SerializeField] Texture2D tex_cursor_abilities;

    [SerializeField] Vector2 hotspot = Vector2.zero;
    [SerializeField] CursorMode cursorMode = CursorMode.Auto;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(tex_cursor_default, hotspot, cursorMode);
    }

    public void SetDefault()
    {
        Cursor.SetCursor(tex_cursor_default, hotspot, cursorMode);
    }
    public void SetAbilities()
    {
        Cursor.SetCursor(tex_cursor_default, hotspot, cursorMode);
    }
}
