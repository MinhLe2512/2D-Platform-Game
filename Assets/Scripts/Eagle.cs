using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : Enemy
{
    private Collider2D coll;
    private Rigidbody2D rb;
    [SerializeField]private Transform[] routes;
    private int routeToGo = 0;

    private float tValue = 0;
    private Vector2 eaglePos;
    private float speedModifier = 0.5f;
    private bool coroutineAllowed = true;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }
  
    // Update is called once per frame
    private void Update()
    {
        if (coroutineAllowed)
            StartCoroutine(GoByRoute(routeToGo));
    }

    private IEnumerator GoByRoute(int num)
    {
        coroutineAllowed = false;

        Vector2 p0 = routes[num].GetChild(0).position;
        Vector2 p1 = routes[num].GetChild(1).position;
        Vector2 p2 = routes[num].GetChild(2).position;
        Vector2 p3 = routes[num].GetChild(3).position;

        while (tValue < 1)
        {
            
            tValue += Time.deltaTime * speedModifier;
            if (facingLeft)
            {
                eaglePos = Mathf.Pow(1 - tValue, 3) * p3 +
                3 * Mathf.Pow(1 - tValue, 2) * tValue * p2 +
                3 * (1 - tValue) * Mathf.Pow(tValue, 2) * p1 +
                Mathf.Pow(tValue, 3) * p0;
            }
            else
            {
                eaglePos = Mathf.Pow(1 - tValue, 3) * p0 +
                3 * Mathf.Pow(1 - tValue, 2) * tValue * p1 +
                3 * (1 - tValue) * Mathf.Pow(tValue, 2) * p2 +
                Mathf.Pow(tValue, 3) * p3;
            }
            transform.position = eaglePos;

            yield return new WaitForEndOfFrame();
        }

        if (facingLeft)
        {
            facingLeft = false;
            transform.localScale = new Vector3(-1, 1);
        }
        else
        {
            facingLeft = true;
            transform.localScale = new Vector3(1, 1);
        }

        yield return new WaitForSeconds(2);
        tValue = 0f;

        if (routeToGo < routes.Length - 1)
            routeToGo += 1;

        coroutineAllowed = true;
    }
}
