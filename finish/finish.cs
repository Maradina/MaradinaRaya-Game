using UnityEngine;
using UnityEngine.SceneManagement; // WAJIB ADA untuk mengatur pindah level/scene

public class FinishPoint : MonoBehaviour
{
    // Nama scene/level berikutnya yang ingin dituju (bisa diubah lewat Inspector)
    public string namaLevelBerikutnya = "SampleScene";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mengecek apakah yang menyentuh finish poin adalah karakter player
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.name.Contains("karakter"))
        {
            Debug.Log("Hore! Level Selesai. Pindah ke: " + namaLevelBerikutnya);

            // Perintah untuk memuat level berikutnya
            SceneManager.LoadScene(namaLevelBerikutnya);
        }
    }
}