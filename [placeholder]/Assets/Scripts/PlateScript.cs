using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLateScript : MonoBehaviour
{
    [SerializeField] private Obstacle[] m_AttachedObjs;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach(Obstacle objs in m_AttachedObjs) 
        {
            objs.activate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (Obstacle objs in m_AttachedObjs)
        {
            objs.deactivate();
        }
    }
}
