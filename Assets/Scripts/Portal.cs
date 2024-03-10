using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject Destination;
    [HideInInspector]
    public bool AbleToTeleport = true;
    private Vector3 posDiff;
    private Quaternion rotDiff;
    private Vector3 scaleDiff;
    // Start is called before the first frame update
    void Start()
    {
        Transform objF = gameObject.transform;
        Transform objD = Destination.transform;
        posDiff = objD.position - objF.position;
        rotDiff = objD.rotation * Quaternion.Inverse(objF.rotation);
        scaleDiff = objD.localScale - objF.localScale;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "HeadCollider" && AbleToTeleport) 
        {
            Destination.GetComponent<Portal>().AbleToTeleport = false;

            Transform player = other.transform.parent.parent;
            if (other.transform.parent.parent.name == "FallbackObjects") 
            {
                player = other.transform.parent.parent.parent.parent;
            }

            player.rotation *= rotDiff;
            player.localScale += scaleDiff;
            player.position += posDiff;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        AbleToTeleport = true;
    }
}
