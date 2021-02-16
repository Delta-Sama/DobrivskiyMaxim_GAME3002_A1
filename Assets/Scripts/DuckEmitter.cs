using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class DuckEmitter : MonoBehaviour
{
    [SerializeField]
    private Text m_scoreText;
    [SerializeField]
    private GameObject m_duckPrefab;
    [SerializeField]
    private GameObject m_coinPrefab;
    [SerializeField]
    private GameObject m_audioSource;
    [SerializeField]
    private AudioClip m_coinClip;
    [SerializeField]
    private AudioClip m_failClip;

    private int m_timer = 0;
    [SerializeField]
    private int m_framesPerDuck = 45;

    private int score = 0;

    private bool launched = false;

    IEnumerator LaunchEmitter()
    {
        yield return new WaitForSeconds(2.5f);
        launched = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LaunchEmitter());
    }

    IEnumerator GivePoints(GameObject arcadeObject, int points)
    {
        yield return new WaitForSeconds(4.1f);
        if (!arcadeObject.GetComponent<ArcadeObjectController>().m_bDead)
        {
            print("Not dead");
            score += points;
            score = Mathf.Max(0, score);
            if (points > 0)
            {
                m_audioSource.GetComponent<AudioSource>().clip = m_coinClip;
                m_audioSource.GetComponent<AudioSource>().Play();
            }
            else
            {
                m_audioSource.GetComponent<AudioSource>().clip = m_failClip;
                m_audioSource.GetComponent<AudioSource>().Play();
            }
        }
    }

    private void SpawnDuckOrCoin()
    {
        if (Random.Range(1, 5) == 1)
        {
            GameObject Coin = Instantiate(m_coinPrefab, transform.position + new Vector3(0.0f,1.0f,0.0f),
                Quaternion.Euler(-90, 0, 0));

            Destroy(Coin, 4.2f);

            Coin.GetComponent<Rigidbody>().velocity = new Vector3(6.0f, 0.0f, 0.0f);

            StartCoroutine(GivePoints(Coin, 1));
        }
        else
        {
            GameObject Duck = Instantiate(m_duckPrefab, transform.position, Quaternion.Euler(-90, 90, 0));

            Destroy(Duck, 4.2f);

            Duck.GetComponent<Rigidbody>().velocity = new Vector3(6.0f, 0.0f, 0.0f);

            StartCoroutine(GivePoints(Duck, -2));
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_scoreText.text = score.ToString();

        if (launched && m_timer-- <= 0)
        {
            SpawnDuckOrCoin();
            m_timer = m_framesPerDuck;
        }
    }
}
