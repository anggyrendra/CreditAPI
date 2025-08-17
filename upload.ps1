# upload.ps1
# Script otomatis upload ke GitHub dengan pull, conflict handling, dan opsi force push

# Konfigurasi GitHub
$GitHubUsername = "anggyrendra"          # ganti dengan username GitHub-mu
$GitHubPAT = "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"  # ganti dengan Personal Access Token
$BranchName = "main"                     # branch default

# Remote URL dengan token (HTTPS)
$RemoteURL = "https://${GitHubUsername}:${GitHubPAT}@github.com/${GitHubUsername}/CreditAPI.git"

Write-Host "Memulai upload repository GitHub..." -ForegroundColor Cyan

# Tambahkan semua perubahan
git add .

# Commit perubahan
$CommitMessage = Read-Host "Masukkan pesan commit"
if (-not $CommitMessage) {
    $CommitMessage = "Update project $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')"
}
git commit -m "$CommitMessage"

# Pull dulu untuk sinkronisasi
Write-Host "Melakukan git pull untuk sinkronisasi..." -ForegroundColor Yellow
$pullResult = git pull $RemoteURL $BranchName --allow-unrelated-histories 2>&1

if ($pullResult -match "CONFLICT") {
    Write-Host "Terjadi konflik saat pull!" -ForegroundColor Red
    $choice = Read-Host "Apakah ingin resolve konflik otomatis dengan versi lokal? (y/n)"
    if ($choice -eq "y") {
        Write-Host "Menyelesaikan konflik dengan versi lokal..." -ForegroundColor Yellow
        git merge --strategy-option theirs
        git add .
        git commit -m "Resolve conflicts using local version"
    } else {
        Write-Host "Batal push karena konflik belum diselesaikan." -ForegroundColor Red
        exit
    }
}

# Push ke remote
Write-Host "Melakukan git push ke remote..." -ForegroundColor Green
try {
    git push $RemoteURL $BranchName
} catch {
    Write-Host "Push gagal, kemungkinan remote memiliki commit baru." -ForegroundColor Red
    $forceChoice = Read-Host "Apakah ingin melakukan force push? (y/n)"
    if ($forceChoice -eq "y") {
        git push $RemoteURL $BranchName --force
        Write-Host "Force push selesai!" -ForegroundColor Cyan
    } else {
        Write-Host "Push dibatalkan." -ForegroundColor Red
        exit
    }
}

Write-Host "Upload selesai!" -ForegroundColor Cyan
