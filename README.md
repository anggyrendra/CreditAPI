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
```

## Soal 2 - API CRUD dengan Authentication & Validation
Endpoints Method	URL	Deskripsi
```endpoint
- POST	/api/kredit	Create pengajuan kredit
- GET	/api/kredit	List semua data
- GET	/api/kredit/{id}	Read pengajuan kredit By ID
- PUT	/api/kredit/{id}	Update pengajuan kredit
- DELETE	/api/kredit/{id}	Delete pengajuan kredit
```

### JWT Authentication
```
- Hanya user terautentikasi yang dapat mengakses API.
- Endpoint login menghasilkan token JWT.
```
### Validasi Input
```
Plafon, bunga, dan tenor > 0
Bunga valid: 0 ≤ bunga ≤ 100
Implementasi: [Range] dan ModelState.IsValid di controller
```
### Simulasi
JWT Authentication
Endpoint login menghasilkan token JWT yang digunakan untuk autentikasi di endpoint lain.
```
endpoint login:
POST /api/auth/login
Request:
{
  "username": "admin",
  "password": "admin123"
}
Response:
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
Token ini harus ditambahkan di header Authorization setiap request CRUD:
Authorization: Bearer <token>
```
CRUD Endpoint
```
Create Pengajuan Kredit
POST /api/kredit
Request:
{
  "plafon": 50000000,
  "bunga": 12,
  "tenor": 24
}
Response:
{
  "id": "c1b9e8f0-8f8d-4e56-8c2b-5f3f9d3bfb12",
  "plafon": 50000000,
  "bunga": 12,
  "tenor": 24,
  "angsuran": 2355000,
  "created_at": "2025-08-17T12:00:00Z",
  "updated_at": "2025-08-17T12:00:00Z"
}
```
```
Read Pengajuan Kredit
GET /api/kredit (List semua data)
[
  {
    "id": "c1b9e8f0-8f8d-4e56-8c2b-5f3f9d3bfb12",
    "plafon": 50000000,
    "bunga": 12,
    "tenor": 24,
    "angsuran": 2355000
  }
]
```
```
GET /api/kredit/{id} (By ID)
{
  "id": "c1b9e8f0-8f8d-4e56-8c2b-5f3f9d3bfb12",
  "plafon": 50000000,
  "bunga": 12,
  "tenor": 24,
  "angsuran": 2355000
}
```
```
Update Pengajuan Kredit
PUT /api/kredit/{id}
Request:
{
  "plafon": 60000000,
  "bunga": 10,
  "tenor": 36
}
Response:
{
  "id": "c1b9e8f0-8f8d-4e56-8c2b-5f3f9d3bfb12",
  "plafon": 60000000,
  "bunga": 10,
  "tenor": 36,
  "angsuran": 1970000,
  "updated_at": "2025-08-17T12:30:00Z"
}
```
```
Delete Pengajuan Kredit
DELETE /api/kredit/{id}
Response:

{
  "message": "Pengajuan kredit berhasil dihapus"
}
```
Validasi Input
```
Plafon, bunga, tenor tidak boleh ≤ 0
Bunga harus 0 ≤ bunga ≤ 100
Contoh error response (input invalid):
{
  "error": "Plafon harus lebih dari 0, bunga 0-100%, tenor lebih dari 0"
}
```
Simulasi CURL (menggunakan token JWT)
```
Create Pengajuan Kredit
curl -X POST "http://localhost:5072/api/kredit" \
-H "Authorization: Bearer <TOKEN>" \
-H "Content-Type: application/json" \
-d '{"plafon":50000000,"bunga":12,"tenor":24}'
```
```
Get List Kredit
curl -X GET "http://localhost:5072/api/kredit" \
-H "Authorization: Bearer <TOKEN>"
```
```
Update Kredit
curl -X PUT "http://localhost:5072/api/kredit/c1b9e8f0-8f8d-4e56-8c2b-5f3f9d3bfb12" \
-H "Authorization: Bearer <TOKEN>" \
-H "Content-Type: application/json" \
-d '{"plafon":60000000,"bunga":10,"tenor":36}'
```
```
Delete Kredit
curl -X DELETE "http://localhost:5072/api/kredit/c1b9e8f0-8f8d-4e56-8c2b-5f3f9d3bfb12" \
-H "Authorization: Bearer <TOKEN>"
```

### Soal 3 - API Perhitungan Angsuran
```
Endpoint
Method	URL	Deskripsi

POST	/api/kredit/calculate	Hitung angsuran per bulan


Parameter Request

{
  "plafon": 100000000,
  "bunga": 12,
  "tenor": 60
}

Rumus Perhitungan Angsuran (Anuitas)

decimal monthlyRate = bunga / 100 / 12;
decimal angsuran = plafon * monthlyRate / (1 - (decimal)Math.Pow((double)(1 + monthlyRate), -tenor));

```
---

### Soal 4 - Unit Testing
```
Minimal 3 skenario CRUD
Minimal 3 skenario perhitungan angsuran
Contoh xUnit:


[Fact]
public void CreateKredit_ValidData_ShouldSucceed() { ... }

[Fact]
public void CalculateAngsuran_ValidInput_ShouldReturnCorrectResult() { ... }

[Fact]
public void DeleteKredit_NotExist_ShouldReturnNotFound() { ... }
```

### Soal 5 - Error Handling & Logging
```
Error Handling

Data tidak ditemukan → HTTP 404

Input tidak valid → HTTP 400


Logging

Menggunakan Serilog atau NLog

Mencatat request dan response API


Log.Information("Request {Method} {Path}", context.Request.Method, context.Request.Path);
```

### Soal 6 - Performance & Scalability
```
1. Meningkatkan performa API untuk ribuan user per hari
- Gunakan connection pooling
- Optimasi query & indexing
- Asynchronous programming
- Caching hasil query atau endpoint

2. Caching
- Gunakan Redis untuk menyimpan hasil perhitungan angsuran atau query data
- Implementasi melalui middleware caching atau service layer caching

3. Arsitektur Microservices
- Pisahkan modul: KreditService, AuthService, CalculationService
- Gunakan API Gateway
- Containerization (Docker/Kubernetes)
- Centralized logging & monitoring
```

## Cara Menjalankan Proyek
1. Clone Repository
 ```
git clone https://github.com/anggyrendra/CreditAPI.git
cd CreditAPI
```
3. Restore & Build
```
dotnet restore
dotnet build
```
3. Konfigurasi Database
- Update connection string di appsettings.json
- Buat database PostgreSQL 16.3
Jalankan migration
```
dotnet ef database update
```
4. Jalankan API
```
dotnet run --project CreditAPI
```
5. Testing
```
dotnet test
```
## apabila mengalami error silahkan extra file 
```
creditAPI.rar
```
