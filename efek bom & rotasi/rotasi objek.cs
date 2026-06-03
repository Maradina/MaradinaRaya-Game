using UnityEngine;

public class RotasiObjek : MonoBehaviour
{
    // Sekarang kita pakai angka puluhan biar gampang ngaturnya
    // Coba mulai dari 50, kalau kurang lambat tinggal turunin ke 20 atau 10
    public float kecepatanPutar = 50f;

    private Vector3 skalaAwal;
    private float sudutDrajat = 0f;

    void Start()
    {
        skalaAwal = transform.localScale;
    }

    void Update()
    {
        // Menambah sudut putaran secara stabil berdasarkan waktu nyata (detik)
        sudutDrajat += kecepatanPutar * Time.deltaTime;

        // Mengubah sudut derajat menjadi efek matematika Sinus
        float efekMuter = Mathf.Sin(sudutDrajat * Mathf.Deg2Rad);

        // Terapkan ke sumbu X
        transform.localScale = new Vector3(skalaAwal.x * efekMuter, skalaAwal.y, skalaAwal.z);
    }
}