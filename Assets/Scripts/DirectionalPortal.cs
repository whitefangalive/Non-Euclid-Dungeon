using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalPortal : MonoBehaviour
{
    public GameObject Destination;
    [HideInInspector]
    public bool AbleToTeleport = true;
    private Vector3 posDiff;
    private Quaternion rotDiff;
    private Vector3 scaleDiff;
    [Range(-360, 360)]
    public float NeededEulerRotationYMin = 0;
    [Range(-360, 360)]
    public float NeededEulerRotationYMax = 0;
    private Quaternion realRotation;
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
            Transform player = other.transform.parent.parent;
            Transform playerRotation = other.transform.parent;
            if (other.transform.parent.parent.name == "FallbackObjects")
            {
                player = other.transform.parent.parent.parent.parent;
                playerRotation = other.transform.parent.parent;
            }
            realRotation = player.transform.rotation * playerRotation.rotation;
            if (realRotation.eulerAngles.y > NeededEulerRotationYMin && (realRotation.eulerAngles.y < NeededEulerRotationYMax)) 
            {
                Destination.GetComponent<DirectionalPortal>().AbleToTeleport = false;
                player.rotation *= rotDiff;
                player.localScale += scaleDiff;
                player.position += posDiff;
            }
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        AbleToTeleport = true;
    }
}
