using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerMovement : MonoBehaviour
{
    //movement variables
    [SerializeField]private float m_Speed;
    private float m_Vertical, m_Horizontal;
    private bool m_Ladder = false;
    private bool m_IsFlying = false;
    private bool m_IsPlayer = true;
    private bool m_PlayingDead = false;

    //definitions for characters we can control
    struct Controller
    {
        public Rigidbody2D m_Rigidbody;
        public Collider2D m_Collider;
    }
    Controller m_Player;
    Controller m_Toy;


    // Start is called before the first frame update
    void Start()
    {
        //create player and toy controllers and fill with needed info
        m_Player = new Controller();
        m_Toy = new Controller();

        m_Player.m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Player.m_Collider = GetComponent<Collider2D>();

        GameObject toy = GameObject.FindWithTag("Toy");

        m_Toy.m_Rigidbody = toy.GetComponent<Rigidbody2D>();
        m_Toy.m_Collider = toy.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if player move player if toy move toy
        if (m_IsPlayer)
            Movement(m_Player);
        else
            Movement(m_Toy);

        //inputs for changing character and toy flight
        if(Input.GetButtonUp("Tab")) m_IsPlayer = !m_IsPlayer;
        if(Input.GetButtonUp("F")) m_IsFlying = !m_IsFlying;
        if(Input.GetButtonUp("C")) m_PlayingDead = !m_PlayingDead;

    }

    void Movement(Controller controller) 
    {
        if (!m_IsPlayer && m_PlayingDead) return;
        //get movement axis
        m_Horizontal = Input.GetAxis("Horizontal");
        m_Vertical = Input.GetAxis("Vertical");
        
        //apply movement to controller
        controller.m_Rigidbody.velocity = new Vector2(m_Horizontal * m_Speed, controller.m_Rigidbody.velocity.y);

        //case for player and using a ladder
        if (m_IsPlayer && m_Ladder)
            controller.m_Rigidbody.velocity = new Vector2(controller.m_Rigidbody.velocity.x, m_Vertical * m_Speed);
        
        //case for toy and flying
        if (!m_IsPlayer && m_IsFlying) 
            controller.m_Rigidbody.velocity = new Vector2(m_Horizontal * m_Speed, m_Vertical * m_Speed);

    }

    //script is ties to the "Player" gameobject so have trigger enter/exit for using ladders
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Ladder") return;
        m_Ladder = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Ladder") return;

        //if leaving top of ladder add some momentum so player doesn't bob on ladder
        if (m_Player.m_Rigidbody.velocity.y > 0.0f && m_Player.m_Rigidbody.velocity.y < 1.0f)
        {
            m_Player.m_Rigidbody.velocity = new Vector2(m_Player.m_Rigidbody.velocity.x, m_Player.m_Rigidbody.velocity.y * 5.0f);
        }
        m_Ladder = false;
    }
}
