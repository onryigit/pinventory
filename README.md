# 📍 Pinventory - Akıllı Depo Yönetim Sistemi

![License](https://img.shields.io/badge/license-MIT-blue.svg)
![.NET Core](https://img.shields.io/badge/.NET%20Core-10.0-purple)
![Status](https://img.shields.io/badge/Status-Completed-success)

**Pinventory**, karmaşık depo süreçlerini sadeleştirmek ve kaotik düzendeki depolarda ürün bulma süresini en aza indirmek için geliştirilmiş; görsel haritalama ve sesli komut teknolojilerini kullanan modern bir stok takip çözümüdür.

##  Proje Hakkında
Çalıştığım firmadaki gerçek bir problemden yola çıkılarak geliştirilmiştir. Binlerce ürünün bulunduğu depolarda "Hangi ürün nerede?" sorusuna en hızlı cevabı vermek amacıyla tasarlanmıştır. Klasik liste mantığının ötesine geçerek, ürünleri **raf fotoğrafları üzerinde görsel koordinatlarla (X/Y)** işaretler.

##  Temel Özellikler

* ** Sesli Arama (Voice-to-Query):** Web Speech API entegrasyonu sayesinde kullanıcılar klavye kullanmadan, sadece ürün adını söyleyerek arama yapabilir.
* ** Görsel İşaretleme (Visual Mapping):** Ürünler veritabanında sadece metin olarak değil, raf görseli üzerindeki % (yüzde) koordinatlarıyla saklanır.
* ** Hata Toleranslı Arama (Fuzzy Search):** Kullanıcı hatalı yazsa bile (Örn: "Vitra" yerine "Vtra"), FuzzySharp algoritması en yakın sonucu bulur.
* ** Dark Mode & Responsive:** Bootstrap 5 ile geliştirilen, tablet ve mobilde göz yormayan karanlık mod arayüzü.
* ** Yönetim Paneli:** Kolayca yeni bölge (raf/oda) ekleme, resim yükleme ve ürün pinleme işlemleri.

##  Kullanılan Teknolojiler

Proje **Clean Architecture** prensiplerine sadık kalınarak, modüler ve sürdürülebilir bir yapıda geliştirilmiştir.

| Alan | Teknoloji |
| --- | --- |
| **Backend** | ASP.NET Core 10.0 MVC |
| **ORM / Veri** | Entity Framework Core (Code-First), MSSQL |
| **Mimari** | MVC, Dependency Injection, Repository Pattern |
| **Frontend** | HTML5, CSS3, Bootstrap 5, JavaScript |
| **Kütüphaneler** | jQuery UI (Autocomplete), FuzzySharp, Web Speech API |

##  Ekran Görüntüleri

<img width="1350" height="1013" alt="i1" src="https://github.com/user-attachments/assets/65cb1dc7-b774-4176-a998-a5dca5de3bd1" />
<img width="1903" height="952" alt="i2" src="https://github.com/user-attachments/assets/59b93100-363b-4289-8751-1c7a0fe7a175" />
<img width="1350" height="1253" alt="i3" src="https://github.com/user-attachments/assets/edd6aad5-93d8-47fd-b28b-0560fd02d20c" />
<img width="1350" height="1013" alt="i4" src="https://github.com/user-attachments/assets/d8e12772-1f3a-4baa-bf0d-5255183f2dc1" />
<img width="1365" height="1942" alt="i44" src="https://github.com/user-attachments/assets/21af3c82-5e2d-4f8b-afdc-cff7b93bf890" />
<img width="1350" height="1942" alt="i5" src="https://github.com/user-attachments/assets/79ecb2b5-44fb-4653-b093-d81dbab91d8a" />
<img width="1350" height="1013" alt="i6" src="https://github.com/user-attachments/assets/f6e360de-b328-41dd-b9b8-9965c18c8bbf" />
<img width="1350" height="1013" alt="i7" src="https://github.com/user-attachments/assets/f6d5e1e1-7b8b-4717-9bee-2f621e697d40" />
*(Not: Ekran görüntüleri temsilidir, projeyi indirip inceleyebilirsiniz.)*

