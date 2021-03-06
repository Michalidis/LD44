﻿using DG.Tweening;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    Texture2D blk;

    private bool fade;
    public float alph;

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blk);
    }

    void Start()
    {
        GetComponent<Camera>().DOOrthoSize(1.0f, 2.0f);

        fade = false;
        alph = 1;
        blk = new Texture2D(1, 1);
        blk.SetPixel(0, 0, new Color(0, 0, 0, alph));
        blk.Apply();
    }

    void Update()
    {
        if (!fade)
        {
            if (alph > 0)
            {
                alph -= Time.deltaTime * .4f;
                if (alph < 0) { alph = 0f; }
                blk.SetPixel(0, 0, new Color(0, 0, 0, alph));
                blk.Apply();
            }
        }
        if (fade)
        {
            if (alph < 1)
            {
                alph += Time.deltaTime * .4f;
                if (alph > 1) { alph = 1f; }
                blk.SetPixel(0, 0, new Color(0, 0, 0, alph));
                blk.Apply();
            }
        }
    }
}
