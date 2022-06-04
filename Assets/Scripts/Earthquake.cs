using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Earthquake : MonoBehaviour
{
    [SerializeField]
    private KeyCode space;
    public GameObject night;
    public GameObject clause;
    public Image D;
    public Spawning script;
    bool day = true;
    bool finish ;
    bool b,bb;
    int i;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
        void Update()
    {
        if (!bb){
            b = true;
            StartCoroutine(Delay());
            bb = true;
        }
        if (!b)
        {
            StartCoroutine(clock());
            b = true;
        }
        if (Input.GetKeyDown(space))
        {
            if (!finish)
            {       
                StartCoroutine(Shake(3f, 0.5f));
                finish = true;
            }
            D.color = new Color(1f, 1f, 1f, 1f);
            StartCoroutine(script.delayKey(D));

        }
        
    }
    IEnumerator clock()
    {
        yield return new WaitForSeconds(1);
        b = false;
        time();
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(10);
        b = false;
    }
    void time()
    {
        if (i < 20 && day)
        {
            i++;
            GetComponent<Camera>().backgroundColor= new Color(
                GetComponent<Camera>().backgroundColor.r,
                GetComponent<Camera>().backgroundColor.g - 0.03255f,
                GetComponent<Camera>().backgroundColor.b - 0.05f,    
                GetComponent<Camera>().backgroundColor.a
            );
            night.GetComponent<SpriteRenderer>().color = new Color(
                night.GetComponent<SpriteRenderer>().color.r,
                night.GetComponent<SpriteRenderer>().color.g,
                night.GetComponent<SpriteRenderer>().color.b,
                night.GetComponent<SpriteRenderer>().color.a + 0.05f    
            );
            if (i == 20)
            {
                bb = false;
            }
        }
        else if(i > 0){
            day = false;
            i--;
            GetComponent<Camera>().backgroundColor= new Color(
                GetComponent<Camera>().backgroundColor.r,
                GetComponent<Camera>().backgroundColor.g + 0.03255f,
                GetComponent<Camera>().backgroundColor.b + 0.05f,    
                GetComponent<Camera>().backgroundColor.a
            );
            night.GetComponent<SpriteRenderer>().color = new Color(
                night.GetComponent<SpriteRenderer>().color.r,
                night.GetComponent<SpriteRenderer>().color.g,
                night.GetComponent<SpriteRenderer>().color.b,   
                night.GetComponent<SpriteRenderer>().color.a - 0.05f    
            );
            if (i == 0)
            {
                bb = false;
            }
        }
        else{
            day = true;
        }
    }
    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orignalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            clause.GetComponent<SpriteRenderer>().color = new Color(
                clause.GetComponent<SpriteRenderer>().color.r,
                clause.GetComponent<SpriteRenderer>().color.b,
                clause.GetComponent<SpriteRenderer>().color.g,
                clause.GetComponent<SpriteRenderer>().color.a + 0.004f
            );
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(x, y, -10f);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transform.position = orignalPosition;
        yield return new WaitForSeconds(1.5f);
        while (elapsed > 0)
        {
            clause.GetComponent<SpriteRenderer>().color = new Color(
                clause.GetComponent<SpriteRenderer>().color.r,
                clause.GetComponent<SpriteRenderer>().color.b,
                clause.GetComponent<SpriteRenderer>().color.g,
                clause.GetComponent<SpriteRenderer>().color.a - 0.004f
            );

            elapsed -= Time.deltaTime;
            yield return 0;
        }

        finish = false;
    }
}
