using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisiblityTest : MonoBehaviour
{
    public bool visible;
    private GenerationManager generationManager;
    // Start is called before the first frame update
    void Start()
    {
        generationManager = GameObject.Find("GenerationManager").GetComponent<GenerationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        visible = generationManager.IsVisibleToCamera(gameObject);
    }
}
