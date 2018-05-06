﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paintbrush : MonoBehaviour
{
    public int resolution = 512;
	public RenderTexture rendt;
	public RawImage rawImage;
    Texture2D whiteMap;
    public float brushSize;
    public Texture2D brushTexture;
    Vector2 stored;
    private float riHalfHeight, riHalfWidth;
    public static Dictionary<Collider, RenderTexture> paintTextures = new Dictionary<Collider, RenderTexture>();
    void Start()
    {
        CreateClearTexture();// clear white texture to draw on
        riHalfHeight = rawImage.rectTransform.sizeDelta.y/2;
        riHalfWidth = rawImage.rectTransform.sizeDelta.x/2;
    }

    void Update()
    {
        //both at bottom left
		if (Input.GetMouseButton(0)) {
			float xCoordinate = Mathf.Floor(Input.mousePosition.x - rawImage.rectTransform.position.x + riHalfWidth);
			float yCoordinate = Mathf.Floor(Input.mousePosition.y - rawImage.rectTransform.position.y + riHalfHeight);
			
			DrawTexture(rendt, xCoordinate, yCoordinate);
		}
		/*
        Debug.DrawRay(transform.position, Vector3.down * 20f, Color.magenta);
        RaycastHit hit;
        //if (Physics.Raycast(transform.position, Vector3.down, out hit))
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) // delete previous and uncomment for mouse painting
        {
            Collider coll = hit.collider;
            if (coll != null)
            {
                if (!paintTextures.ContainsKey(coll)) // if there is already paint on the material, add to that
                {
                    Renderer rend = hit.transform.GetComponent<Renderer>();
                    paintTextures.Add(coll, getWhiteRT());
                    rend.material.SetTexture("_PaintMap", paintTextures[coll]);
                }
                if (stored != hit.lightmapCoord) // stop drawing on the same point
                {
                    stored = hit.lightmapCoord;
                    Vector2 pixelUV = hit.lightmapCoord;
                    pixelUV.y *= resolution;
                    pixelUV.x *= resolution;
                    DrawTexture(paintTextures[coll], pixelUV.x, pixelUV.y);
                }
            }
        }*/
    }

    void DrawTexture(RenderTexture rt, float posX, float posY)
    {

        RenderTexture.active = rt; // activate rendertexture for drawtexture;
        GL.PushMatrix();                       // save matrixes
        GL.LoadPixelMatrix(0, resolution, resolution, 0);      // setup matrix for correct size

        // draw brushtexture
        Graphics.DrawTexture(new Rect(posX - brushTexture.width / brushSize, (rt.height - posY) - brushTexture.height / brushSize, brushTexture.width / (brushSize * 0.5f), brushTexture.height / (brushSize * 0.5f)), brushTexture);
        GL.PopMatrix();
        RenderTexture.active = null;// turn off rendertexture


    }

    RenderTexture getWhiteRT()
    {
        RenderTexture rt = new RenderTexture(resolution, resolution, 32);
        Graphics.Blit(whiteMap, rt);
        return rt;
    }

    void CreateClearTexture()
    {
        whiteMap = new Texture2D(1, 1);
        whiteMap.SetPixel(1, 1, Color.white);
        whiteMap.Apply();
    }
}