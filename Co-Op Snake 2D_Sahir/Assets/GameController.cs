using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject PauseUI;
    public GameObject GameoverUI;
    public GameObject LobbyUI;
    public GameObject StartUpUI;
    public SnakeController snakeController;

    void Update()
        {
            if(Input.GetKey(KeyCode.Escape))
            {
                snakeController.PauseSnake();

                PauseUI.SetActive(true);
                
            }
        }

        public void QuitGame()
        {
            SoundManager.Instance.Play(SoundManager.Sounds.onclick);
            Debug.Log("Application Quit");
            Application.Quit();

        }

        public void ReloadGame()
        {
            SoundManager.Instance.Play(SoundManager.Sounds.onclick);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ResumeGame()
        {
            snakeController.ResumeSnake();
            SoundManager.Instance.Play(SoundManager.Sounds.onclick);
            PauseUI.SetActive(false);

        }

        public void LobbyLoading()
        {
            SoundManager.Instance.Play(SoundManager.Sounds.onclick);
            SceneManager.LoadScene(0);
        }
        public void PlayButton()
        {
            SoundManager.Instance.Play(SoundManager.Sounds.onclick);
            LobbyUI.SetActive(true);
            StartUpUI.SetActive(false);

        }

}
