using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToggle : Obstacle
{
    public override void activate()
    {
        Vector3 oldpos = gameObject.GetComponent<Transform>().position;
        gameObject.GetComponent<Transform>().position = new Vector3(oldpos.x, oldpos.y - 500, oldpos.z);
    }

    public override void deactivate()
    {
        Vector3 oldpos = gameObject.GetComponent<Transform>().position;
        gameObject.GetComponent<Transform>().position = new Vector3(oldpos.x, oldpos.y + 500, oldpos.z);
    }
}
