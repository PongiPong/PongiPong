using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10f;
    public float boundaryY = 4.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float move = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            move = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            move = -1f;
        }
        Vector3 pos = transform.position;
        pos.y += move * speed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, -boundaryY, boundaryY);
        transform.position = pos;
    }
}
