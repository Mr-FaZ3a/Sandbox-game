using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnado : MonoBehaviour
{
    float go;
    public bool b;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(running());
        if (!b)
        {
            if (transform.position.x > (go + 0.1f))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-(Random.Range(3f,7f)),0);
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if(transform.position.x < (go - 0.1f))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(3f, 7f),0);
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else{
                go = Random.Range(-8f, 8f);
            }
        }
        else{
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name != "Ground")
        {
            Physics2D.IgnoreCollision(
                col.gameObject.GetComponent<Collider2D>(),
                GetComponent<Collider2D>()
            );
        }
    }
    IEnumerator running()
    {
        yield return new WaitForSeconds(5);
        b = true;
    }
}
