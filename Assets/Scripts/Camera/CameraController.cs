using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Tilemap map;
    private Vector3 bottomLimit;
    private Vector3 topLimit;

    private float halfHeight;
    private float halfWidwth;

    public int musicToPlay;
    private bool musicStarted;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;

        halfHeight = Camera.main.orthographicSize;
        halfWidwth = halfHeight * Camera.main.aspect;

        bottomLimit = map.localBounds.min + new Vector3(halfWidwth, halfHeight, 0f);
        topLimit = map.localBounds.max + new Vector3(-halfWidwth, -halfHeight, 0f);

        PlayerController.instance.SetBounds(map.localBounds.min, map.localBounds.max);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        //keep the camera inside the bounds
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLimit.x, topLimit.x), Mathf.Clamp(transform.position.y, bottomLimit.y, topLimit.y), transform.position.z);

        if (!musicStarted)
        {
            musicStarted = true;
            AudioManager.instance.PlayBGM(musicToPlay);
        }
    }
}
