using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Spawning : MonoBehaviour
{
    [SerializeField]
    private KeyCode q,w,d, f;
    public GameObject s1, s2;
    public GameObject ground;
    private Transform humanPosition;
    public GameObject Human;
    public int place;
    public GameObject turnado;
    public GameObject explosion;
    public Image Q,S,F;
    bool finishtur = true, war;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(q))
        {
            randomplace();
            Instantiate(
                Human,
                new Vector2(
                    humanPosition.position.x,
                    humanPosition.position.y
                ),
                Quaternion.identity
            ).name = "Human";
            Q.color = new Color(1f,1f,1f, 1f);
            StartCoroutine(delayKey(Q));
            
        }
        if (Input.GetKeyDown(w))
        {
            S.color = new Color(1f,1f,1f, 1f);
            StartCoroutine(delayKey(S));
            if (finishtur)
            {
                randomplace();
                Instantiate(
                    turnado,
                    new Vector2(
                        humanPosition.position.x,
                        humanPosition.position.y
                    ),
                    Quaternion.identity
                ).name = "turnado";
                finishtur = false;
                StartCoroutine(DisasterLanching());
            }
        }
        if (Input.GetKeyDown(f))
        {
            if (!war)
            {
                war = true;
                StartCoroutine(War(0.05f));
            }
            F.color = new Color(1f,1f,1f, 1f);
            StartCoroutine(delayKey(F));
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if( col.gameObject.name == "Ground")
        {
        }
    }
    public IEnumerator delayKey(Image key)
    {
        yield return new WaitForSeconds(0.25f);
        key.color = new Color(
            1f,1f,1f,
            0.2f
        );
    }
    void randomplace()
    {
        place = Random.Range(1,3);
        switch (place)
        {
            case 1:
                humanPosition = s1.transform; break;
            case 2:
                humanPosition = s2.transform; break;
        }
    }
    IEnumerator DisasterLanching()
    {
        yield return new WaitForSeconds(5);
        finishtur = true;
    }
    IEnumerator War(float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-7f, 7f);
            float y = Random.Range(-4f, 4f);
            GameObject exp = Instantiate(
                explosion,
                new Vector2(x,y),
                Quaternion.identity
            );
            elapsed += Time.deltaTime;
            yield return new WaitForSeconds(0.6f);
            Destroy(exp);

        }
        war = false;
    }
}
