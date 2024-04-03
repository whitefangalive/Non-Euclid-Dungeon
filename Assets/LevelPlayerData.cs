using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPlayerData : MonoBehaviour
{
    // Start is called before the first frame update
    public int FarClipPlane = 300;
    public CameraClearFlags flags;
    public Color BackGroundColor;
    void Start()
    {
        Camera.main.farClipPlane = FarClipPlane;
        Camera.main.clearFlags = flags;
        if (Camera.main.clearFlags == CameraClearFlags.SolidColor) 
        {
            Camera.main.backgroundColor = BackGroundColor;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
