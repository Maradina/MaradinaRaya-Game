using UnityEngine;
using System.Collections;
using TMPro; // WAJIB DITAMBAHKAN agar bisa mengontrol TextMeshPro lewat kodingan

public class lvl3 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private SpriteRenderer karakterSprite;

    [Header("Sistem Skor Level 3 (0-7)")]
    public SpriteRenderer objekAngkaUI;
    public Sprite[] kumpulanGambarAngka;
    private int jumlahSkor = 0;

    [Header("Sistem Nyawa Level 3")]
    public GameObject[] daftarObjekNyawa;
    private int sisaNyawa;

    [Header("Efek Kesakitan Level 3")]
    public float durasiKedip = 1.5f;
    private bool sedangSakit = false;

    [Header("UI Game Over dengan Efek")]
    public TextMeshProUGUI teksGameOver; // Kita langsung ambil komponen TextMeshPro-nya

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        karakterSprite = GetComponent<SpriteRenderer>();
        sisaNyawa = daftarObjekNyawa.Length;
        UpdateTampilanSkor();

        // Menyembunyikan tulisan Game Over saat game baru mulai
        if (teksGameOver != null)
        {
            teksGameOver.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (sisaNyawa <= 0)
        {
            if (rb != null) rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        float moveX = 0f;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveX = 1f;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;

        if (rb != null)
        {
            rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (sisaNyawa <= 0) return;

        if (collision.gameObject.CompareTag("Koin"))
        {
            Destroy(collision.gameObject);
            if (jumlahSkor < 7)
            {
                jumlahSkor += 1;
                UpdateTampilanSkor();
            }
        }

        if (collision.gameObject.CompareTag("Bom"))
        {
            if (!sedangSakit)
            {
                Destroy(collision.gameObject);
                KurangiNyawa();

                if (sisaNyawa > 0)
                {
                    StartCoroutine(EfekKenaBom());
                }
            }
        }
    }

    void UpdateTampilanSkor()
    {
        if (objekAngkaUI != null && kumpulanGambarAngka.Length > jumlahSkor)
        {
            objekAngkaUI.sprite = kumpulanGambarAngka[jumlahSkor];
        }
    }

    void KurangiNyawa()
    {
        if (sisaNyawa > 0)
        {
            sisaNyawa -= 1;
            if (daftarObjekNyawa[sisaNyawa] != null)
            {
                daftarObjekNyawa[sisaNyawa].SetActive(false);
            }

            if (sisaNyawa <= 0)
            {
                Debug.Log("Game Over! Nyawa Habis.");

                // PANGGIL EFEK ANIMASI GAME OVER DI SINI
                if (teksGameOver != null)
                {
                    StartCoroutine(EfekGameOverMuncul());
                }
            }
        }
    }

    IEnumerator EfekKenaBom()
    {
        sedangSakit = true;
        float waktuBerjalan = 0f;

        while (waktuBerjalan < durasiKedip)
        {
            karakterSprite.color = Color.red;
            yield return new WaitForSeconds(0.15f);
            karakterSprite.color = Color.white;
            yield return new WaitForSeconds(0.15f);
            waktuBerjalan += 0.3f;
        }

        karakterSprite.color = Color.white;
        sedangSakit = false;
    }

    // FUNGSI KORUTIN BARU: Efek Animasi Game Over (Fade In + Scale Up)
    IEnumerator EfekGameOverMuncul()
    {
        teksGameOver.gameObject.SetActive(true);

        // 1. Set text awal jadi transparan total & ukurannya agak kecil
        Color warnaAsal = teksGameOver.color;
        teksGameOver.color = new Color(warnaAsal.r, warnaAsal.g, warnaAsal.b, 0f);
        teksGameOver.transform.localScale = new Vector3(0.5f, 0.5f, 1f);

        float durasiEfek = 1.0f; // Efek berjalan selama 1 detik
        float waktu = 0f;

        while (waktu < durasiEfek)
        {
            waktu += Time.deltaTime;
            float persentase = waktu / durasiEfek;

            // Efek Fade-In (Mengubah Alpha/Transparansi ke 1)
            teksGameOver.color = new Color(warnaAsal.r, warnaAsal.g, warnaAsal.b, persentase);

            // Efek Scale-Up (Membesar perlahan sampai ukuran normal 1.0)
            float scale = Mathf.Lerp(0.5f, 1.0f, persentase);
            teksGameOver.transform.localScale = new Vector3(scale, scale, 1f);

            yield return null; // Tunggu ke frame berikutnya
        }

        // Pastikan di akhir animasi, nilainya benar-benar sempurna
        teksGameOver.color = new Color(warnaAsal.r, warnaAsal.g, warnaAsal.b, 1f);
        teksGameOver.transform.localScale = Vector3.one;
    }
}