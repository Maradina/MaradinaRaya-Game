using UnityEngine;
using UnityEngine.UI;

public class LevelMenuManager : MonoBehaviour
{
    [Header("Masukkan Gambar Bintang/Centang tiap level:")]
    public GameObject[] tandaSelesaiLevel;

    [Header("Nama Scene Level (Huruf besar/kecil harus persis):")]
    public string[] namaSceneLevel = { "lvl 1", "lvl 2", "lvl 3"};

    void Start()
    {
        UpdateTampilanLevel();
    }

    void UpdateTampilanLevel()
    {
        for (int i = 0; i < tandaSelesaiLevel.Length; i++)
        {
            // Cek buku catetan Unity, apa level ini udah dapet nilai 1 (tamat)?
            if (PlayerPrefs.GetInt(namaSceneLevel[i] + "_Selesai", 0) == 1)
            {
                // Kalau udah, munculkan tanda bintang/centangnya!
                tandaSelesaiLevel[i].SetActive(true);
            }
            else
            {
                // Kalau belum, tetap sembunyikan
                tandaSelesaiLevel[i].SetActive(false);
            }
        }
    }

    void Update()
    {
        // Fitur Rahasia: Tekan tombol R di keyboard buat NGERESET semua data level!
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            UpdateTampilanLevel();
            Debug.Log("DATA LEVEL DIRESET!");
        }
    }
}