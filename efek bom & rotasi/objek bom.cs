using UnityEngine;

public class GerakNaikTurun : MonoBehaviour
{
    [Header("Pengaturan Gerakan Fisika")]
    // Kecepatan gerak (Coba mulai dari angka 1 atau 2, karena pakai sistem fisika ini sudah cukup lambat)
    public float kecepatan = 1.5f;

    // Seberapa jauh bom bergerak dari posisi awal sebelum balik arah
    public float jarakJangkauan = 1.2f;

    private Rigidbody2D rb;
    private float posisiAwalY;
    private int arah = 1; // 1 berarti naik, -1 berarti turun

    void Start()
    {
        // Mengambil komponen Rigidbody 2D dari si bom
        rb = GetComponent<Rigidbody2D>();
        posisiAwalY = transform.position.y;

        // Pastikan settingan Rigidbody-nya benar agar tidak jatuh karena gravitasi
        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void Update()
    {
        // Cek batas atas
        if (transform.position.y >= posisiAwalY + jarakJangkauan)
        {
            arah = -1;
        }
        // Cek batas bawah
        else if (transform.position.y <= posisiAwalY - jarakJangkauan)
        {
            arah = 1;
        }

        // Gerakan tanpa fisika Rigidbody (Tinggal geser posisi transform-nya)
        transform.Translate(Vector2.up * arah * kecepatan * Time.deltaTime);
    }
}