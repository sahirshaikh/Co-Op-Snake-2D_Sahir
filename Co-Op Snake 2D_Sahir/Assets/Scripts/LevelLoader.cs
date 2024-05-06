using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{

    [SerializeField] private string LevelName;
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.onclick);
        SceneManager.LoadScene(LevelName);
    }

}
