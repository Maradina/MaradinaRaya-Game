using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;

    private void Awake()
    {
        // 1. Kunci PlayerPrefs HARUS SAMA PERSIS dengan yang ada di FinishPoint.cs
        int unlockedlevel = PlayerPrefs.GetInt("levelUnlocked", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for (int i = 0; i < unlockedlevel; i++)
        {
            // Cek agar tidak error jika jumlah level yang terbuka melebihi jumlah tombol
            if (i < buttons.Length)
            {
                buttons[i].interactable = true;
            }
        }
    }

    public void OpenLevel(int levelId)
    {
        
        string levelName = "lvl " + levelId;
        SceneManager.LoadScene(levelName);
    }
}