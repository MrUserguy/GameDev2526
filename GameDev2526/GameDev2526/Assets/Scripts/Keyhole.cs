using UnityEngine;

public class Keyhole : MonoBehaviour
{
    public GameObject door;
    public GameObject key;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();

            if (player != null && player.hasKey)
            {
                Destroy(door);      // open door
                Destroy(key);
                player.hasKey = false; // consume key
            }
            
        }
    }

}
