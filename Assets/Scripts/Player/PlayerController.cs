using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public float moveSpeed;

    public Animator Myanim;

    public static PlayerController instance;
    public bool canMove = true;

    public string areaTransitionName;
    private Vector3 bottomLimit;
    private Vector3 topLimit;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }

        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        }
        else
        {
            playerRB.velocity = Vector2.zero;
        }

        Myanim.SetFloat("moveX", playerRB.velocity.x);
        Myanim.SetFloat("moveY", playerRB.velocity.y);

        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            if (canMove)
            {
                Myanim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                Myanim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }            
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLimit.x, topLimit.x), Mathf.Clamp(transform.position.y, bottomLimit.y, topLimit.y), transform.position.z);
    }

    public void SetBounds(Vector3 bootleft, Vector3 topRig)
    {
        this.bottomLimit = bootleft + new Vector3(.5f, .5f, 0f);
        this.topLimit = topRig + new Vector3(-.5f, -.5f, 0f);
    }
}
