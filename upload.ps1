# Nama repo GitHub
$REPO_NAME = "CreditAPI"
$GITHUB_USER = "anggyrendra" # Ganti dengan username GitHub-mu
$GITHUB_URL = "https://github.com/$GITHUB_USER/$REPO_NAME.git"

# Inisialisasi git jika belum
if (!(Test-Path ".git")) {
    git init
}

# Set branch utama
git branch -M main

# Tambahkan semua file
git add .

# Commit perubahan
git commit -m "Initial commit"

# Tambahkan remote repository jika belum ada
$remote = git remote
if (-not ($remote -contains "origin")) {
    git remote add origin $GITHUB_URL
}

# Push ke GitHub
git push -u origin main
