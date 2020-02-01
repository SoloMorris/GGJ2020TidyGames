using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationTarget : MonoBehaviour
{

    Vector2 input;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("HorizontalRed");
        input.y = Input.GetAxisRaw("VerticalRed");
        transform.localPosition = input;
    }
}
