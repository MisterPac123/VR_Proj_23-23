using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBoard : MonoBehaviour
{
    public Texture2D texture;
    public Vector2 textureSize;
    private Texture2D originalTexture;

    void Start()
    {
        Renderer r = transform.GetComponent<Renderer>();
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        r.material.mainTexture = texture;
        originalTexture = texture;
    }
}
