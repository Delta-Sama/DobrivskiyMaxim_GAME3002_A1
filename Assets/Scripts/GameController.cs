using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_vBallStartPosition = Vector3.zero;

    [SerializeField]
    private GameObject m_pBallPrefab = null;

    private Vector3 m_vTargetPos = Vector3.zero;
    private GameObject m_TargetDisplay = null;

    // Variables
    private float d_move = 0.015f;
    private bool m_enabled = true;
    [SerializeField]
    private float gravity = 9.8f;

    // Start is called before the first frame update
    void Start()
    {
        CreateTargetDisplay();
        
    }

    private void CreateTargetDisplay()
    {
        m_TargetDisplay = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        m_TargetDisplay.transform.position = Vector3.zero;
        m_TargetDisplay.transform.localScale = new Vector3(1.0f, 0.05f, 1.0f);
        m_TargetDisplay.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        m_TargetDisplay.GetComponent<Renderer>().material.color = Color.red;
        m_TargetDisplay.GetComponent<Collider>().enabled = false;
    }

    IEnumerator SetCooldown()
    {
        m_enabled = false;
        yield return new WaitForSeconds(0.75f);
        m_enabled = true;
    }

    private void LaunchBall()
    {
        if (!m_enabled) return;

        GameObject Ball = Instantiate(m_pBallPrefab, m_vBallStartPosition, Quaternion.identity);

        Destroy(Ball, 2.0f);

        Vector3 initialVel = Vector3.zero;
        initialVel.y = Mathf.Sqrt(2 * gravity * (m_vTargetPos.y - m_vBallStartPosition.y));// sqrt(2Hg)
        float t = initialVel.y / gravity;
        initialVel.x = (m_vTargetPos.x - m_vBallStartPosition.x) / t;
        initialVel.z = (m_vTargetPos.z - m_vBallStartPosition.z) / t;

        Ball.GetComponent<Rigidbody>().velocity = initialVel;

        StartCoroutine(SetCooldown());
    }
    private void HandleUserInput()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            LaunchBall();
        }

        if (Input.GetKey(KeyCode.W))
        {
            m_vTargetPos.y += d_move;
        }

        if (Input.GetKey(KeyCode.S))
        {
            m_vTargetPos.y -= d_move;
        }

        if (Input.GetKey(KeyCode.A))
        {
            m_vTargetPos.x -= d_move;
        }

        if (Input.GetKey(KeyCode.D))
        {
            m_vTargetPos.x += d_move;
        }

        if (m_vTargetPos.y < 1) m_vTargetPos.y = 1;
    }
    // Update is called once per frame
    void Update()
    {
        HandleUserInput();

        m_TargetDisplay.transform.position = m_vTargetPos;
    }
}
