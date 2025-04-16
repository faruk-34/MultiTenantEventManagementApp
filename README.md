
# 🎟️ MultiTenantEventManagementApp

Bu proje, çok kiracılı (multi-tenant) bir etkinlik yönetim sistemidir. Kullanıcılar farklı kiracılar altında etkinlik oluşturabilir, kayıt olabilir ve yönetimsel işlemler gerçekleştirebilir.

## 🏗️ Proje Katmanları

### 📁 `Application`
İş mantığının yazıldığı katmandır.
- `Interfaces`: Servis arabirimleri
- `Mapping`: DTO ↔ Entity dönüşümleri
- `Models`: DTO'lar ve view modeller
- `Services`: İş mantığı servisleri
- `Validator`: FluentValidation sınıfları

### 📁 `Domain`
Veri modeli ve temel iş kurallarının bulunduğu katmandır.
- `Entities`: Temel varlık sınıfları (User, Tenant, Event, vb.)
- `Enums`: Enum tanımları
- `Interfaces`: Domain katmanına ait arabirimler
- `Imports`: Seed veya başlangıç verileri ile ilgili sınıflar

### 📁 `Infrastructure`
Veri erişimi ve dış servis entegrasyonlarının yapıldığı katmandır.
- `Context`: EF Core DB context’leri
- `Migrations`: Migration dosyaları
- `Redis`: Redis önbellekleme altyapısı
- `Repositories`: Repository implementasyonları
- `ModelBuilderExtensions.cs`: Varlık yapılandırmaları
- `WorkContext.cs`: Kullanıcı bilgilerini taşıyan context

### 📁 `WebAPI`
API uç noktalarının tanımlandığı katmandır.
- `Controllers`: REST API controller’ları
- `Middleware`: Global hata yakalayıcı vb. özel middleware bileşenleri
- `GlobalExceptionHandler.cs`: SeriLog ile alınan  hataların consolda gösterilmesi ve loglanması. 
- `Program.cs`: Uygulama başlatma noktası
- `appsettings.json`: Uygulama konfigürasyonu

### 🧪 `tests`
Uygulamanın testleri.
- `TestsApplication`: Test projesi
- `TenantServiceTests.cs`: Tenant servislerine ait testler

---

## 🚀 Kurulum

```bash
git clone https://github.com/faruk-34/MultiTenantEventManagementApp.git
cd MultiTenantEventManagementApp
```

### Gerekli Araçlar:
- [.NET 8 SDK](https://dotnet.microsoft.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- Redis (isteğe bağlı, Redis özellikleri varsa)


## 🗄️ Entity Framework Core Migration Komutları

Migration oluşturmak için:

```bash
Add-Migration mg1 -StartupProject WebAPI -Project Infrastructure
```

Veritabanını güncellemek için:

```bash
Update-Database -StartupProject WebAPI -Project Infrastructure
```

Alternatif olarak terminal (CLI) üzerinden:

```bash
dotnet ef migrations add mg1 --startup-project ../WebAPI --project .
dotnet ef database update --startup-project ../WebAPI --project .
```
> Not: `Infrastructure` klasöründe `DbContext` bulunduğu için EF komutları bu proje üzerinden çalıştırılır.
```

## 📌 Temel Özellikler

- ✅ Çok kiracılı (multi-tenant) yapı
- ✅ JWT tabanlı kimlik doğrulama
- ✅ Etkinlik oluşturma ve yönetimi
- ✅ Katılımcı kayıt işlemleri
- ✅ Global hata yönetimi ve validasyon

---

## 🛡️ Kimlik Doğrulama

Kimlik doğrulama JWT ile yapılmaktadır. Token alımı için `/api/auth/login` uç noktası kullanılabilir.

---

## 📬 API Uç Noktaları

Swagger dokümantasyonu yakında eklenecektir. Şimdilik Postman veya `WebAPI.http` dosyası üzerinden istekler yapılabilir.

---


