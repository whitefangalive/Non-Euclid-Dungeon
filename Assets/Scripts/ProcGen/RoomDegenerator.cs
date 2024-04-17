using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDegenerator : MonoBehaviour
{
    private GenerationManager generationManager;
    public GameObject Parent;

    private bool once = false;

    public int chanceToDespawn;
    public int randomNumber;
    public bool visible;

    private ProgressionScript progression;
    // Start is called before the first frame update
    void Start()
    {
        generationManager = GameObject.Find("GenerationManager").GetComponent<GenerationManager>();
        chanceToDespawn = generationManager.ChanceToDespawn;
        progression = GameObject.Find("ProgressionManager").GetComponent<ProgressionScript>();
    }

    // Update is called once per frame
    void Update()
    {
        visible = generationManager.IsVisibleToCamera(gameObject);
        if (!progression.DisableDegeneration) {
            if (Parent == null && (!visible || generationManager.playerPastGenerationDistance(transform.position)))
            {
                Destroy(gameObject.transform.parent.gameObject);
                generationManager.AmountOfRooms++;
            }
            if (!visible)
            {
                if (once == false)
                {
                    {
                        randomNumber = Random.Range(1, chanceToDespawn);
                        if (generationManager.playerPastGenerationDistance(transform.position)) 
                        {
                            randomNumber = Random.Range(1, chanceToDespawn / 5);
                        }
                        once = true;
                        if (randomNumber == 1 && Parent != transform.parent.gameObject)
                        {
                            DungeonGenerator dungGen = Parent.GetComponent<DungeonGenerator>();
                            dungGen.ResetAllWalls();
                            dungGen.currentPlacedNodes--;
                            generationManager.AmountOfRooms++;
                            Destroy(gameObject.transform.parent.gameObject);
                        }
                    }
                }
            }
            else
            {
                once = false;
            }
        }
    }
}
