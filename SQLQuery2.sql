-- Veritabanını oluştur
CREATE DATABASE OyunDB;
GO

-- O veritabanını kullan
USE OyunDB;
GO

-- Oyun kayıtlarını tutacak tabloyu oluştur
CREATE TABLE OyunLoglar (
    LogID INT IDENTITY(1,1) PRIMARY KEY, -- Otomatik artan ID
    OyuncuAdi NVARCHAR(50),              -- Oyuncunun adı
    HedefSayi NVARCHAR(10),              -- Tutulan sayı
    HamleSayisi INT,                     -- Kaç hamlede bildi
    Tarih DATETIME,                      -- Oyun tarihi
    Sure INT                             -- Süre (saniye)
);
GO

-- 1. SORGU: TÜM VERİLER (ham hali)
SELECT * 
FROM OyunLoglar;
GO

-- 2. SORGU: SIRALI LİSTE (leaderboard mantığı)
-- Önce hamle sayısına göre, eşitse süreye göre sıralar
SELECT OyuncuAdi, HamleSayisi, Sure, Tarih
FROM OyunLoglar
ORDER BY HamleSayisi ASC, Sure ASC;
GO

-- 3. SORGU: SADECE EN İYİ 5 OYUNCU
SELECT TOP 5 OyuncuAdi, HamleSayisi, Sure, Tarih
FROM OyunLoglar
ORDER BY HamleSayisi ASC, Sure ASC;
GO