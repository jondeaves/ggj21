using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    private GGJ21 m_PlayerControls;

    private void Awake()
    {
        m_PlayerControls = new GGJ21();
        m_PlayerControls.Player.Fire.canceled += ctx => SceneManager.LoadScene(0);
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
