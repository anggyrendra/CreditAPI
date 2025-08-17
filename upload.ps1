# =======================
# Script Upload GitHub Otomatis
# =======================

# Konfigurasi
$RepoPath = "D:\KreditAPI\CreditAPI"   # Folder project
$CommitMessage = Read-Host "Masukkan pesan commit"

# Input GitHub credentials
$GitHubUsername = Read-Host "anggyrendra"
$GitHubPAT = Read-Host "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" -AsSecureString

# Convert secure string ke plain text untuk digunakan di URL
$PtrBSTR = [System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($GitHubPAT)
$GitHubPATPlain = [System.Runtime.InteropServices.Marshal]::PtrToStringAuto($PtrBSTR)

# Nama repo (sesuai repo GitHub Anda)
$RepoName = "CreditAPI"

# Pindah ke folder project
Set-Location $RepoPath

# Cek apakah git sudah di-init
if (-not (Test-Path ".git")) {
    git init
    Write-Host "Git repository baru di-init."
}

# Tambahkan semua perubahan
git add .

# Commit perubahan
git commit -m "$CommitMessage"

# Set remote URL menggunakan PAT
$RemoteURL = "https://${GitHubUsername}:${GitHubPATPlain}@github.com/${GitHubUsername}/${RepoName}.git"

# Cek apakah remote origin sudah ada
$remote = git remote
if ($remote -contains "origin") {
    git remote set-url origin $RemoteURL
    Write-Host "Remote origin di-update."
} else {
    git remote add origin $RemoteURL
    Write-Host "Remote origin ditambahkan."
}

# Cek apakah branch main ada
$branchExists = git branch --list main
if (-not $branchExists) {
    git checkout -b main
    Write-Host "Branch 'main' dibuat."
} else {
    git checkout main
}

# Push ke branch main
git push -u origin main

Write-Host "Upload selesai!"
