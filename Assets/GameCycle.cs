using Game;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameCycle : MonoBehaviour
{
    public UnityEvent OnStart;
    public UnityEvent OnEnd;

    [SerializeField]
    private PlayerController control;
    private bool _paused;

    private void Start()
    {
        OnStart?.Invoke();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void TimeOut()
    {
        control.enabled = false;
        _paused = true;
        OnEnd?.Invoke();
    }

    private void Update()
    {
        if (_paused && Input.GetKeyDown(Hotkeys.BUBBLE_ACTIVATE))
            Restart();
    }
}
