# AuthServer
.NET Core ile AuthServer JWT tabanlı kimlik doğrulama yapısı ile Access Token ve Refresh Token mekanizması kurgulandı.

Biri ana olmak üzere toplam dört API geliştirildi. Her kullanıcı, sadece yetkili olduğu API'lere erişim sağlayabilmekte; yetkisi olmayan isteklere sistem tarafından izin verilmemektedir.

AuthServer.API klasöründe appsettings dosyasında database connection string girerek kullanabilirsiniz
