using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    [SerializeField]
    private float m_fPushSpeed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Duck" || collision.transform.tag == "Coin")
        {
            if (!collision.gameObject.GetComponent<ArcadeObjectController>().m_bDead)
            {
                collision.gameObject.GetComponent<ArcadeObjectController>().m_bDead = true;
                collision.gameObject.GetComponent<Rigidbody>().useGravity = true;
                collision.gameObject.GetComponent<AudioSource>().Play();
                collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, m_fPushSpeed);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
