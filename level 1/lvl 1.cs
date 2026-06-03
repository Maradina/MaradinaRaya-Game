using UnityEngine;

public class lvl1 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private SpriteRenderer karakterSprite;

    // Variabel untuk mengontrol animasi karakter
    private Animator anim;

    [Header("Sistem Skor (0-4)")]
    public SpriteRenderer objekAngkaUI;
    public Sprite[] kumpulanGambarAngka;

    private int jumlahSkor = 0;
    
    // Variabel baru untuk menyimpan arah input dari Update ke FixedUpdate
    private float moveX = 0f;
    private bool inginLompat = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        karakterSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // EFEK ANTI-GETER 1: Memaksa interpolasi fisik aktif lewat kode
        if (rb != null)
        {
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }

        UpdateTampilanSkor();
    }

    // Update digunakan KHUSUS untuk membaca deteksi tombol dari keyboard agar responsif
    void Update()
    {
        moveX = 0f;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveX = 1f;
            if (karakterSprite != null) karakterSprite.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1f;
            if (karakterSprite != null) karakterSprite.flipX = true;
        }

        // Membaca tombol lompat (Space)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inginLompat = true;
        }

        // Logika pengaktif efek animasi hidup
        if (anim != null)
        {
            if (moveX != 0f)
            {
                anim.SetBool("isJalan", true);
            }
            else
            {
                anim.SetBool("isJalan", false);
            }
        }
    }

    // EFEK ANTI-GETER 2: Semua pergerakan Rigidbody2D WAJIB ditaruh di FixedUpdate
    void FixedUpdate()
    {
        if (rb != null)
        {
            // Jalankan karakter secara konstan dan halus di sini
            rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

            // Eksekusi lompat jika tombol space ditekan di Update
            if (inginLompat)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                inginLompat = false; // Reset tombol lompat kembali
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Koin"))
        {
            Destroy(collision.gameObject);

            if (jumlahSkor < 4)
            {
                jumlahSkor += 1;
                UpdateTampilanSkor();
            }

            Debug.Log("Koin Berhasil Diambil! Skor: " + jumlahSkor);
        }
    }

    void UpdateTampilanSkor()
    {
        if (objekAngkaUI != null && kumpulanGambarAngka.Length > jumlahSkor)
        {
            objekAngkaUI.sprite = kumpulanGambarAngka[jumlahSkor];
        }
    }
}