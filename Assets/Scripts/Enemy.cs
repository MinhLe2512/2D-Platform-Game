using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    [SerializeField] protected float leftWaypoint;
    [SerializeField] protected float rightWaypoint;
    protected bool facingLeft = true;
    // Start is called before the first frame update

    public void Defeat()
    {
        anim.SetTrigger("Death");
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Explode()
    {
        Destroy(this.gameObject);
    }
}
