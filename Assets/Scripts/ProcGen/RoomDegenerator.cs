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
        if (!progression.DisableDegeneration) {
            if (Parent == null)
            {
                Destroy(gameObject.transform.parent.gameObject);
                generationManager.AmountOfRooms++;
            }
            if (!generationManager.IsVisibleToCamera(gameObject))
            {
                if (once == false)
                {
                    {
                        randomNumber = Random.Range(1, chanceToDespawn);
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
