using System.Collections;
using UnityEngine;

public class lvl2 : MonoBehaviour
{
    [Header("Sistem Pergerakan Fisika")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    private float inputHorizontal;

    private Rigidbody2D rb;
    private bool diAtasTanah = true;

    [Header("Sistem Skor (0-4)")]
    public SpriteRenderer objekAngkaUI;
    public Sprite[] kumpulanGambarAngka;
    private int skorSekarang = 0; // Menghitung jumlah koin yang diambil

    [Header("Sistem Nyawa")]
    public GameObject[] daftarObjekNyawa;
    private int indeksNyawaSekarang;

    [Header("Efek Terluka (Warna Merah)")]
    public float durasiEfekMerah = 0.2f;
    public SpriteRenderer karakterSprite;
    private bool sedangSakit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (karakterSprite == null)
        {
            Transform anakKarakter = transform.Find("karakter_0");
            if (anakKarakter != null)
            {
                karakterSprite = anakKarakter.GetComponent<SpriteRenderer>();
            }
        }

        indeksNyawaSekarang = daftarObjekNyawa.Length;
    }

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        if (inputHorizontal > 0 && karakterSprite != null)
        {
            karakterSprite.flipX = false;
        }
        else if (inputHorizontal < 0 && karakterSprite != null)
        {
            karakterSprite.flipX = true;
        }

        if (Input.GetButtonDown("Jump") && diAtasTanah)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            diAtasTanah = false;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(inputHorizontal * moveSpeed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("tangga") || collision.gameObject.name.Contains("pipa") || collision.gameObject.CompareTag("Ground"))
        {
            diAtasTanah = true;
        }

        // --- DETEKSI BOM (JIKA BOM ADALAH BENDA PADAT) ---
        if (collision.gameObject.CompareTag("Bom") && !sedangSakit)
        {
            KenaBom();
            Destroy(collision.gameObject); // <--- INI BIAR BOMNYA HANCUR/HILANG SETELAH DITABRAK
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // --- DETEKSI BOM (JIKA BOM DICENTANG IS TRIGGER) ---
        if (collision.CompareTag("Bom") && !sedangSakit)
        {
            KenaBom();
            Destroy(collision.gameObject); // <--- INI BIAR BOMNYA HANCUR/HILANG SETELAH DITABRAK
        }

        // --- DETEKSI KOIN (KOIN WAJIB DICENTANG IS TRIGGER) ---
        if (collision.CompareTag("Koin"))
        {
            AmbilKoin(collision.gameObject);
        }
    }

    void AmbilKoin(GameObject koin)
    {
        Debug.Log("Koin Diambil!");
        Destroy(koin); // <--- INI BIAR KOINNYA HANCUR/HILANG DARI LAYAR

        // Tambah skor dan update gambar angka UI jika ada
        skorSekarang++;
        if (objekAngkaUI != null && kumpulanGambarAngka != null && skorSekarang < kumpulanGambarAngka.Length)
        {
            objekAngkaUI.sprite = kumpulanGambarAngka[skorSekarang];
        }
    }

    void KenaBom()
    {
        Debug.Log("Karakter Terluka!");

        if (karakterSprite != null)
        {
            StartCoroutine(EfekSakit());
        }

        if (indeksNyawaSekarang > 0)
        {
            indeksNyawaSekarang--;
            if (daftarObjekNyawa[indeksNyawaSekarang] != null)
            {
                daftarObjekNyawa[indeksNyawaSekarang].SetActive(false);
            }
        }

        if (indeksNyawaSekarang <= 0)
        {
            Debug.Log("Game Over!");
            if (karakterSprite != null) karakterSprite.gameObject.SetActive(false);
        }
    }

    IEnumerator EfekSakit()
    {
        sedangSakit = true;
        karakterSprite.color = Color.red;
        yield return new WaitForSeconds(durasiEfekMerah);
        if (karakterSprite != null)
        {
            karakterSprite.color = Color.white;
        }
        sedangSakit = false;
    }
}