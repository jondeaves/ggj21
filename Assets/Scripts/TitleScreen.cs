using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public AudioSource m_Keanu;

    private GGJ21 m_PlayerControls;

    private float m_VideoTime = 5;
    private float m_VideoTimer = 0;
    private bool m_PlayingVideo = true;

    private void Awake()
    {
        m_PlayerControls = new GGJ21();
        m_PlayerControls.Player.Fire.canceled += ctx => {
            if (m_PlayingVideo == false)
            {
                SceneManager.LoadScene(1);

                Color col = GameObject.FindGameObjectWithTag("Panel").GetComponent<RawImage>().color;
                col = new Color(col.r, col.g, col.b, 1);

                GameObject.FindGameObjectWithTag("Panel").GetComponent<RawImage>().color = col;
            }
        };

        m_PlayingVideo = true;
    }

    private void Update()
    {
        if (m_PlayingVideo)
        {
            m_VideoTimer += Time.deltaTime;

            if (m_VideoTimer >= m_VideoTime)
            {
                m_PlayingVideo = false;

                if (m_Keanu != null)
                {
                    m_Keanu.Play();
                }
            }
        }
        else if (GameObject.FindGameObjectWithTag("Panel") != null)
        {
            Color col = GameObject.FindGameObjectWithTag("Panel").GetComponent<RawImage>().color;
            col = new Color(col.r, col.g, col.b, Mathf.MoveTowards(col.a, 0, Time.deltaTime));

            GameObject.FindGameObjectWithTag("Panel").GetComponent<RawImage>().color = col;

            //GameObject.FindGameObjectWithTag("Panel").SetActive(false);
        }
    }

    private void OnEnable()
    {
        m_PlayerControls.Player.Enable();
    }

    private void OnDisable()
    {
        m_PlayerControls.Player.Disable();
    }
}
