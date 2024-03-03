using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    // This method is called when the sword collides with another collider marked as a trigger
    void OnTriggerEnter(Collider other)
    {
        // Log the name of the collided game object
        Debug.Log("Sword collided with: " + other.name);
        
        // Perform actions specific to the collision, if needed
        // You can add more logic here based on the collided object
    }
}
