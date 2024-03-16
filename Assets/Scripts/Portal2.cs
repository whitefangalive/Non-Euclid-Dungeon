using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal2 : MonoBehaviour
{
    public GameObject Destination;
    [HideInInspector]
    public bool AbleToTeleport = true;
    private Vector3 playerDiff;
    private Quaternion rotDiff;
    private Vector3 scaleDiff;
    // Start is called before the first frame update
    void Start()
    {
        Transform objF = gameObject.transform;
        Transform objD = Destination.transform;
        rotDiff = objD.rotation * Quaternion.Inverse(objF.rotation);
        scaleDiff = objD.localScale - objF.localScale;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "HeadCollider" && AbleToTeleport) 
        {
            Destination.GetComponent<Portal2>().AbleToTeleport = false;

            Transform player = other.transform.parent.parent;
            if (other.transform.parent.parent.name == "FallbackObjects") 
            {
                player = other.transform.parent.parent.parent.parent;
            }
            playerDiff = player.position - gameObject.transform.position;

            playerDiff = RotateVector(playerDiff, rotDiff);

            player.rotation *= rotDiff;
            player.localScale += scaleDiff;
            player.position = Destination.transform.position + playerDiff;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        AbleToTeleport = true;
    }
    Vector3 RotateVector(Vector3 vector, Quaternion rotation)
    {
        return rotation * vector;
    }
}
