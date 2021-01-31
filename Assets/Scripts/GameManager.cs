using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GGJ21 m_PlayerControls;

    private int totalReturnWins;

    [Tooltip("How many items have been dropped off so far")]
    public int m_CurrentDropoffCount = 0;

    private void Awake()
    {
        m_PlayerControls = new GGJ21();
        m_PlayerControls.Player.Cancel.canceled += ctx => SceneManager.LoadScene(0);
        m_PlayerControls.Player.DebugWin.canceled += ctx => SceneManager.LoadScene(3);
    }
    void Start()
    {
        totalReturnWins = GameObject.FindGameObjectsWithTag("Item").Length;
        Debug.Log(string.Format("You need {0} dropoffs for the win", totalReturnWins));
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CurrentDropoffCount >= totalReturnWins)
        {
            // Go to victory screen
            SceneManager.LoadScene(3);
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
