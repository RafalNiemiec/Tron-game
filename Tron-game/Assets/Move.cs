using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Movment keys
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rightKey;
    public KeyCode leftKey;

    // Movment speed
    public float speed = 16;

    // Wall prefab
    public GameObject wallPrefab;

    // Current wall
    Collider2D wall;

    // Last Wall's end
    Vector2 lastWallEnd;

    void spawnWall() {
        // Save last wall's position
        lastWallEnd = transform.position;

        // Spawn a new Lightwall
        GameObject g = Instantiate(wallPrefab, transform.position, Quaternion.identity);
        wall = g.GetComponent<Collider2D>();
    }

    void fitColliderBetween(Collider2D co, Vector2 a, Vector2 b) {
        // Calculate the Center Position
        co.transform.position = a + (b - a) * 0.5f;

        // Scale it
        float dist = Vector2.Distance(a, b);
        if (a.x != b.x)
            co.transform.localScale = new Vector2(dist + 1, 1);
        else
            co.transform.localScale = new Vector2(1, dist + 1);
    }
    
    void OnTriggerEnter2D(Collider2D co) {
        // not the current wall?
        if (co != wall) {
            print("Player lost: " + name);
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // initial velocity
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        spawnWall();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(upKey)) {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
            spawnWall();
        }

        else if (Input.GetKeyDown(downKey)) {
            GetComponent<Rigidbody2D>().velocity = -Vector2.up * speed;
            spawnWall();
        }

        else if (Input.GetKeyDown(rightKey)) {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
            spawnWall();
        }

        else if (Input.GetKeyDown(leftKey)) {
            GetComponent<Rigidbody2D>().velocity = -Vector2.right * speed;
            spawnWall();
        }

        fitColliderBetween(wall, lastWallEnd, transform.position);
        
    }
}
