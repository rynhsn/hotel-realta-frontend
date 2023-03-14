# MIGRASI FRONTEND PROJECT

## Clone Project  
  ```
  git clone https://github.com/rynhsn/hotel-realta-frontend.git
  cd ./hotel-realta-frontend
  git checkout -b [namabranch]
  ```
- [X] Sesuaikan menu section di folder `Shared/Partials/Sidebar.razor`
- [X] Pindahkan file di folder `Pages` masing-masing module ke `Pages` project baru
- [X] Pindahkan file di folder `Components` masing-masing module ke `Components` project baru (Jika ada)
- [X] Beresin dulu semua tampilannya pake data dummy
- [X] Bisa gunakan komponen Plain Admin Pro, untuk referensi di:
  - [Link Dokumentasi](https://plainadmin.com/docs )
  - [Link Demo](https://demo.plainadmin.com/)
- [X] Push tampilan
  ```
  git add .
  git commit -m "[message]"
  ```
  ```
  git push -u origin [namabranch]
  ```
  ```
  git pull origin [namabranch]
  ```
- [X] Buat branch baru
  ```
  git checkout -b [namabranch]-data
  ```
- [X] Pindahkan file di folder `HttpRepository` masing-masing module ke `HttpRepository` project baru 
- [X] Sesuaikan services di file `Program.cs` sesuai module
- [X] Push project
  ```
  git add .
  git commit -m "[message]"
  git push -u origin [namabranch]-data
  ```
