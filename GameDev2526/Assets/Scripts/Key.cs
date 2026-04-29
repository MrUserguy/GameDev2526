using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject door;

    private Transform followTarget;
    public bool pickedUp = false;
    [SerializeField] public Sprite newsprite;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            spriteRenderer.sprite = newsprite;
            Destroy(door);
            

        }
    }
}