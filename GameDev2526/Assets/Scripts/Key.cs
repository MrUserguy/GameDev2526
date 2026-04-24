using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject door;

    private Transform followTarget;
    public bool pickedUp = false;

    public Vector3 offset = new Vector3(0.5f, 0.5f, 0); // adjust position
    public float followSpeed = 10f; // higher = snappier

    void Update()
    {
        if (pickedUp && followTarget != null)
        {
            // Smooth follow (lerp)
            Vector3 targetPos = followTarget.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();

            if (player != null)
            {
                player.hasKey = true; // give key to player
            }

            // Start following player
            pickedUp = true;
            followTarget = collision.transform;

            // Disable physics
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.simulated = false;

            // Disable collider so it doesn't interfere
            GetComponent<Collider2D>().enabled = false;
        }
    }
}