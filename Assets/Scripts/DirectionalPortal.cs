using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalPortal : MonoBehaviour
{
    public GameObject Destination;
    [HideInInspector]
    public bool AbleToTeleport = true;
    private Vector3 playerDiff;
    private Quaternion rotDiff;
    private Vector3 scaleDiff;
    [Range(-360, 360)]
    public float NeededEulerRotationYMin = 0;
    [Range(-360, 360)]
    public float NeededEulerRotationYMax = 0;
    public float rotation;
    // Start is called before the first frame update
    void Start()
    {
        Transform objF = gameObject.transform;
        Transform objD = Destination.transform;
        rotDiff = objD.rotation * Quaternion.Inverse(objF.rotation);
        scaleDiff = Divide(objD.localScale, objF.localScale);
        NeededEulerRotationYMin = NeededEulerRotationYMin + transform.root.rotation.eulerAngles.y;
        NeededEulerRotationYMax = NeededEulerRotationYMax + transform.root.rotation.eulerAngles.y;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "HeadCollider" && AbleToTeleport) 
        {
            Transform player = other.transform.root;
            Transform playerRotation = other.transform.parent.parent;
            rotation = playerRotation.rotation.eulerAngles.y;

            float playerAngle = playerRotation.rotation.eulerAngles.y;
            float minAngle = NeededEulerRotationYMin;
            float maxAngle = NeededEulerRotationYMax;

            // Adjust angles to be in the range [0, 360)
            playerAngle = (playerAngle + 360) % 360;
            minAngle = (minAngle + 360) % 360;
            maxAngle = (maxAngle + 360) % 360;

            // Check if playerAngle is between minAngle and maxAngle
            if ((minAngle <= maxAngle && playerAngle >= minAngle && playerAngle <= maxAngle) ||
                (minAngle > maxAngle && (playerAngle >= minAngle || playerAngle <= maxAngle)))
            {
                Destination.GetComponent<DirectionalPortal>().AbleToTeleport = false;
                playerDiff = player.position - gameObject.transform.position;

                playerDiff = RotateVector(playerDiff, rotDiff);


                player.rotation *= rotDiff;
                player.localScale = Multiply(player.localScale, scaleDiff);
                player.position = Destination.transform.position + playerDiff;
                
            }
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
    public static Vector3 Divide(Vector3 v1, Vector3 v2)
    {
        if (v2.x == 0f || v2.y == 0f || v2.z == 0f)
        {
            Debug.LogError("Division by zero detected. Returning Vector3.zero.");
            return Vector3.zero;
        }

        return new Vector3(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);
    }
    public static Vector3 Multiply(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
    }
}