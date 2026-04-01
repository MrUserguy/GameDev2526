using UnityEngine;

public class TempPlatform : MonoBehaviour
{

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            createPlatform();
        }
    }
    void createPlatform()
    {
        Vector2 spawnLocation = (Vector2)transform.position + new Vector2(0,0);

    }
}
