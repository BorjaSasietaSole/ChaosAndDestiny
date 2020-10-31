using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public float moveSpeed;

    public Animator Myanim;

    public static PlayerController instance;

    public string areaTransitionName;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;

        Myanim.SetFloat("moveX", playerRB.velocity.x);
        Myanim.SetFloat("moveY", playerRB.velocity.y);

        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            Myanim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            Myanim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }
    }
}
