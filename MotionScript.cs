using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionScript : MonoBehaviour
{

    public Joystick joystick;

    public Rigidbody2D rb;
    
    void FixedUpdate()
    {
        float y = joystick.Vertical; //rotation
        float x = joystick.Horizontal;

        
        if (x > 0 && y > 0)
            rb.rotation = (float)(-Mathf.Atan(x / y) * (180 / 3.1416));
        else if (x > 0 && y < 0)
            rb.rotation = (float)(-Mathf.Atan(Mathf.Abs(y) / x) * (180 / 3.1416)) - 90;
        else if (x < 0 && y > 0)
            rb.rotation = (float)(Mathf.Atan(Mathf.Abs(x) / y) * (180 / 3.1416));
        else if (x < 0 && y < 0)
            rb.rotation = (float)(Mathf.Atan(Mathf.Abs(y) / Mathf.Abs(x)) * (180 / 3.1416)) + 90;
        

    
        int speed = 200;        
        var move = new Vector2(joystick.Horizontal, joystick.Vertical);
        rb.AddForce(move * speed);
        //physics options, above: space physics, below, ground physics

        /*var move = new Vector3(joystick.Horizontal, joystick.Vertical, 0);
        transform.position += move * speed * Time.unscaledDeltaTime;*/
    }

}