# KreditAPI - REST API Pengajuan Kredit

## Deskripsi Proyek
KreditAPI adalah REST API sederhana untuk mengelola pengajuan kredit menggunakan **.NET Core v8.0** dan **Entity Framework** dengan **PostgreSQL 16.3**. API ini mendukung CRUD, perhitungan angsuran, JWT Authentication, validasi input, unit testing, error handling, logging, dan konsep performa & scalability.



## Soal 1 - Database & Query Optimization

### Tabel `Kredit`
| Kolom       | Tipe Data        | Keterangan                   |
|------------ |-----------------|------------------------------|
| id         | UUID (PK)       | Primary Key unik             |
| plafon     | numeric         | Jumlah pinjaman              |
| bunga      | decimal(5,2)    | Bunga dalam persen (%)       |
| tenor      | integer         | Lama cicilan dalam bulan     |
| angsuran   | numeric         | Angsuran per bulan           |
| created_at | timestamp       | Waktu pembuatan data         |
| updated_at | timestamp       | Waktu terakhir update        |

### Indexing
```sql
CREATE INDEX idx_kredit_plafon ON Kredit(plafon);
CREATE INDEX idx_kredit_tenor ON Kredit(tenor);
```

### SQL Query
#### Cari pengajuan kredit dengan tenor paling panjang dan plafon tertinggi
```sql
SELECT * FROM Kredit
ORDER BY tenor DESC, plafon DESC
LIMIT 1;
```
#### Menghitung rata-rata bunga
```sql
SELECT AVG(bunga) AS rata_rata_bunga FROM Kredit;
```sql
test
