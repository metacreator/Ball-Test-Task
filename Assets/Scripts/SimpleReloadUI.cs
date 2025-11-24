using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SimpleReloadUI_TMP : MonoBehaviour
{
    [SerializeField] private Game game;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private float reloadDelay = 2f;

    private bool displayed;

    private void Update()
    {
        if (displayed) return;

        if (game.LevelFailed)
        {
            Show("LEVEL FAILED: " + GetFailReason(), Color.red);
        }
        else if (game.LevelCompleted)
        {
            Show("LEVEL COMPLETE!", Color.green);
        }
    }

    private string GetFailReason()
    {
        return game.LastFailReason;
    }

    private void Show(string msg, Color col)
    {
        if (messageText == null) return;
        displayed = true;

        messageText.text = msg;
        messageText.color = col;
        messageText.gameObject.SetActive(true);

        Invoke(nameof(Reload), reloadDelay);
    }

    private void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}