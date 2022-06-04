using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField]
    private KeyCode w, space, f;
    Sprite old;
    bool finishtur = true;
    int percentage;
    bool destroyed;
    bool finishshake = true;
    bool war = true;
    bool died;
    float x,y;
    public GameObject fire;
    GameObject burn;
    // Start is called before the first frame update
    void Start()
    {
        old = GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if(old != GetComponent<SpriteRenderer>().sprite)
        {
            Debug.Log("stupid bug over here!!");
            GetComponent<BoxCollider2D>().size = new Vector2(
                GetComponent<BoxCollider2D>().size.x,
                GetComponent<SpriteRenderer>().bounds.size.y
            );
            old = GetComponent<SpriteRenderer>().sprite;
        }
        if (Input.GetKeyDown(w) && finishtur)
        {
            StartCoroutine(DisasterLanching());
            percentage = Random.Range(0,101);
            if (percentage < 70 && !died)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                destroyed = true;
            }
        }

        if (Input.GetKeyDown(space) && finishshake)
        {
            StartCoroutine(shaking());
            percentage = Random.Range(0,101);
            if(percentage < 50 && !died)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                destroyed = true;
            }
        }
        if (Input.GetKeyDown(f) && war && (!(finishshake && finishtur) || !destroyed))
        {
            percentage = Random.Range(0,101);
            if(percentage < 40 && !destroyed)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                StartCoroutine(War());
            }
            

        }
    }
    void FixedUpdate()
    {
        if((destroyed || died) && transform.rotation.z < 0.50f)
        {
            GetComponent<Animator>().enabled = false;
            // GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.rotation = new Quaternion(
                transform.rotation.x,
                transform.rotation.y,
                transform.rotation.z + 0.01f,
                transform.rotation.w
            );
            if (died)
            {
                burn.transform.rotation = new Quaternion(
                    transform.rotation.x,
                    transform.rotation.y,
                    transform.rotation.z + 0.01f,
                    transform.rotation.w
                );
                burn.GetComponent<Rigidbody2D>().velocity = new Vector2(
                    GetComponent<Rigidbody2D>().velocity.x,
                    GetComponent<Rigidbody2D>().velocity.y
                );
            }
            
        }
        else if (destroyed){
            StartCoroutine(dying());
        }
        else if(died)
        {
            burn.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            StartCoroutine(kill());
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
    IEnumerator DisasterLanching()
    {
        finishtur = false;
        yield return new WaitForSeconds(5);
        finishtur = true;
    }
    IEnumerator dying()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    IEnumerator shaking()
    {
        finishshake = false;
        yield return new WaitForSeconds(7.5f);
        finishshake = true;
    }
    IEnumerator War()
    {
        war = false;
        yield return new WaitForSeconds(5.4f);

        burn = Instantiate(
            fire,
            new Vector2(
                Random.Range(
                    transform.position.x - (GetComponent<SpriteRenderer>().bounds.size.x / 2),
                    transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2)
                ),
                Random.Range(
                    transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y / 8f),
                    transform.position.y + (GetComponent<SpriteRenderer>().bounds.size.y / 2) 
                )
            ),
            Quaternion.identity
        );
        yield return new WaitForSeconds(1f);
        died = true;
        war = true;
    }
    IEnumerator kill()
    {
        yield return new WaitForSeconds(2f);
        Destroy(burn);
        Destroy(gameObject);
    }
}
