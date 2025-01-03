using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour
{
    [SerializeField] private Sprite cursorSprite; // Sprite yang akan digunakan sebagai cursor
    private Vector2 cursorHotspot;

    void Start()
    {
        // Mengonversi sprite menjadi Texture2D
        Texture2D cursorTexture = SpriteToTexture2D(cursorSprite);

        // Menentukan hotspot pada tengah sprite
        cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);

        // Mengubah cursor
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }

    // Fungsi untuk mengonversi Sprite menjadi Texture2D
    Texture2D SpriteToTexture2D(Sprite sprite)
    {
        Rect rect = sprite.textureRect;
        Texture2D texture = new Texture2D((int)rect.width, (int)rect.height);
        texture.SetPixels(sprite.texture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height));
        texture.Apply();
        return texture;
    }
}
