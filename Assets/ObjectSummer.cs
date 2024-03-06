using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSummer : MonoBehaviour
{
    private GenerationManager generationManager;
    public GameObject[] possibleNodes;

    // Start is called before the first frame update
    void Start()
    {
        generationManager = GameObject.Find("GenerationManager").GetComponent<GenerationManager>();

        int randomNode = Random.Range(0, possibleNodes.Length);
        if (generationManager.CanPlaceRoom(gameObject, possibleNodes[randomNode], QueryTriggerInteraction.Ignore))
        {
            GameObject summoned = Instantiate(possibleNodes[randomNode], transform.position, Quaternion.Euler(0, 0, 0), gameObject.transform);
        }
    }
}
