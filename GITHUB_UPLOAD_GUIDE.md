# 🚀 GitHub Upload Guide - ASP.NET Aspire To-Do App

## Step-by-Step Instructions

### **1️⃣ Create Repository on GitHub**

1. Go to https://github.com/new
2. Fill in the repository details:
   - **Repository name**: `asp-todoapp` (or your preferred name)
   - **Description**: `Full-stack To-Do app built with ASP.NET Aspire, REST API & Blazor Server`
   - **Visibility**: Choose `Public` (or `Private` if you prefer)
   - **Initialize repository**: Leave unchecked (we'll push existing code)
3. Click **"Create repository"**

---

### **2️⃣ Initialize Git Locally (if not already done)**

Open PowerShell/Terminal in your project root directory and run:

```bash
# Navigate to your project root
cd C:\Users\Ahmad\YourProjectPath

# Initialize git repo
git init

# Add all files (respects .gitignore)
git add .

# Create initial commit
git commit -m "Initial commit: Full ASP.NET Aspire To-Do application"
```

---

### **3️⃣ Add GitHub Remote and Push**

Replace `YOUR_USERNAME` and `YOUR_REPO_NAME` with your actual GitHub username and repository name:

```bash
# Add GitHub as remote
git remote add origin https://github.com/YOUR_USERNAME/YOUR_REPO_NAME.git

# Rename branch to main (if not already)
git branch -M main

# Push to GitHub
git push -u origin main
```

---

### **4️⃣ Set GitHub Authentication (if prompted)**

You have two options:

#### **Option A: Personal Access Token (Recommended)**
1. Go to https://github.com/settings/tokens
2. Click "Generate new token" → "Generate new token (classic)"
3. Select scopes: `repo`, `workflow`, `read:user`, `user:email`
4. Copy the token
5. When Git asks for password, paste the token

#### **Option B: SSH Key (More Secure)**
```bash
# Generate SSH key
ssh-keygen -t ed25519 -C "your_email@example.com"

# Add to SSH agent
eval $(ssh-agent -s)
ssh-add ~/.ssh/id_ed25519

# Copy public key to GitHub
# Go to https://github.com/settings/ssh/new
# Paste content of: cat ~/.ssh/id_ed25519.pub
```

---

### **5️⃣ Verify Upload**

Visit: `https://github.com/YOUR_USERNAME/YOUR_REPO_NAME`

You should see all your files including:
- ✅ Solution files (.sln or .slnx)
- ✅ All project folders
- ✅ Documentation (README.md, QUICKSTART.md, etc.)
- ✅ .gitignore (hidden by default)
- ✅ LICENSE
- ✅ .github/workflows (for CI/CD)

---

## 📋 What Gets Uploaded vs. Ignored

### ✅ **INCLUDED in Repository**
- `*.csproj` and `*.sln` / `*.slnx` files
- `Program.cs` and all source code
- `Components/` and `Pages/` (Blazor files)
- `*.razor` files
- `README.md`, `QUICKSTART.md`, documentation
- `LICENSE`
- `.gitignore`
- `.github/workflows/`
- Configuration files (appsettings.json, etc.)

### ❌ **EXCLUDED by .gitignore**
- `bin/` and `obj/` folders (build artifacts)
- `.vs/` (Visual Studio cache)
- `.vscode/` (VS Code settings)
- `node_modules/`
- `*.suo`, `*.user` (user-specific files)
- Local environment files
- SSL certificates
- `appsettings.local.json`

---

## 🎯 Post-Upload Steps

### **1. Add Repository to GitHub Desktop (Optional)**
```bash
git clone https://github.com/YOUR_USERNAME/YOUR_REPO_NAME.git
```

### **2. Check GitHub Actions Status**
1. Go to your repository → **"Actions"** tab
2. You should see the `.NET Build and Test` workflow running
3. Green checkmark = successful build

### **3. Configure Branch Protection (Recommended)**
1. Go to **"Settings"** → **"Branches"**
2. Click **"Add rule"** under Branch protection rules
3. Select `main` branch
4. Enable protections (require PR reviews, passing checks, etc.)

---

## 📝 Complete Command Summary

Copy and paste this entire block to upload your project:

```bash
# 1. Initialize git (if not done)
git init

# 2. Add files
git add .

# 3. Commit
git commit -m "Initial commit: Full ASP.NET Aspire To-Do application"

# 4. Add remote
git remote add origin https://github.com/YOUR_USERNAME/YOUR_REPO_NAME.git

# 5. Rename branch
git branch -M main

# 6. Push
git push -u origin main
```

---

## ✅ Checklist Before Upload

- [x] `.gitignore` created
- [x] `.github/workflows/dotnet.yml` created
- [x] `LICENSE` file added
- [x] `README.md` updated with badges
- [x] All documentation files present
- [x] `.sln` or `.slnx` file present
- [x] Project builds successfully locally
- [x] GitHub repository created
- [x] Git initialized locally
- [x] Files committed with meaningful message

---

## 🆘 Troubleshooting

### "fatal: remote origin already exists"
```bash
git remote remove origin
git remote add origin https://github.com/YOUR_USERNAME/YOUR_REPO_NAME.git
```

### "Permission denied (publickey)"
Make sure SSH key is added to GitHub or use HTTPS with personal access token.

### "Updates were rejected because the remote contains work that you do not have locally"
```bash
git pull origin main --rebase
git push origin main
```

### Large files rejected (>100MB)
```bash
# Check file size
git ls-files --size

# If needed, install Git LFS
git lfs install
git lfs track "*.iso"
git add .gitattributes
```

---

## 📚 Additional Resources

- [GitHub Getting Started](https://docs.github.com/en/get-started)
- [Git Tutorial](https://git-scm.com/docs)
- [GitHub CLI Tool](https://cli.github.com/)
- [Managing SSH Keys](https://docs.github.com/en/authentication/connecting-to-github-with-ssh)

---

## 🎉 Done!

Your ASP.NET Aspire To-Do app is now on GitHub! 🚀

**Share your repository URL:**
`https://github.com/YOUR_USERNAME/YOUR_REPO_NAME`

Consider starring ⭐ the repo if you want to bookmark it yourself!
