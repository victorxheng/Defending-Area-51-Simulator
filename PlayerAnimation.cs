using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    public Joystick joystick;
    public Animator player;
	void Update () {
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            player.SetBool("joystickOn", true);
        else
            player.SetBool("joystickOn", false);
	}
}
