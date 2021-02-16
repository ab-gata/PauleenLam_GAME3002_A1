using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIFunctions : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_InitVelText = null;
    [SerializeField]
    private TextMeshProUGUI m_ScoreText = null;
    [SerializeField]
    private TextMeshProUGUI m_PointsText = null;
    [SerializeField]
    private TextMeshProUGUI m_AngleText = null;


    // UI TEXT FUNCTIONS------------------------------------------------------------------------------------
    // Callback to update the interface
    public void OnRequestUpdateUI(float fInitVel, float fPoints, float fScore, float fAngle)
    {
        UpdateParams(fInitVel, fPoints, fScore, fAngle);
    }

    // Update the interface internally
    private void UpdateParams(float fInitVel, float fPoints, float fScore, float fAngle)
    {
        m_InitVelText.text = "Initial Velocity = " + fInitVel + " m/s^2";
        m_PointsText.text = "Score = " + fPoints;
        m_ScoreText.text = "Score = " + fScore;
        m_AngleText.text = "Angle = " + fAngle + " deg";
    }


    // BUTTON FUNCTIONS-------------------------------------------------------------------------------------
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
