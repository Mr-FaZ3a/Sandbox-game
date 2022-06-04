using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : MonoBehaviour
{
    float go;
    float aspectx = 8f;
    bool b = true;
    bool doit;
    Rigidbody2D body;
    Sprite old; 
    public GameObject tree;
    float initialx;
    // Start is called before the first frame update
    void Start()
    {
        initialx = transform.position.x;
        body = GetComponent<Rigidbody2D>();
        go = Random.Range(-aspectx, aspectx);
        old = tree.GetComponent<SpriteRenderer>().sprite;
    }
    void Awake()
    {
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!doit)
        {
            if (gameObject.name == "Spawner1" || gameObject.name == "Spawner2")
            {
                b = false;
            }
            else{
                b = true;
                doit = true;
            }
        }
        if (col.gameObject.name != "Ground")
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (b)
        {
        
            if (transform.position.x < (go - 0.1f) )
            {
                body.velocity = new Vector2(Random.Range(1f,3f),
                    body.velocity.y);
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (transform.position.x > (go + 0.1f)){
                body.velocity = new Vector2(-(Random.Range(1f,3f)),
                    body.velocity.y);
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else{
                body.velocity = Vector2.zero;
                GetComponent<Animator>().enabled = false;
                b = false;
                planting();
            }
        }
        else if (!b) {
            GetComponent<Animator>().enabled = true;
            if (transform.position.x < (initialx - 0.1f))
            {
                body.velocity = new Vector2(Random.Range(1f, 3f),0);
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if(transform.position.x > (initialx + 0.1f))
            {
                body.velocity = new Vector2(-(Random.Range(1f,3f)),0);
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else{
                body.velocity = Vector2.zero;
                GetComponent<Animator>().enabled = false;
                Destroy(gameObject);
            }
        }
    }
    void planting()
    {  
        Instantiate(
            tree,
            transform.position,
            Quaternion.identity
        ).name = "tree";
    }

}
