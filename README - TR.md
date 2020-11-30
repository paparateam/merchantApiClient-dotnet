# İçindekiler

<a href="#intro">Giriş</a>

<a href="#enums">Enumlar</a>

<a href="#account">Hesap Bilgileri</a>

<a href="#banking">Bankacılık</a>

<a href="#cash-deposit">Fiziksel Nokta Entegrasyonu</a>

<a href="#mass-payment">Ödeme Dağıtma</a>

<a href="#payments">Ödeme Alma</a>

<a href="#validation">Doğrulama</a>

<a href="#response-types">Geri Dönüş Tipleri</a>

# <a name="intro">Giriş</a> 

Papara ile entegre olmak için aşağıdaki adımları takip edebilirsiniz;

1. API Anahtarınızı edinin. Böylece Papara doğrulama sistemi API isteklerinin kimliğini doğrulayabilir. API Anahtarınızı almak için https://merchant.test.papara.com/ URL adresine gidin. Başarıyla oturum açtıktan sonra, API Anahtarı https://merchant.test.papara.com/APIInfo adresinde görüntülenebilir.

2. Kütüphaneyi kurun. Böylece yazılımınız Papara API ile entegre olabilir. Kurulum işlemleri aşağıdaki gibidir.

## Nuget İşlemleri

```bash
# dotnet ile yükleme
dotnet add package Papara.Net
dotnet restore
```
veya

```bash
# NuGet ile yükleme
PM> Install-Package Papara.Net
```

# Konfigürasyonlar

## Dotnet Core Kurulumu

API'ye bağlanmadan önce, dotnet core geliştiricileri istemci ayarlarını yapılandırmalıdır. Projenizin kök dizininde bir `appsettings.json` dosyası oluşturun. Uygun `appsettings.json` dosyası aşağıdaki gibi görünmelidir;

``` json  {
{
  "Papara": {
  	"ApiKey": "INSERT_YOUR_API_KEY_HERE", // Papara'ya kayıtlı API KEY
  	"Env": "Test", // Test veya Geliştirme ortamı seçimi
  }
}
```

`Appsetting.json` dosyasını oluşturduktan sonra, `Startup.cs` dosyanıza `ConfigureServices` methoduna aşağıdaki satırları ekleyin.

```csharp
services.AddPapara(o =>
                   {
                       o.ApiKey = Configuration["Papara:ApiKey"];
                       o.Env = Configuration["Papara:Env"];
                   });
```

Kütüphaneyi kurmak için dependency injection kullanılabilir. API Anahtarı ve ortamı değişkeni, `appsettings.json` dosyasından okunacaktır.

```csharp
private readonly  IPaparaClient PaparaClient;
public AccountController(IPaparaClient  paparaClient)
{
    this.PaparaClient  = paparaClient;      
}
```

Başka bir yol ise manuel olarak oluşturmaktır.

```csharp
private readonly  PaparaClient paparaClient;         

public AccountController()
{
    paparaClient = new PaparaClient("INSERT_YOUR_API_KEY_HERE",PaparaEnv.Test);
} 
```

Veya, `SetOptions` methodu kullanılabilir. API Anahtarı ve ortamı değişkeni, `appsettings.json` dosyasından okunacaktır.

```csharp
PaparaClient  paparaClient = new PaparaClient();  
paparaClient.SetOptions("INSERT_YOUR_API_KEY_HERE", PaparaEnv.Test);
```

### Dotnet Core API Test İsteği

Her şey ayarlandıktan sonra, her şeyin çalıştığını test etmek için aşağıdaki kod bloğunu kullanın.

```csharp
private readonly  IPaparaClient PaparaClient;

public  AccountController(IPaparaClient paparaClient)
{        
    this.PaparaClient  = paparaClient;
}       

[HttpGet("[action]")]  
public async  Task<ActionResult<Account>> GetAccount()   
{    
    PaparaSingleResult<Account>  accountResult = await this.PaparaClient.AccountService.GetAccountAsync();
    if(accountResult.Succeeded) 
    {         
        Account account =  accountResult.Data; 
        return  Ok(account);  
    }     
    else     
    {    
        return  BadRequest(accountResult.Error);  
    }   
}  
```

*Not: Bütün methodların hem senkron hem de asenkron halleri mevcuttur.* 

Örn: `await this.PaparaClient.AccountService.GetAccountAsync()` ve `this.PaparaClient.AccountService.GetAccount();` 

## .NET Framework Kurulumu

.NET Framework geliştiricileri için, `ApiKey` ve `Env` değişkenlerini `appSettings` bölümü altındaki `web.config` dosyasına ekleyin.

``` xml  
<add key="Papara:ApiKey" value="INSERT_YOUR_API_KEY_HERE"/>

<add key="Papara:Env" value="Test"/>
```

### .NET Framework API Test İsteği

Her şey ayarlandıktan sonra, her şeyin çalıştığını test etmek için aşağıdaki kod bloğunu kullanın.

``` csharp
private readonly PaparaClient paparaClient;

public AccountController()
{
    paparaClient = new PaparaClient(ConfigurationManager.AppSettings["Papara:ApiKey"], PaparaEnv.Test);
}

public JsonResult<Account> GetAccount()
{
    var accountResult = this.paparaClient.AccountService.GetAccount();

    if (!accountResult.Succeeded)
    {
        throw new Exception(accountResult.Error.Message);
    }

    return Json(accountResult.Data);
}
```

# <a name="enums">Enumlar</a>

# CashDepositProvisionStatus

Bir para yatırma talebi yapıldığında, aşağıdaki durumlar geri dönecek ve provizyon durumunu gösterecektir.

| Anahtar         | **Değer** | **Açıklama**         |
| --------------- | --------- | -------------------- |
| Pending         | 0         | Provizyon bekleniyor |
| Complete        | 1         | Tamamlandı           |
| Cancel          | 2         | İptal edildi         |
| ReadyToComplete | 3         | Tamamlanmaya hazır   |

 

# Currency

API'da bulunan bütün para birimleri aşağıdaki gibidir.

| **Anahtar** | **Değer** | **Açıklama**    |
| ----------- | --------- | --------------- |
| TRY         | 0         | Türk Lirası     |
| USD         | 1         | Amerikan Doları |
| EUR         | 2         | Euro            |

 

# EntryType

Giriş Türleri hesap defterlerinde ve para yatırma işlemlerinde parayı takip etmek için kullanılır. Olası giriş türleri aşağıdaki gibidir.

| **Anahtar**                   | **Değer** | **Açıklama**                                                 |
| ----------------------------- | --------- | ------------------------------------------------------------ |
| BankTransfer                  | 1         | Banka Transferi: Para Yatırma veya Çekme                     |
| CorporateCardTransaction      | 2         | Papara Kurumsal Kart İşlemi: Üye iş yerine tahsis edilen kurum kartı ile gerçekleştirilen işlemdir. |
| LoadingMoneyFromPhysicalPoint | 6         | Fiziki Noktadan Para Yükleme: Anlaşmalı yerden nakit para yatırma işlemi |
| MerchantPayment               | 8         | Satıcı Ödemesi: Papara ile Ödeme                             |
| PaymentDistribution           | 9         | Ödeme Dağıtımı: Papara ile toplu ödeme                       |
| EduPos                        | 11        | Çevrimdışı ödeme. Papara üzerinden EDU POS                   |

 

# PaymentMethod

Kabul edilen üç ödeme yöntemi aşağıdaki gibidir.

| **Anahtar**   | **Değer** | **Açıklama**          |
| ------------- | --------- | --------------------- |
| PaparaAccount | 0         | Papara Hesap Bakiyesi |
| Card          | 1         | Tanımlı Kredi Kartı   |
| Mobile        | 2         | Mobil Ödeme           |

 

# PaymentStatus

Ödeme tamamlandıktan sonra API'dan aşağıdaki ödeme durumları dönecektir.

| **Anahtar** | **Değer** | **Açıklama**               |
| ----------- | --------- | -------------------------- |
| Pending     | 0         | Ödeme Bekliyor             |
| Completed   | 1         | User completed the payment |
| Refunded    | 2         | Order refunded             |

# <a name="account">Hesap Bilgileri</a>

Bu bölüm üye işyerine ait hesap ve bakiye bilgilerinin kullanımı için hazırlanan teknik entegrasyon bilgilerini içerir. Papara hesabındaki hesap ve bakiye bilgileri `Account` servisi ile alınabilir. Geliştiriciler ayrıca bakiyede değişiklik işlemlerin bir listesini içeren bakiye geçmişini de alabilirler.

## Hesap Bilgilerine Erişim

Satıcı hesabı ve bakiye bilgilerini döndürür. Bakiye bilgileri cari bakiyeyi, kullanılabilir ve blokeli bakiyeyi içerirken, hesap bilgileri satıcının marka adını ve tam unvanını içerir. Bu işlemi gerçekleştirmek için `Account` servisinde bulunan `GetAccount` methodunu kullanın.

### Account Model

`Account` sınıfı, `Account` servisi tarafından API'den dönen hesap bilgileri eşleştirmek için kullanılır ve hesap bilgilerini içerir.

| **Değişken Adı**    | **Tip**                  | **Açıklama**                                                 |
| ------------------- | ------------------------ | ------------------------------------------------------------ |
| LegalName           | string                   | Satıcının şirket unvanını alır veya belirler.                |
| BrandName           | string                   | Satıcının şirket marka adını alır veya belirler.             |
| AllowedPaymentTypes | List<AllowedPaymentType> | Satıcının şirket için kabul edilen ödeme tiplerini alır veya belirler. |
| Balances            | List<AccountBalance>     | Satıcının şirketin hesap bakiyesini alır veya belirler.      |

### AllowedPaymentType Model

`AllowedPaymentType` sınıfı, `Account` servisi tarafından API'den dönen hesap bilgilerini eşleştirmek için kullanılır. `AllowPaymentType`, izin verilen ödeme türlerini gösterir.

| **Değişken adı** | **Tip** | **Açıklama**                                                 |
| ---------------- | ------- | ------------------------------------------------------------ |
| PaymentMethod    | int     | Ödeme tipini alır veya belirler.<br />0 – Papara Hesap Bakiyesi  <br />1 – Kredi/Banka kartı <br />2 – Mobil Ödeme. |

### AccountBalance Model

`AccountBalance` sınıf, `Account` servisi tarafından API'den dönen hesap bakiyesi değeriyle eşleştirmek için kullanılır. Hesap bakiyesi, cari bakiye rakamlarını gösterir ve üç tür bakiye ve genel para birimini listeler.

| **Değişken adı** | **Tip** | **Açıklama**                                |
| ---------------- | ------- | ------------------------------------------- |
| Currency         | int     | Para birimini alır veya belirler.           |
| TotalBalance     | decimal | Toplam bakiyeyi alır veya belirler.         |
| LockedBalance    | decimal | Blokeli bakiyeyi alır veya belirler.        |
| AvailableBalance | decimal | Kullanılabilir bakiyeyi alır veya belirler. |

### Servis Methodu

#### Kullanım Amacı 

Yetkili satıcı için hesap bilgilerini ve cari bakiyeyi getirir.

| **Method**                  | **Parametreler** | **Geri Dönüş Tipi**         |
| --------------------------- | ---------------- | --------------------------- |
| GetAccount, GetAccountAsync |                  | PaparaSingleResult<Account> |

#### Kullanım

``` csharp
private readonly IPaparaClient PaparaClient;
public AccountController(IPaparaClient paparaClient)
{
    this.PaparaClient = paparaClient;
}

[HttpGet("[action]")]
public async Task<ActionResult<Account>> GetAccount()
{
    PaparaSingleResult<Account> accountResult = await this.PaparaClient.AccountService.GetAccountAsync();

    if (accountResult.Succeeded)
    {
        Account account = accountResult.Data;

        return Ok(account);
    }
    else
    {
        return BadRequest(accountResult.Error);
    }
}
```

## Hesap Hareketlerini Listeleme

Satıcı hesap hareketlerini(işlem listesi) sayfalı biçimde döndürür. Bu method, her işlem için ortaya çıkan bakiye dahil olmak üzere bir satıcı için yapılan tüm işlemleri listelemek için kullanılır. Bu işlemi gerçekleştirmek için `Account` hizmetinde `ListLedgers` methodunu kullanın. `StartDate` ve `EndDate` bilgileri gönderilmelidir.

### AccountLedger Model

`AccountLedger` sınıfı, `Account` servisi tarafından API'den dönen değerleri eşleştirmek için kullanılır. Bir işlemin kendisini temsil eder.

| **Değişken Adı**    | **Tip**      | **Açıklama**                                                 |
| ------------------- | ------------ | ------------------------------------------------------------ |
| ID                  | int?         | Merchant ID alır veya belirler.                              |
| CreatedAt           | DateTime     | Hesap hareketlerinin oluşma tarihinialır veya belirler.      |
| EntryType           | EntryType?   | Giriş türünü alır veya belirler.                             |
| EntryTypeName       | string       | Giriş tür adını alır veya belirler.                          |
| Amount              | decimal?     | Tutarı alır veya belirler.                                   |
| Fee                 | decimal?     | Hizmet bedelini alır veya belirler.                          |
| Currency            | int?         | Para birimini alır veya belirler.                            |
| CurrencyInfo        | CurrencyInfo | Para birimi bilgisini alır veya belirler.                    |
| ResultingBalance    | decimal?     | Kalan bakiyeyi alır veya belirler.                           |
| Description         | string       | Açıklamayı alır veya belirler.                               |
| MassPaymentId       | string       | Toplu ödeme ID'sini alır veya belirler. Ödeme işlemlerinde mükerrer tekrarı önlemek için üye işyeri tarafından gönderilen benzersiz değerdir. Hesap hareketlerinde toplu ödeme türü işlem kayıtlarında işlemin kontrolünü sağlamak için görüntülenir. Diğer ödeme türlerinde boş olacaktır. |
| CheckoutPaymentId   | string       | Ödeme ID'sini alır veya belirler. Ödeme kaydı işleminde veri nesnesinde bulunan kimlik alanıdır. Ödeme işleminin benzersiz tanımlayıcısıdır. Hesap hareketlerinde kasa tipi işlem kayıtlarında görüntülenir. Diğer ödeme türlerinde boş olacaktır. |
| CheckoutReferenceID | string       | Checkout referans ID'ini alır veya belirler. Bu, ödeme işlemi kaydı oluşturulurken gönderilen referans kimliği alanıdır. Üye işyeri sisteminde ödeme işleminin referans bilgisidir. Hesap hareketlerinde kasa tipi işlem kayıtlarında görüntülenir. Diğer ödeme türlerinde boş olacaktır |

### CurrencyInfo Model

`CurrencyInfo` sınıfı, `AccountLedger` modeli tarafından API'den dönen para birimi değerlerini almak veya ayarlamak için kullanılır. Hesap hareketlerinde bulunan para birimi bilgilerini temsil eder.

| **Değişken Adı**     | **Tip**  | **Açıklama**                                                 |
| -------------------- | -------- | ------------------------------------------------------------ |
| CurrencyEnum         | Currency | Para birimi tipini alır veya belirler                        |
| Symbol               | string   | Para birimi sembolünü alır veya belirler                     |
| Code                 | string   | Para birimi kodunu alır veya belirler                        |
| PreferredDisplayCode | string   | Para biriminin tercih edilen gösterim kodunu alır veya belirler |
| Name                 | string   | Para biriminin adını alır veya belirler                      |
| IsCryptoCurrency     | bool?    | Para biriminin kripto para olup olmadığını alır veya belirler |
| Precision            | int      | Para biriminin virgülden sonra kaç hane gösterileceğini alır veya belirler |
| IconUrl              | string   | Para birimi ikonu URL'ini alır veya belirler                 |

### LedgerListOptions Model

`LedgerListOptions` `Account` servisi tarafından hesap hareketleri listeleme işlemine istek parametreleri sağlamak için kullanılır

| **Değişken Adı** | **Tip**  | **Açıklama**                                                 |
| ---------------- | -------- | ------------------------------------------------------------ |
| StartDate        | DateTime | İşlemlerin başlangıç tarihini alır veya belirler             |
| EndDate          | DateTime | İşlemlerin bitiş tarihlerini alır veya belirler              |
| EntryType        | enum     | İşlemlerin hareket tiplerini alır veya belirler              |
| AccountNumber    | int?     | Satıcı hesap numarasını alır veya belirler                   |
| Page             | int      | İstenen sayfa numarasını alır veya belirler. İstenen tarihte, istenen PageSize için 1'den fazla sonuç sayfası varsa, bunu sayfalar arasında dönmek için kullanın |
| PageSize         | int      | Bir sayfada getirilmesi istenen kalem sayısını alır veya belirler. Min=1, Max=50 |

### Servis Methodu

#### Kullanım Amacı

Yetkili satıcı için hesap hareketleri listesini döndürür.

| **Method**                    | **Parametreler**  | **Geri Dönüş Tipi**             |
| ----------------------------- | ----------------- | ------------------------------- |
| ListLedgers, ListLedgersAsync | LedgerListOptions | PaparaListResult<AccountLedger> |

#### Kullanım Şekli

``` csharp
LedgerListOptions ledgerListOptions = new LedgerListOptions
{
    StartDate = DateTime.Now.AddDays(-30),
    EndDate = DateTime.Now,
    Page = 1,
    PageSize = 20
};

PaparaListResult<AccountLedger> listLedgersResult = await this.PaparaClient.AccountService.ListLedgersAsync(ledgerListOptions);

if (listLedgersResult.Succeeded)
{
    PaparaPagingResult<AccountLedger> accountLedgers = listLedgersResult.Data;

    return Ok(accountLedgers);
}
else
{
    return BadRequest(listLedgersResult.Error);
}
```

## Mutabakat Bilgilerine Erişim

Verilen süre içindeki işlemlerin sayısını ve hacmini hesaplar. Bu işlemi gerçekleştirmek için ` Account`  servisinde bulunan ` GetSettlement` methodunu kullanın. ` StartDate` ve ` EndDate` gönderilmelidir.

### Settlement Model

`Settlement` sınıfı, ` Account` servisi tarafından API'dan dönen mutabakat değerlerini eşleştirmek için kullanılır.

| **Değişken Adı** | **Tip** | **Açıklama**                       |
| ---------------- | ------- | ---------------------------------- |
| Count            | int?    | İşlem sayısını alır veya belirler. |
| Volume           | int?    | İşlem hacmini alır veya belirler   |

### SettlementGetOptions Model

`SettlementGetOptions` sınıfı, ` Account` servisi tarafından API'dan dönen mutabakat değerlerini eşleştirmek için kullanılır.

| **Değişken Adı** | **Tip**   | **Açıklama**                                      |
| ---------------- | --------- | ------------------------------------------------- |
| StartDate        | DateTime  | İşlemlerin başlangıç tarihini alır veya belirler. |
| EndDate          | DateTime  | İşlemlerin bitiş tarihini alır veya belirler.     |
| EntryType        | EntryType | İşlemlerin giriş tipini alır veya belirler.       |

### Servis Methodu

#### Kullanım Amacı

Yetkili satıcı için mutabakat bilgilerini getirir.

| **Method**                         | **Parametreler**     | **Dönüş Tipi**                 |
| ---------------------------------- | -------------------- | ------------------------------ |
| GetSettlement,  GetSettlementAsync | SettlementGetOptions | PaparaSingleResult<Settlement> |

#### Kullanım Şekli

``` csharp
SettlementGetOptions settlementGetOptions = new SettlementGetOptions
{
    StartDate = DateTime.Now.AddDays(-30),
    EndDate = DateTime.Now
};

PaparaSingleResult<Settlement> settlementResult = await this.PaparaClient.AccountService.GetSettlementAsync(settlementGetOptions);

if (settlementResult.Succeeded)
{
    Settlement settlement = settlementResult.Data;

    return Ok(settlement);
}
else
{
    return BadRequest(settlementResult.Error);
}
```

# <a name="banking">Bankacılık</a> 

Bu bölümde, banka hesaplarını Papara'da hızlı ve güvenli bir şekilde listelemek ve / veya banka hesaplarına para çekme talebi oluşturmak isteyen işyerleri için hazırlanmış teknik entegrasyon bilgileri yer almaktadır.

## Banka Hesap Bilgilerine Erişim

Satıcı kurumun kayıtlı banka hesaplarını getirir. Bu işlemi gerçekleştirmek için `Banking` servisinde bulunan `GetBankAccounts` methodunu kullanın.

### BankAccount Model

`BankAccount` sınıfı, `Banking` servisi tarafından API'den dönen banka hesaplarını eşleştirmek için kullanılır

| **Değişken Adı** | **Tip** | **Açıklama**                                     |
| ---------------- | ------- | ------------------------------------------------ |
| BankAccountId    | int?    | Satıcının banka hesap ID'sini alır veya belirler |
| BankName         | string  | Satıcının banka adını alır veya belirler         |
| BranchCode       | string  | Satıcının şube kodunu alır veya belirler         |
| Iban             | string  | IBAN numarasını alır veya belirler               |
| AccountCode      | string  | Satıcının hesap kodunu alır veya belirler        |
| Description      | string  | Açıklamayı alır veya belirler                    |
| Currency         | string  | Para birimini alır veya belirler                 |

### Servis Methodu

#### Kullanım Amacı

Yetkili satıcı için banka hesaplarını döndürür.

| **Method**      | **Parametreler** | **Geri Dönüş Tipi**            |
| --------------- | ---------------- | ------------------------------ |
| GetBankAccounts |                  | PaparaArrayResult<BankAccount> |

#### Kullanım Şekli

``` csharp 
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması.
};

var bankingService = new BankingService(requestOptions);
var bankingServiceResult = bankingService.GetBankAccounts();

if (!bankingServiceResult.Succeeded)
{
    throw new Exception(bankingServiceResult.Error.Message);
}

return bankingServiceResult;
```

## Para Çekim İşlemi

Satıcılar için para çekme talepleri oluşturur. Bu işlemi gerçekleştirmek için `Banking` hizmetinde `Withdrawal` methodunu kullanın.

### BankingWithdrawalOptions 

`BankingWithdrawalOptions` `Banking` servisi tarafından istek parametrelerini sağlamak için kullanılır.

| **Değişken Adı** | **Tip** | **Açıklama**                                                 |
| ---------------- | ------- | ------------------------------------------------------------ |
| BankAccountId    | int?    | Para çekme işlemi tamamlandığında hangi paranın aktarılacağı hedef banka hesap kimliğini alır veya belirler. Banka hesaplarını listeleme isteği sonucunda elde edilir. |
| Amount           | decimal | Çekilecek para tutarını alır veya belirler.                  |

### Servis Methodu

#### Kullanım Amacı

Yetkili satıcı için belirli bir banka hesabından para çekme talebi oluşturur.

| **Method** | **Parametreler**         | **Geri Dönüş Tipi** |
| ---------- | ------------------------ | ------------------- |
| Withdrawal | BankingWithdrawalOptions | PaparaServiceResult |

#### Kullanım Şekli

``` csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması.
};

var bankingService = new BankingService(requestOptions);

var bankingServiceResult = bankingService.Withdrawal(new BankingWithdrawalOptions
{
    Amount = 1,
    BankAccountId = 1
});

if (!bankingServiceResult.Succeeded)
{
    throw new Exception(bankingServiceResult.Error.Message);
}

return bankingServiceResult;
```

## Olası Hatalar ve Hata Kodları

| **Hata Kodu** | **Hata Açıklaması**                         |
| ------------- | ------------------------------------------- |
| 105           | Yetersiz bakiye                             |
| 115           | Talep edilen miktar minimum limitin altında |
| 120           | Banka hesabı bulunamadı                     |
| 247           | Satıcı hesabı aktif değil                   |

# <a name="cash-deposit">Fiziksel Nokta Entegrasyonu</a> 

Papara fiziksel nokta entegrasyonu ile son kullanıcıların Papara hesaplarına bakiye yükleyebilecekleri para yükleme noktası olabilir ve kazanç sağlayabilirsiniz. Fiziksel nokta entegrasyon yöntemleri sadece kullanıcıların Papara hesaplarına nakit yükledikleri senaryolarda kullanılmalıdır.

## Para Yatırma Bilgilerine Erişim

Nakit para yükleme bilgilerini döndürür. Bu işlemi gerçekleştirmek için `CashDeposit`  servisinde bulunan `GetCashDeposit `methodunu kullanın. `Id` gönderilmelidir.

### CashDeposit Model

`CashDeposit` sınıfı, `CashDeposit` servisi tarafından API'den dönen nakit para yükleme bilgilerini eşleştirmek için kullanılır.

| **Değişken Adı**  | **Tip**   | **Açıklama**                                                 |
| ----------------- | --------- | ------------------------------------------------------------ |
| MerchantReference | string    | Satıcının referans numarasını alır veya belirler.            |
| Id                | int?      | Nakit para yükleme Id'sini alır veya belirler.               |
| CreatedAt         | DateTime? | Nakit para yükleme işleminin yapıldığı alır veya belirler.   |
| Amount            | decimal?  | Nakit para yükleme işleminin tutarını alır veya belirler.    |
| Currency          | int?      | Nakit para yükleme işleminin para birimini alır veya belirler. |
| Fee               | decimal?  | Nakit para yükleme işleminin hizmet bedelini alır veya belirler. |
| ResultingBalance  | decimal?  | Nakit para yükleme işleminden sonra kalan bakiyeyi alır veya belirler. |
| Description       | string    | Nakit para yükleme işleminin açıklamasını alır veya belirler. |

### CashDepositGetOptions

`CashDepositGetOptions` `CashDeposit` servisine istek parametrelerini sağlamak için kullanılır

| **Değişken Adı** | **Tip** | **Açıklama**                                   |
| ---------------- | ------- | ---------------------------------------------- |
| Id               | long    | Nakit para yükleme Id'sini alır veya belirler. |

### Servis Methodu

#### Kullanım Amacı

Nakit para yükleme işlemi bilgilerini döner

| **Method**     | **Parametreler**      | **Geri Dönüş Tipi**             |
| -------------- | --------------------- | ------------------------------- |
| GetCashDeposit | CashDepositGetOptions | PaparaSingleResult<CashDeposit> |

####   Kullanım Şekli

``` csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var cashDepositService = new CashDepositService(requestOptions);
var cashDepositServiceResult = cashDepositService.GetCashDeposit(new CashDepositGetOptions
{
    Id = 1
});

if (!cashDepositServiceResult.Succeeded)
{
    throw new Exception(cashDepositServiceResult.Error.Message);
}

return cashDepositServiceResult;
```

## Referans Numarasına Göre Nakit Para Yükleme İşlemine Erişim

Satıcı referans bilgileri ile birlikte fiziksel noktadan para yükleme işlemine ait bilgileri döndürür. Bu işlemi gerçekleştirmek için `CashDeposit` servisinde bulunan `GetCashDepositByReference` methodunu kullanın. `Reference` gönderillmelidir.

### CashDepositByReferenceOptions

`CashDepositByReferenceOptions` `CashDeposit` servisine istek parametrelerini sağlamak için kullanılır.

| **Değişken Adı** | **Tip** | **Açıklama**                                                 |
| ---------------- | ------- | ------------------------------------------------------------ |
| Reference        | string  | Nakit para yükleme işleminin referans numarasını alır veya belirler. Zorunlu parametredir. |

### Servis Methodu

#### Kullanım Amacı

Satıcının benzersiz referans numarasını kullanarak bir nakit para yükleme nesnesi döndürür.

| **Method**                | **Parametreler**              | **Geri Dönüş Tipi**             |
| ------------------------- | ----------------------------- | ------------------------------- |
| GetCashDepositByReference | CashDepositByReferenceOptions | PaparaSingleResult<CashDeposit> |

#### Kullanım Şekli

``` csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var cashDepositService = new CashDepositService(requestOptions);
var cashDepositServiceResult = cashDepositService.GetCashDepositByReference(new CashDepositByReferenceOptions
{
    Reference = "MERCHANT_REFERENCE_NO"
});

if (!cashDepositServiceResult.Succeeded)
{
    throw new Exception(cashDepositServiceResult.Error.Message);
}

return cashDepositServiceResult;
```

## Telefon Numarası ile Para Yükleme

Kullanıcının telefon numarasını kullanarak fiziksel noktadan kullanıcıya para yatırır. Bu işlemi gerçekleştirmek için `Cash Deposit` servisinde bulunan `CreateWithPhoneNumber` methodunu kullanın. `PhoneNumber`, `Amount` ve `MerchantReference` gönderilmelidir.

### CashDepositToPhoneOptions

`CashDepositToPhoneOptions` `CashDeposit` servisine istek parametrelerini sağlamak için kullanılır.

| **Değişken Adı**  | **Tip** | **Açıklama**                                                 |
| ----------------- | ------- | ------------------------------------------------------------ |
| PhoneNumber       | string  | Papara hesabına kayıtlı cep telefonu numarasını alır veya belirler. |
| Amount            | decimal | Yüklenecek para tutarını alır veya belirler. Bu tutar ödemeyi alan kullanıcının hesabına aktarılacaktır. Üye işyeri hesabından düşülecek tutar tam olarak bu sayı olacaktır. |
| MerchantReference | string  | Satıcı referans numarasını alır veya belirler. Nakit yükleme işlemlerinde yanlış tekrarları önlemek için üye işyeri tarafından gönderilen benzersiz değerdir. Kısasüre önce gönderilmiş ve başarılı bir merchantReference, yeni bir taleple yeniden gönderilirse, istek başarısız olur. Başarısız isteklerle gönderilen MerchantReference yeniden gönderilebilir. |

### Servis Methodu

#### Kullanım Amacı

Son kullanıcının telefon numarasını kullanarak nakit para yatırma isteği oluşturur.

| **Method**            | **Parametreler**          | **Geri Dönüş Tipi**             |
| --------------------- | ------------------------- | ------------------------------- |
| CreateWithPhoneNumber | CashDepositToPhoneOptions | PaparaSingleResult<CashDeposit> |

#### Kullanım Şekli

``` csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var cashDepositService = new CashDepositService(requestOptions);
var cashDepositServiceResult = cashDepositService.CreateWithPhoneNumber(new CashDepositToPhoneOptions
{
    Amount = 1,
    MerchantReference = "MERCHANT_REFERENCE_NO", 
    PhoneNumber = "TARGET_PHONE_NUMBER"
});

if (!cashDepositServiceResult.Succeeded)
{
    throw new Exception(cashDepositServiceResult.Error.Message);
}

return cashDepositServiceResult;
```

## Papara Numarası ile Para Yükleme

Fiziksel noktadan Papara numarası ile kullanıcıya para yatırır. Bu işlemi yapmak için  `Cash Deposit` servisinde bulunan `CreateWithAccountNumber` methodunu kullanın. `AccountNumber`, `Amount` ve `MerchantReference` gönderilmelidir.

### CashDepositToAccountNumberOptions

`CashDepositToAccountNumberOptions` `CashDeposit` servisine istek parametrelerini sağlamak için kullanılır.

| **Değişken Adı**  | **Tip** | **Açıklama**                                                 |
| ----------------- | ------- | ------------------------------------------------------------ |
| AccountNumber     | int     | Hesap numarasını alır veya belirler. Nakit yükleme yapılacak kullanıcının Papara hesap numarasıdır. |
| Amount            | decimal | Yüklenecek para tutarını alır veya belirler. Bu tutar ödemeyi alan kullanıcının hesabına aktarılacaktır. Üye işyeri hesabından düşülecek tutar tam olarak bu sayı olacaktır. |
| MerchantReference | string  | Satıcı referans numarasını alır veya belirler. Nakit yükleme işlemlerinde yanlış tekrarları önlemek için üye işyeri tarafından gönderilen benzersiz değerdir. Kısasüre önce gönderilmiş ve başarılı bir merchantReference, yeni bir taleple yeniden gönderilirse, istek başarısız olur. Başarısız isteklerle gönderilen MerchantReference yeniden gönderilebilir. |

### Servis Methodu

#### Kullanım Amacı

Son kullanıcının hesap numarasını kullanarak nakit para yükleme talebi oluşturur.

| **Method**              | **Parametreler**                  | **Geri Dönüş Tipi**             |
| ----------------------- | --------------------------------- | ------------------------------- |
| CreateWithAccountNumber | CashDepositToAccountNumberOptions | PaparaSingleResult<CashDeposit> |

#### Kullanım Şekli


```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var cashDepositService = new CashDepositService(requestOptions);
var cashDepositServiceResult = cashDepositService.CreateWithAccountNumber(new CashDepositToAccountNumberOptions
{
    Amount = 1,
    AccountNumber = 1,
    MerchantReference = "MERCHANT_REFERENCE_NO"
});

if (!cashDepositServiceResult.Succeeded)
{
    throw new Exception(cashDepositServiceResult.Error.Message);
}

return cashDepositServiceResult;
```

## TC Kimlik Numarası ile Para Yükleme

Fiziksel noktadan TCKN ile kullanıcıya para yatırır. Bu işlemi yapmak için  `Cash Deposit` servisinde bulunan `CreateWithTckn` methodunu kullanın. `Tckn`, `Amount` ve `MerchantReference` gönderilmelidir.

### CashDepositToTcknOptions

`CashDepositToTcknOptions` `CashDeposit` servisine istek parametrelerini sağlamak için kullanılır.

| **Değişken Adı**  | **Tip** | **Açıklama**                                                 |
| ----------------- | ------- | ------------------------------------------------------------ |
| Tckn              | long    | Nakit yükleme yapılacak kullanıcının TC kimlik numarasını alır veya belirler. |
| Amount            | decimal | Yüklenecek para tutarını alır veya belirler. Bu tutar ödemeyi alan kullanıcının hesabına aktarılacaktır. Üye işyeri hesabından düşülecek tutar tam olarak bu sayı olacaktır. |
| MerchantReference | string  | Satıcı referans numarasını alır veya belirler. Nakit yükleme işlemlerinde yanlış tekrarları önlemek için üye işyeri tarafından gönderilen benzersiz değerdir. Kısasüre önce gönderilmiş ve başarılı bir merchantReference, yeni bir taleple yeniden gönderilirse, istek başarısız olur. Başarısız isteklerle gönderilen MerchantReference yeniden gönderilebilir. |

### Servis Methodu

#### Kullanım Amacı

Son kullanıcının TC kimlik numarasını kullanarak nakit para yükleme talebi oluşturur.

| **Method**     | **Parametreler**         | **Geri Dönüş Tipi**             |
| -------------- | ------------------------ | ------------------------------- |
| CreateWithTckn | CashDepositToTcknOptions | PaparaSingleResult<CashDeposit> |

#### Kullanım Şekli

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var cashDepositService = new CashDepositService(requestOptions);
var cashDepositServiceResult = cashDepositService.CreateWithTckn(new CashDepositToTcknOptions
{
    Amount = 1,
    MerchantReference = "MERCHANT_REFERENCE_NO",
    Tckn = 12345678901
}); 
if (!cashDepositServiceResult.Succeeded)
{
    throw new Exception(cashDepositServiceResult.Error.Message);
}

return cashDepositServiceResult;
```

## TCKN ile Ön Ödemesiz Para Yükleme

Fiziksel noktadan TCKN ile kullanıcıya ön ödemesiz olarak para yatırır. Bu işlemi yapmak için  `Cash Deposit` servisinde bulunan `CreateProvisionWithTckn` methodunu kullanın. `Tckn`, `Amount` ve `MerchantReference` gönderilmelidir.

### CashDepositProvision Model

`CashDepositProvision` sınıfı `CashDeposit` servisi tarafından API'den dönen ön ödemesiz para yükleme bilgilerini eşleştirmek için kullanılır

| **Değişken Adı**  | **Tip**  | **Açıklama**                                                 |
| ----------------- | -------- | ------------------------------------------------------------ |
| Id                | int      | Ön ödemesiz para yükleme işleminin ID'sini alır veya belirler. |
| CreatedAt         | DateTime | Ön ödemesiz para yükleme işleminin oluşturulma tarihini alır veya belirler. |
| Amount            | decimal? | Ön ödemesiz para yükleme işleminin tutarını alır veya belirler. |
| Currency          | int      | Ön ödemesiz para yükleme işleminin para birimini alır veya belirler. |
| MerchantReference | string   | Satıcı referans numarasını alır veya belirler.               |
| UserFullName      | string   | Kullanıcının tam adını alır veya belirler.                   |

### CashDepositToTcknOptions

`CashDepositToTcknOptions` `CashDeposit` servisine istek parametrelerini sağlamak için kullanılır.

| **Değişken Adı**  | **Tip** | **Açıklama**                                                 |
| ----------------- | ------- | ------------------------------------------------------------ |
| Tckn              | int     | Nakit yükleme yapılacak kullanıcının TC kimlik numarasını alır veya belirler. |
| Amount            | decimal | Yüklenecek para tutarını alır veya belirler. Bu tutar ödemeyi alan kullanıcının hesabına aktarılacaktır. Üye işyeri hesabından düşülecek tutar tam olarak bu sayı olacaktır. |
| MerchantReference | string  | Satıcı referans numarasını alır veya belirler. Nakit yükleme işlemlerinde yanlış tekrarları önlemek için üye işyeri tarafından gönderilen benzersiz değerdir. Kısasüre önce gönderilmiş ve başarılı bir merchantReference, yeni bir taleple yeniden gönderilirse, istek başarısız olur. Başarısız isteklerle gönderilen MerchantReference yeniden gönderilebilir. |

### Servis Methodu

#### Kullanım Amacı

Son kullanıcının TC kimlik numarasını kullanarak ön yüklemesiz nakit para yükleme talebi oluşturur.

| **Method**              | **Parametreler**         | **Geri Dönüş Tipi**                      |
| ----------------------- | ------------------------ | ---------------------------------------- |
| CreateProvisionWithTckn | CashDepositToTcknOptions | PaparaSingleResult<CashDepositProvision> |

#### Kullanım Şekli

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var cashDepositService = new CashDepositService(requestOptions);
var cashDepositServiceResult = cashDepositService.CreateProvisionWithTckn(new CashDepositToTcknOptions
{
    Amount = 1,
    MerchantReference = "MERCHANT_REFERENCE_NO",
    Tckn = 12345678901
});

if (!cashDepositServiceResult.Succeeded)
{
    throw new Exception(cashDepositServiceResult.Error.Message);
}

return cashDepositServiceResult;
```

## Telefon Numarası ile Ön Ödemesiz Para Yükleme

Kullanıcının telefon numarasını kullanarak fiziksel noktadan kullanıcıya ön ödemesiz olark para yatırır. Bu işlemi gerçekleştirmek için `Cash Deposit` servisinde bulunan `CreateProvisionWithPhoneNumber` methodunu kullanın. `PhoneNumber`, `Amount` ve `MerchantReference` gönderilmelidir.

### Servis Methodu

#### Kullanım Amacı

Son kullanıcının telefon numarasını kullanarak ön ödemesiz nakit para yatırma isteği oluşturur.

| **Method**                     | **Parametreler**          | **Geri Dönüş Tipi**                      |
| ------------------------------ | ------------------------- | ---------------------------------------- |
| CreateProvisionWithPhoneNumber | CashDepositToPhoneOptions | PaparaSingleResult<CashDepositProvision> |

#### Kullanım Şekli

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var cashDepositService = new CashDepositService(requestOptions);
var cashDepositServiceResult = cashDepositService.CreateProvisionWithPhoneNumber(new CashDepositToPhoneOptions
{
    Amount = 1,
    MerchantReference = "MERCHANT_REFERENCE_NO",
    PhoneNumber = "PHONE_NUMBER"
});

if (!cashDepositServiceResult.Succeeded)
{
    throw new Exception(cashDepositServiceResult.Error.Message);
}

return cashDepositServiceResult;
```

## Papara Numarası ile Ön Ödemesiz Para Yükleme

Fiziksel noktadan Papara numarası ile kullanıcıya ön ödemesiz olarak para yatırır. Bu işlemi yapmak için  `Cash Deposit` servisinde bulunan `CreateProvisionWithAccountNumber` methodunu kullanın. `AccountNumber`, `Amount` ve `MerchantReference` gönderilmelidir.

### Servis Methodu

#### Kullanım Amacı

Son kullanıcının hesap numarasını kullanarak ön ödemesiz nakit para yatırma isteği oluşturur.

| **Method**                       | **Parametreler**                  | **Geri Dönüş Tipi**                      |
| -------------------------------- | --------------------------------- | ---------------------------------------- |
| CreateProvisionWithAccountNumber | CashDepositToAccountNumberOptions | PaparaSingleResult<CashDepositProvision> |

#### Kullanım Şekli

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var cashDepositService = new CashDepositService(requestOptions);
var cashDepositServiceResult = cashDepositService.CreateProvisionWithAccountNumber(new CashDepositToAccountNumberOptions
{
    Amount = 1,
    MerchantReference = "MERCHANT_REFERENCE_NO",
    AccountNumber = 1
});

if (!cashDepositServiceResult.Succeeded)
{
    throw new Exception(cashDepositServiceResult.Error.Message);
}

return cashDepositServiceResult;
```

## Referans Numarasına Göre Nakit Yükleme Onaylama 

Kullanıcı tarafından oluşturulan referans kodu ile fiziki noktadan ön ödemesiz nakit para yükleme talebini kontrol ederek onaylanmaya hazır hale getirir. Bu işlemi gerçekleştirmek için,  `Cash Deposit` servisinde bulunan `ProvisionByReferenceControl` methodunu kullanın. `referenceCode` ve `amount` gönderilmelidir.

### Servis Methodu

#### Kullanım Amacı

Ön ödemesiz nakit para yükleme talebini tamamlanmaya hazır hale getirir.

| **Method**                  | **Parametreler**          | **Geri Dönüş Tipi** |
| --------------------------- | ------------------------- | ------------------- |
| ProvisionByReferenceControl | CashDepositControlOptions | ServiceResult       |

#### Kullanım Şekli

```java
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var cashdepositControlOptions = new CashDepositControlOptions
{
    Amount = 10,
    ReferenceCode = "CASH_DEPOSIT_REFERENCE_NUMBER"
};

var result = paparaClient.CashDepositService.ProvisionByReferenceControl(cashdepositControlOptions);

return result;
```

## Referans Numarasına Göre Nakit Yükleme İşlemini Tamamlama

Kullanıcı tarafından oluşturulan referans kodu ile fiziki noktadan ön ödemesiz nakit para yükleme talebini onaylar ve bakiyeyi kullanıcıya aktarır. Bu işlemi gerçekleştirmek için `CashDeposit` servisinde bulunan `completeProvisionByReference` methodunu kullanın. `referenceCode` ve `amount` gönderilmelidir.

### Servis Methodu

#### Kullanım Amacı

Ön ödemesiz nakit yükleme işlemini tamamlar

| **Method**                   | **Parametreler**          | **Geri Dönüş Tipi** |
| ---------------------------- | ------------------------- | ------------------- |
| CompleteProvisionByReference | CashDepositControlOptions | ServiceResult       |

#### Kullanım Şekli

```java
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var cashdepositControlOptions = new CashDepositControlOptions
{
    Amount = 10,
    ReferenceCode = "CASH_DEPOSIT_REFERENCE_NUMBER"
};

var result = paparaClient.CashDepositService.CompleteProvisionByReference(cashdepositControlOptions);

return result;
```



## Nakit Yükleme İşlemini Tamamlama

Bekleyen para yükleme işlemlerini tamamlamak için kullanılır. Kullanıcının hesabına paranın geçmesi için işlemin tamamlanması gerekir. Bu işlemi gerçekleştirmek için `CashDeposit` servisinde bulunan `completeProvisionByReference` methodunu kullanın. `Id` ve `TransactionDate` gönderilmelidir.

### CashDepositCompleteOptions

`CashDepositCompleteOptions` `CashDeposit` servisine istek parametrelerini sağlamak için kullanılır.

| **Değişken Adı** | **Tip**  | **Açıklama**                                                 |
| ---------------- | -------- | ------------------------------------------------------------ |
| Id               | int      | Ön ödemesiz nakit yükleme işleminin ID'sini alır veya belirler |
| TransactionDate  | DateTime | Ön ödemesiz nakit yükleme işleminin işlem tarihini alır veya belirler |

### Servis Methodu

#### Kullanım Amacı

Bekleyen ön ödemesiz para yükleme işlemlerini tamamlamak için kullanılır.

| **Method**        | **Parametreler**           | **Geri Dönüş Tipi**             |
| ----------------- | -------------------------- | ------------------------------- |
| CompleteProvision | CashDepositCompleteOptions | PaparaSingleResult<CashDeposit> |

#### Kullanım Şekli

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var cashDepositService = new CashDepositService(requestOptions);
var cashDepositServiceResult = cashDepositService.CompleteProvision(new CashDepositCompleteOptions
{
    Id = 1,
    TransactionDate = DateTime.Now
});

if (!cashDepositServiceResult.Succeeded)
{
    throw new Exception(cashDepositServiceResult.Error.Message);
}

return cashDepositServiceResult;
```

## Tarihe Göre Nakit Para Yükleme Bilgilerine Erişim

Para yatırma bilgilerini tarihe göre getirir. Bu işlemi gerçekleştirmek için, `Cash Deposit` bulunan`GetCashDepositByDate` methodunu kullanın. `StartDate`, `EndDate`, `PageIndex` ve `PageItemCount` gönderilmelidir.

### CashDepositByDateOptions

`CashDepositByDateOptions` `CashDeposit` servisine istek parametrelerini sağlamak için kullanılır.

| **Değişken Adı** | **Tip**  | **Açıklama**                                                 |
| ---------------- | -------- | ------------------------------------------------------------ |
| StartDate        | DateTime | Nakit para yükleme işlemlerinin başlangıç tarihini alır veya belirler. |
| EndDate          | DateTime | Nakit para yükleme işlemlerinin bitiş tarihini alır veya belirler. |
| PageIndex        | int      | Sayfa dizinini alır veya belirler. Bir sayfada gösterilmek istenen kayıt sayısına (pageItemCount) göre hesaplanan sayfalardan gösterilmek istenen sayfanın indeks numarasıdır. Not: ilk sayfa her zaman 1'dir |
| PageItemCount    | int      | Sayfa öğesi sayısını alır veya belirler. Bir sayfada gösterilmesi istenen kayıtların sayısıdir. |

### Servis Methodu

#### Kullanım Amacı

Verilen tarihler aralığındaki nakit para yükleme işlemlerine erişim için kullanılır

| **Method**           | **Parametreler**         | **Geri Dönüş Tipi**            |
| -------------------- | ------------------------ | ------------------------------ |
| GetCashDepositByDate | CashDepositByDateOptions | PaparaArrayResult<CashDeposit> |

#### Kullanım Şekli

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var cashDepositService = new CashDepositService(requestOptions);
var cashDepositServiceResult = cashDepositService.GetCashDepositByDate(new CashDepositByDateOptions
{
    StartDate = DateTime.Now.AddDays(-10),
    EndDate = DateTime.Now,
    PageIndex = 1,
    PageItemCount = 20
});

if (!cashDepositServiceResult.Succeeded)
{
    throw new Exception(cashDepositServiceResult.Error.Message);
}

return cashDepositServiceResult;
```

## Ön Ödemesiz İşlemler için Mutabakat

Verilen tarihlerde gerçekleştirilen ön ödemesiz para yükleme işlemlerin toplam sayısını ve hacmini döndürür. Hesaplamaya hem başlangıç hem de bitiş tarihleri dahil edilir. Bu işlemi gerçekleştirmek için, `Cash Deposit`  servisinde bulunan`ProvisionSettlements` methodunu kullanın. `StartDate` ve `EndDate` gönderilmelidir.

### CashDepositSettlementOptions

`CashDepositSettlementOptions` `CashDeposit` servisine istek parametrelerini sağlamak için kullanılır.

| **Değişken Adı** | **Tip**    | **Açıklama**                                      |
| ---------------- | ---------- | ------------------------------------------------- |
| StartDate        | DateTime   | Mutabakatın başlangıç tarihini alır veya belirler |
| EndDate          | DateTime   | Mutabakatın bitiş tarihini alır veya belirler     |
| EntryType        | EntryType? | Mutabakatın giriş tipini alır veya belirler       |

### Servis Methodu

#### Kullanım Amacı

Verilen tarihler arasındaki toplam ön ödemesiz nakit para yükleme işlem hacmini ve sayımı döndürür. Başlangıç ve bitiş tarihleri dahildir.

| **Method**           | **Parametreler**             | **Geri Dönüş Tipi**                       |
| -------------------- | ---------------------------- | ----------------------------------------- |
| ProvisionSettlements | CashDepositSettlementOptions | PaparaSingleResult<CashDepositSettlement> |

#### Kullanım Şekli

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var cashDepositService = new CashDepositService(requestOptions);
var cashDepositServiceResult = cashDepositService.ProvisionSettlements(new CashDepositSettlementOptions
{
    StartDate = DateTime.Now.AddDays(-10),
    EndDate = DateTime.Now,
    EntryType = EntryType.BankTransfer
});

if (!cashDepositServiceResult.Succeeded)
{
    throw new Exception(cashDepositServiceResult.Error.Message);
}

return cashDepositServiceResult;
```

## Mutabakatlar

Verilen tarihlerde gerçekleştirilen para yükleme işlemlerinin toplam sayısını ve hacmini döndürür. Hesaplamaya hem başlangıç hem de bitiş tarihleri dahil edilir. Bu işlemi gerçekleştirmek için, `Cash Deposit`  servisinde bulunan`Settlements` methodunu kullanın. `StartDate` ve `EndDate` gönderilmelidir.

### CashDepositSettlementOptions

`CashDepositSettlementOptions` `CashDeposit` servisine istek parametrelerini sağlamak için kullanılır.

| **Değişken Adı** | **Tip**    | **Açıklama**                                         |
| ---------------- | ---------- | ---------------------------------------------------- |
| StartDate        | DateTime   | Mutabakatın başlangıç tarihini alır veya belirler    |
| EndDate          | DateTime   | Mutabakatın bitiş tarihini alır veya belirler        |
| EntryType        | EntryType? | Mutabakatın giriş tipini tarihini alır veya belirler |

### Servis Methodu

#### Kullanım Amacı

Verilen tarihler arasındaki toplam nakit para yükleme işlem hacmini ve sayımı döndürür. Başlangıç ve bitiş tarihleri dahildir.

| **Method**           | **Parametreler**             | **Geri Dönüş Tipi**                       |
| -------------------- | ---------------------------- | ----------------------------------------- |
| ProvisionSettlements | CashDepositSettlementOptions | PaparaSingleResult<CashDepositSettlement> |

#### Kullanım Şekli

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var cashDepositService = new CashDepositService(requestOptions);
var cashDepositServiceResult = cashDepositService.Settlements(new CashDepositSettlementOptions
{
    StartDate = DateTime.Now.AddDays(-10),
    EndDate = DateTime.Now,
    EntryType = EntryType.BankTransfer
});

if (!cashDepositServiceResult.Succeeded)
{
    throw new Exception(cashDepositServiceResult.Error.Message);
}

return cashDepositServiceResult;
```

## Olası Hatalar ve Hata Kodları

| **Hata Kodu** | **Hata Açıklaması**                                          |
| ------------- | ------------------------------------------------------------ |
| 100           | Kullanıcı bulunamadı.                                        |
| 101           | Satıcı bilgisi bulunamadı.                                   |
| 105           | Yetersiz bakiye.                                             |
| 107           | Kullanıcı bu işlem ile toplam işlem limitini aşıyor.         |
| 111           | Kullanıcı bu işlem ile aylık toplam işlem limitini aşıyor.   |
| 112           | Gönderilen tutar minimum gönderim tutarının altında.         |
| 203           | Kullanıcı hesabı blokeli.                                    |
| 997           | Nakit para yatırma yetkisi, hesabınızda tanımlanmamıştır. Müşteri temsilcinizle iletişime geçmelisiniz. |
| 998           | Gönderdiğiniz parametreler beklenen formatta değil. Örnek: zorunlu alanlardan biri sağlanmamıştır. |
| 999           | Papara sisteminde hata meydana geldi.                        |



# <a name="mass-payment">Ödeme Dağıtma</a> 

Bu bölüm, ödemelerini kullanıcılarına hızlı, güvenli ve yaygın bir şekilde Papara üzerinden dağıtmak isteyen işyerleri için hazırlanmış teknik entegrasyon bilgilerini içerir.

## Ödeme Dağıtım Bilgilerine Erişim

Ödeme dağıtım işlemi hakkında bilgileri döner. Bu işlemi yamak için `MassPayment` servisinde bulunan `GetMassPayment` methodunu kullanın. `Id` gönderilmelidir.

### Mass Payment Model

`MassPayment` sınıfı, `MassPayment` servisi tarafından API'den dönen ödeme dağıtım bilgilerini eşleştirmek için kullanılır.

| **Değişken Adı** | **Tip**  | **Açıklama**                                                 |
| ---------------- | -------- | ------------------------------------------------------------ |
| MassPaymentId    | string   | Ödeme ID'sini alır veya belirler.                            |
| Id               | int?     | Ödeme yapıldıktan sonra oluşan ID'yi alır veya belirler.     |
| CreatedAt        | DateTime | Ödeme tarihini alır veya belirler.                           |
| Amount           | decimal? | Ödeme tutarını alır veya belirler.                           |
| Currency         | int?     | Ödeme yapılan para birmini alır veya belirler. Değerler "1","2" veya "3" olabilir. |
| Fee              | decimal? | Hizmet bedelini alır veya belirler.                          |
| ResultingBalance | decimal? | Kalan bakiyeyi alır veya belirler.                           |
| Description      | string   | Açıklamayı alır veya belirler.                               |

### MassPaymentGetOptions

`MassPaymentGetOptions` `MassPayment` servisine istek parametrelerini sağlamak için kullanılır.

| **Değişken Adı** | **Tip** | **Açıklama**                      |
| ---------------- | ------- | --------------------------------- |
| Id               | long    | Ödeme ID'sini alır veya belirler. |

### Servis Methodu

#### Kullanım Amacı

Yetkili satıcı için ödeme dağıtım bilgisine erişmek için kullanılır

| **Method**     | **Parametreler**      | **Geri Dönüş Tipi**             |
| -------------- | --------------------- | ------------------------------- |
| GetMassPayment | MassPaymentGetOptions | PaparaSingleResult<MassPayment> |

#### Kullanım Şekli

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var massPaymentService = new MassPaymentService(requestOptions);
var massPaymentServiceResult = massPaymentService.GetMassPayment(new MassPaymentGetOptions
{
    Id = 1
});

if (!massPaymentServiceResult.Succeeded)
{
    throw new Exception(massPaymentServiceResult.Error.Message);
}

return massPaymentServiceResult;
```

## Referans Numarasına Göre Ödeme Dağıtım Bilgilerine Erişim

Referans numarası kullanarak ödeme dağıtım süreci hakkında bilgi verir. Bu işlemi gerçekleştirmek için `MassPayment` servisinde bulunan `GetMassPaymentByReference` methodunu kullanın. `Reference` gönderilmelidir.

### MassPaymentByReferenceOptions

`MassPaymentByIdOptions` `MassPayment` servisine istek parametrelerini sağlamak için kullanılır.

| **Değişken Adı** | **Tip** | **Açıklama**                                           |
| ---------------- | ------- | ------------------------------------------------------ |
| Reference        | String  | Ödeme işleminin referans numarasını alır veya belirler |

### Servis Methodu

#### Kullanım Amacı

Yetkili satıcı için ödeme bilgisine erişmek için kullanılır

| **Method**                | **Parametreler**              | **Geri Dönüş Tipi**             |
| ------------------------- | ----------------------------- | ------------------------------- |
| GetMassPaymentByReference | massPaymentByReferenceOptions | PaparaSingleResult<MassPayment> |

#### Kullanım Şekli

```java
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var masspaymentOptions = new MassPaymentByReferenceOptions
{
    Reference = result.Data.MassPaymentId
};

var massPaymentServiceResult = paparaClient.MassPaymentService.GetMassPaymentByReference(masspaymentOptions);

if (!massPaymentServiceResult.Succeeded)
{
    throw new Exception(massPaymentServiceResult.Error.Message);
}

return massPaymentServiceResult;
```

## Hesap Numarasına Ödeme Gönderme

Papara numarasına para gönderin. Bu işlemi gerçekleştirmek için `MassPayment` servisinde bulunan `PostMassPayment` methodunu kullanın. `AccountNumber`, `Amount` ve `MassPaymentId` gönderilmelidir.

### MassPaymentToPaparaNumberOptions

`MassPaymentToPaparaNumberOptions` `MassPayment` servisine istek parametrelerini sağlamak için kullanılır.

| **Değişken Adı**   | **Tip**  | **Açıklama**                                                 |
| ------------------ | -------- | ------------------------------------------------------------ |
| AccountNumber      | string   | Papara hesap numarasını alır veya belirler. Ödemeyi alacak kullanıcının 10 haneli Papara numarası. 1234567890 veya PL1234567890 biçiminde olabilir. Papara sürüm geçişinden önce Papara numarasına cüzdan numarası deniyordu, eski cüzdan numaraları Papara numarası olarak değiştirildi. Ödeme eski cüzdan numaralarına dağıtılabilir. |
| ParseAccountNumber | int?     | Ayrıştırma hesap numarasını alır veya belirler. Hesap numarasını long tip olarak ayrıştırır. Eski papara entegrasyonlarında PL ile başlanarak hesap / cüzdan numarası yapılıyordu. Hizmet, kullanıcılarından papara numarasını alan üye işyerlerine sorun yaşatmaması için PL ile başlayan numaraları kabul edecek şekilde yazılmıştır. |
| Amount             | decimal? | Miktarı alır veya belirler. Ödeme işleminin tutarıdır. Bu tutar ödemeyi alan kullanıcının hesabına aktarılacaktır. Bu rakam artı işlem ücreti üye işyeri hesabından tahsil edilecektir. |
| MassPaymentId      | string   | Ödeme ID'sini alır veya belirler. Ödeme işlemlerinde hatalı tekrarları önlemek için üye işyeri tarafından gönderilen benzersiz değerdir. Kısa süre önce gönderilmiş ve başarılı olan bir massPaymentId yeni bir taleple tekrar gönderilirse, istek başarısız olur. |
| TurkishNationalId  | long     | TC kimlik numarasını alır veya belirler. Ödemeyi alacak kullanıcının gönderdiği kimlik bilgilerinin Papara sisteminde kontrolünü sağlar. Kimlik bilgilerinde bir çelişki olması durumunda işlem gerçekleşmeyecektir. |
| Description        | string   | Açıklamayı alır veya ayarlar. Satıcı tarafından sağlanan işlemin açıklamasıdır. Zorunlu bir alan değildir. Gönderilirse işlem açıklamalarında alıcı tarafından görülür. |

### Servis Methodu

#### Kullanım Amacı

Yetkili satıcı için verilen hesap numarasına ödeme göndermek için kullanılır

| **Method**      | **Parametreler**                 | **Geri Dönüş Tipi**             |
| --------------- | -------------------------------- | ------------------------------- |
| PostMassPayment | MassPaymentToPaparaNumberOptions | PaparaSingleResult<MassPayment> |

#### Kullanım Şekli

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var massPaymentService = new MassPaymentService(requestOptions);
var massPaymentServiceResult = massPaymentService.PostMassPayment(new MassPaymentToPaparaNumberOptions
{
    AccountNumber = "ACCOUNT_NUMBER",
    Amount = 1,
    Description = "test",
    MassPaymentId = "MASS_PAYMENT_ID",
    ParseAccountNumber = 1,
    TurkishNationalId = 12345678901
});

if (!massPaymentServiceResult.Succeeded)
{
    throw new Exception(massPaymentServiceResult.Error.Message);
}

return massPaymentServiceResult;
```

## E-Posta Adresine Ödeme Gönderme

Papara'da kayıtlı e-posta adresine para gönderin. Bu işlemi gerçekleştirmek için `MassPayment` servisinde bulunan `PostMassPaymentToEmail` methodunu kullanın. `Email`, `Amount` ve `MassPaymentId` gönderilmelidir.

### MassPaymentToEmailOptions

`MassPaymentToEmailOptions` `MassPayment` servisine istek parametrelerini sağlamak için kullanılır.

| **Değişken Adı**  | **Tip**  | **Açıklama**                                                 |
| ----------------- | -------- | ------------------------------------------------------------ |
| Email             | string   | Hedef e-posta adresini alır veya belirler.                   |
| Amount            | decimal? | Miktarı alır veya belirler. Ödeme işleminin tutarıdır. Bu tutar ödemeyi alan kullanıcının hesabına aktarılacaktır. Bu rakam artı işlem ücreti üye işyeri hesabından tahsil edilecektir |
| MassPaymentId     | string   | Ödeme ID'sini alır veya belirler. Ödeme işlemlerinde hatalı tekrarları önlemek için üye işyeri tarafından gönderilen benzersiz değerdir. Kısa süre önce gönderilmiş ve başarılı olan bir massPaymentId yeni bir taleple tekrar gönderilirse, istek başarısız olur. |
| TurkishNationalId | long     | TC kimlik numarasını alır veya belirler. Ödemeyi alacak kullanıcının gönderdiği kimlik bilgilerinin Papara sisteminde kontrolünü sağlar. Kimlik bilgilerinde bir çelişki olması durumunda işlem gerçekleşmeyecektir. |
| Description       | string   | Açıklamayı alır veya ayarlar. Satıcı tarafından sağlanan işlemin açıklamasıdır. Zorunlu bir alan değildir. Gönderilirse işlem açıklamalarında alıcı tarafından görülür. |

### Servis Methodu

#### Kullanım Amacı

Yetkili satıcı için verilen e-posta adresine ödeme göndermek için kullanılır

| **Method**             | **Parametreler**          | **Geri Dönüş Tipi**             |
| ---------------------- | ------------------------- | ------------------------------- |
| PostMassPaymentToEmail | MassPaymentToEmailOptions | PaparaSingleResult<MassPayment> |

#### Kullanım Şekli

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var massPaymentService = new MassPaymentService(requestOptions);
var massPaymentServiceResult = massPaymentService.PostMassPaymentToEmail(new MassPaymentToEmailOptions
{
    Amount = 1,
    Description = "test",
    Email = "test@test.com",
    MassPaymentId = "MASS_PAYMENT_ID",
    TurkishNationalId = 12345678901
});

if (!massPaymentServiceResult.Succeeded)
{
    throw new Exception(massPaymentServiceResult.Error.Message);
}

return massPaymentServiceResult;
```

## Telefon Numarasına Ödeme Gönderme

Papara'da kayıtlı telefon numarasına para gönderin. Bu işlemi gerçekleştirmek için `MassPayment` servisinde bulunan `PostMassPaymentToPhone` methodunu kullanın. `PhoneNumber`, `Amount` ve `MassPaymentId` gönderilmelidir.

### MassPaymentToPhoneNumberOptions

`MassPaymentToPhoneNumberOptions` `MassPayment` servisine istek parametrelerini sağlamak için kullanılır.

| **Değişken Adı**  | **Tip**  | **Açıklama**                                                 |
| ----------------- | -------- | ------------------------------------------------------------ |
| PhoneNumber       | string   | Kullanıcının telefon numarasını alır veya belirler. Ödemeyi alacak kullanıcının Papara'da kayıtlı cep telefonu numarasıdır. Bir ülke kodu içermeli ve + ile başlamalıdır. |
| Amount            | decimal? | Miktarı alır veya belirler. Ödeme işleminin tutarıdır. Bu tutar ödemeyi alan kullanıcının hesabına aktarılacaktır. Bu rakam artı işlem ücreti üye işyeri hesabından tahsil edilecektir |
| MassPaymentId     | string   | Ödeme ID'sini alır veya belirler. Ödeme işlemlerinde hatalı tekrarları önlemek için üye işyeri tarafından gönderilen benzersiz değerdir. Kısa süre önce gönderilmiş ve başarılı olan bir massPaymentId yeni bir taleple tekrar gönderilirse, istek başarısız olur. |
| TurkishNationalId | long     | TC kimlik numarasını alır veya belirler. Ödemeyi alacak kullanıcının gönderdiği kimlik bilgilerinin Papara sisteminde kontrolünü sağlar. Kimlik bilgilerinde bir çelişki olması durumunda işlem gerçekleşmeyecektir. |
| Description       | string   | Açıklamayı alır veya ayarlar. Satıcı tarafından sağlanan işlemin açıklamasıdır. Zorunlu bir alan değildir. Gönderilirse işlem açıklamalarında alıcı tarafından görülür. |

### Servis Methodu

#### Kullanım Amacı

Yetkili satıcı için verilen telefon numarasına ödeme göndermek için kullanılır

| **Method**             | **Parametreler**                | **Geri Dönüş Tipi**             |
| ---------------------- | ------------------------------- | ------------------------------- |
| PostMassPaymentToPhone | MassPaymentToPhoneNumberOptions | PaparaSingleResult<MassPayment> |

#### Kullanım Şekli

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var massPaymentService = new MassPaymentService(requestOptions);
var massPaymentServiceResult = massPaymentService.PostMassPaymentToPhone(new MassPaymentToPhoneNumberOptions
{
    Amount = 1,
    Description = "test",
    MassPaymentId = "MASS_PAYMENT_ID",
    PhoneNumber = "PHONE_NUMBER",
    TurkishNationalId = 12345678901
});

if (!massPaymentServiceResult.Succeeded)
{
    throw new Exception(massPaymentServiceResult.Error.Message);
}

return massPaymentServiceResult;
```

## Olası Hatalar ve Hata Kodları

| **Hata Kodu** | **Hata Açıklaması**                                          |
| ------------- | ------------------------------------------------------------ |
| 100           | Kullanıcı bulunamadı                                         |
| 105           | Yetersiz bakiye                                              |
| 107           | Alıcı bakiye limitini aşıyor. Basit hesaplar için mümkün olan en yüksek bakiye 750 TL'dir. |
| 111           | Alıcı aylık işlem limitini aşıyor. Basit hesaplar tanımlı kaynaktan aylık toplam 2000 TL ödeme alabilir. |
| 133           | MassPaymentID yakın zamanda kullanıldı.                      |
| 997           | Ödemeleri dağıtma yetkiniz yok. Müşteri temsilcinizle iletişime geçebilir ve satıcı hesabınıza bir ödeme dağıtım tanımı talep edebilirsiniz. |
| 998           | Gönderdiğiniz parametreler beklenen formatta değil. Örnek: Müşteri numarası 10 haneden az. Bu durumda, hata mesajı format hatasının ayrıntılarını içerir. |
| 999           | Papara sisteminde bir hata oluştu.                           |



# <a name="payments">Ödeme Alma</a> 

Ödeme alma, oluşturma veya listeleme ve geri ödeme için ödeme hizmeti kullanılacaktır. Ödeme butonunu kullanıcılara göstermeden önce üye işyeri Papara'da bir ödeme işlemi oluşturmalıdır. Ödeme kayıtları zamana bağlıdır. Son kullanıcı tarafından tamamlanmayan ve ödenmeyen işlem kayıtları 1 saat sonra Papara sisteminden silinir. Tamamlanan ödeme kayıtları asla silinmez ve her zaman API ile sorgulanabilir.

## Ödeme Bilgilerine Erişim

Ödeme bilgilerini döndürür. Bu işlemi gerçekleştirmek için `Payment` servisinde bulunan `GetPayment` methodunu kullanın. `Id` gönderilmelidir.

### Payment Model

`Payment` sınıfı, `Payment` servisi tarafından API'den dönen ödeme değerlerini eşleştirmek için kullanılır.

| **Değişken Adı**         | **Tip**  | **Açıklama**                                                 |
| ------------------------ | -------- | ------------------------------------------------------------ |
| Merchant                 | Account  | Satıcıyı alır veya belirler                                  |
| Id                       | string   | ID'yi alır veya belirler                                     |
| CreatedAt                | DateTime | Ödemenin oluşturulma tarihini alır veya belirler             |
| MerchantId               | string   | Satıcı ID'sini alır veya belirler                            |
| UserId                   | string   | Kullanıcı ID'sini alır veya belirler                         |
| PaymentMethod            | int?     | Ödeme Yöntemini alır veya belirler. <br />0 - Kullanıcı, mevcut Papara bakiyesiyle işlemi tamamladı <br />1 - Kullanıcı, işlemi daha önce tanımlanmış bir banka / kredi kartı ile tamamladı. <br />2 - Kullanıcı, mobil ödeme yoluyla işlemi tamamladı. |
| PaymentMethodDescription | string   | Ödeme yöntemi açıklamasını alır veya belirler.               |
| ReferenceId              | string   | Referans numarasını alır veya belirler.                      |
| OrderDescription         | string   | Sipariş açıklamasını alır veya belirler.                     |
| Status                   | int?     | Ödeme durumunu alır veya belirler.<br /> 0 - Bekleniyor, ödeme henüz yapılmadı. <br />1 - Ödeme yapıldı, işlem tamamlandı. 2 - İşlemler üye işyeri tarafından iade edildi. |
| StatusDescription        | string   | Ödeme durumu açıklamasını alır veya belirler                 |
| Amount                   | decimal? | Ödeme tutarını alır veya belirler                            |
| Fee                      | decimal? | Ödeme hizmet bedelini alır veya belirler                     |
| Currency                 | int?     | Ödeme yapılacak para birimini alır veya belirler. Değerler “0”,  “1”, “2” veya  “3” olabilir. |
| NotificationUrl          | string   | Bildirim URL'ini alır veya belirler.                         |
| NotificationDone         | bool?    | Bildirimin yapılıp yapılmadığını alır veya belirler.         |
| RedirectUrl              | string   | Yönlendirme URL'ini alır veya belirler.                      |
| PaymentUrl               | string   | Ödeme URL'ini alır veya belirler.                            |
| MerchantSecretKey        | string   | Satıcı gizli anahtarını alır veya belirler.                  |
| ReturningRedirectUrl     | string   | Geri dönen yönlendirme URL'ini alır veya belirler.           |
| TurkishNationalId        | long     | TC kimlik numarasını alır veya belirler.                     |

### PaymentGetOptions

`PaymentGetOptions` ödeme bilgilerine ulaşırken parametre olarak kullanılır

| **Değişken Adı** | **Tip** | **Açıklama**                               |
| ---------------- | ------- | ------------------------------------------ |
| Id               | string  | Benzersiz ödeme ID'sini alır veya belirler |

### Servis Methodu

#### Kullanım Amacı

Ödeme ve bakiye bilgilerine erişmek için kullanılır

| **Method** | **Parametreler**  | **Geri Dönüş Tipi**         |
| ---------- | ----------------- | --------------------------- |
| GetPayment | PaymentGetOptions | PaparaSingleResult<Payment> |

#### Kullanım Şekli

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var paymentService = new PaymentService(requestOptions);
var paymentServiceResult = paymentService.GetPayment(new PaymentGetOptions
{
    Id = "1"
});

if (!paymentServiceResult.Succeeded)
{
    throw new Exception(paymentServiceResult.Error.Message);
}

return paymentServiceResult;
```

## Referans Numarasına Göre Ödeme Bilgilerine Erişim

Ödeme bilgilerini döndürür. Bu işlemi gerçekleştirmek için `Payment` servisinde bulunan `GetPaymentByReference` methodunu kullanın. `ReferenceId` gönderilmelidir.

### PaymentByReferenceOptions

`PaymentGetOptions` ödeme bilgilerine ulaşırken parametre olarak kullanılır

| **Değişken Adı** | **Tip** | **Açıklama**                                 |
| ---------------- | ------- | -------------------------------------------- |
| ReferenceId      | string  | Ödeme referans numarasını alır veya belirler |

### Servis Methodu

#### Kullanım Amacı

Yetkili satıcı için ödeme ve bakiye bilgilerine erişmek istenildiğinde kullanılır.

| **Method**            | **Parametreler**          | **Geri Dönüş Tipi**         |
| --------------------- | ------------------------- | --------------------------- |
| GetPaymentByReference | PaymentByReferenceOptions | PaparaSingleResult<Payment> |

#### Kullanım Şekli

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var paymentService = new PaymentService(requestOptions);
var paymentServiceResult = paymentService.GetPaymentByReference(new PaymentByReferenceOptions
{
    ReferenceId = "PAYMENT_REFERENCE"
});

if (!paymentServiceResult.Succeeded)
{
    throw new Exception(paymentServiceResult.Error.Message);
}

return paymentServiceResult;
```

## Ödeme Oluşturma

Yeni bir ödeme kaydı oluşturur. Bu işlemi gerçekleştirmek için `Payment` servisinde bulunan `CreatePayment`  methodunu kullanın. `Amount`, `ReferenceId`, `OrderDescription`, `NotificationUrl` ve `RedirectUrl` sağlanmalıdır.

### PaymentCreateOptions

`PaymentCreateOptions` ödeme oluştururken parametre olarak kullanılır

| **Değişken Adı**  | **Tip** | **Açıklama**                                                 |
| ----------------- | ------- | ------------------------------------------------------------ |
| Amount            | decimal | Ödeme yapılacak miktarı alır veya belirler. Ödeme işleminin tutarı. Tam olarak bu tutar ödemeyi yapan kullanıcının hesabından alınacak ve bu tutar ödeme ekranında kullanıcıya gösterilecektir. Miktar alanı minimum 1.00, maksimum 500000.00 olabilir |
| ReferenceId       | string  | Referans ID'sini alır veya belirler. Üye işyeri sistemindeki ödeme işleminin referans bilgileridir. İşlem, Papara'ya gönderildiği gibi sonuç bildirimlerinde değiştirilmeden üye işyerine iade edilecektir. 100 karakterden fazla olmamalıdır. Bu alanın benzersiz olması gerekmez ve Papara böyle bir kontrol yapmaz |
| OrderDescription  | string  | Sipariş açıklamasını alır veya belirler. Ödeme işleminin açıklamasıdır. Gönderilen bilgi, Papara ödeme sayfasında kullanıcıya gösterilecektir. Kullanıcı tarafından başlatılan işlemi doğru bir şekilde bildiren bir tanıma sahip olmak, başarılı ödeme şansını artıracaktır. |
| NotificationUrl   | string  | Bildirim URL'sini alır veya belirler. Ödeme bildirim isteklerinin (IPN) gönderileceği URL'dir.  "NotificationUrl" ile gönderilen URL'ye Papara, ödeme tamamlandıktan hemen sonra bir HTTP POST isteği ile ödemenin tüm bilgilerini içeren bir ödeme nesnesi gönderecektir. Üye işyeri bu talebe 200 OK döndürürse tekrar bildirim yapılmayacaktır. Üye işyeri bu bildirime 200 OK dönmezse, Papara, üye işyeri 200 OK'e dönene kadar 24 saat boyunca ödeme bildirimi (IPN) talepleri yapmaya devam edecektir. |
| RedirectUrl       | string  | Yönlendirme URL'sini alır veya belirler. İşlemin sonunda kullanıcının yönlendirileceği URL |
| TurkishNationalId | long    | TC kimlik numarasını alır veya belirler. Ödemeyi alacak kullanıcının gönderdiği kimlik bilgilerinin Papara sisteminde kontrolünü sağlar. Kimlik bilgilerinde bir çelişki olması durumunda işlem gerçekleşmeyecektir. |

### Servis Methodu

#### Kullanım Amacı

Ödeme oluşturmak için kullanılacaktır.

| **Method**    | **Parametreler**     | **Geri Dönüş Tipi**         |
| ------------- | -------------------- | --------------------------- |
| CreatePayment | PaymentCreateOptions | PaparaSingleResult<Payment> |

#### Kullanım Şekli

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var paymentService = new PaymentService(requestOptions);
var paymentServiceResult = paymentService.CreatePayment(new PaymentCreateOptions
{
    Amount = 1,
    NotificationUrl = "NOTIFICATION_URL",
    OrderDescription = "Test",
    RedirectUrl = "REDIRECT_URL",
    ReferenceId = "123",
    TurkishNationalId = 12345678901
});

if (!paymentServiceResult.Succeeded)
{
    throw new Exception(paymentServiceResult.Error.Message);
}

return paymentServiceResult;
```

## İade İşlemi

Satıcının ödeme ID'siyle tamamlanmış bir ödemesini iade etmesini sağlar. Bu işlemi gerçekleştirmek için `Payment` servisinde bulunan `Refund` yöntemini kullanın. `Id` gönderilmelidir.

### PaymentRefundOptions

`PaymentRefundOptions` iade oluştururken parametre olarak kullanılır.

| **Değişken Adı** | **Tip** | **Açıklama**                     |
| ---------------- | ------- | -------------------------------- |
| Id               | string  | Ödeme ID'sini alır veya belirler |

### Servis Methodu

#### Kullanım Amacı

Yetkili satıcı için bir ödemenin iade edileceği durumlarda kullanılır.

| **Method** | **Parametreler**     | **Geri Dönüş Tipi** |
| ---------- | -------------------- | ------------------- |
| Refund     | PaymentRefundOptions | PaparaServiceResult |

#### Kullanım Şekli

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var paymentService = new PaymentService(requestOptions);
var paymentServiceResult = paymentService.Refund(new PaymentRefundOptions
{
    Id = "PAYMENT_ID"
});

if (!paymentServiceResult.Succeeded)
{
    throw new Exception(paymentServiceResult.Error.Message);
}

return paymentServiceResult;
```

## Ödemeleri Listeleme

Satıcının tamamlanan ödemelerini sıralı bir şekilde listeler. Bu işlemi gerçekleştirmek için `Payment` servisinde buluan `List` methodunu kullanın. `PageIndex`ve `PageItemCount ` gönderilmelidir.

### PaymentListItem

`PaymentListItem` sınıfı `Payment` servisi tarafından API'den dönen liste bilgilerini eşleştirmek için kullanılır

| **Değişken Adı**         | **Tip**  | **Açıklama**                                                 |
| ------------------------ | -------- | ------------------------------------------------------------ |
| Id                       | string   | Ödeme ID'sini alır veya belirler.                            |
| CreatedAt                | DateTime | Ödemenin yapıldığı tarihi alır veya belirler.                |
| MerchantId               | string   | Satıcı ID'sini alır veya belirler.                           |
| UserId                   | string   | Kullanıcı ID'sini alır veya belirler.                        |
| PaymentMethod            | int?     | Ödeme Yöntemini alır veya belirler<br />0 - Kullanıcı, mevcut Papara bakiyesiyle işlemi tamamladı <br />1 - Kullanıcı, işlemi daha önce tanımlanmış bir banka / kredi kartı ile tamamladı. <br />2 - Kullanıcı, mobil ödeme yoluyla işlemi tamamladı. |
| PaymentMethodDescription | string   | Ödeme açıklamasını alır veya belirler.                       |
| ReferenceId              | string   | Referans ID'yi alır veya belirler.                           |
| OrderDescription         | string   | Sipariş açıklamasını alır veya belirler.                     |
| Status                   | int?     | Ödeme durumunu alır veya belirler. <br />0 - Bekleniyor, ödeme henüz yapılmadı. <br />1 - Ödeme yapıldı, işlem tamamlandı. <br />2 - İşlemler üye işyeri tarafından iade edilir. |
| StatusDescription        | string   | Ödeme durum açıklamasını alır veya belirler.                 |
| Amount                   | decimal? | Ödeme tutarını alır veya belirler.                           |
| Fee                      | decimal? | Hizmet bedelini alır veya belirler.                          |
| Currency                 | int?     | Ödemenin yapıldığı para birimini alır veya belirler. Olabilecek değerler “0”,  “1”, “2” veya “3” |
| NotificationUrl          | string   | Bildirim URL'ini alır veya belirler                          |
| NotificationDone         | bool?    | Bildirimin yapılıp yapılmadığını alır veya belirler          |
| RedirectUrl              | string   | Yönlendirme URL'ini alır veya belirler                       |
| PaymentUrl               | string   | Ödeme URL'ini alır veya belirler                             |
| MerchantSecretKey        | string   | Satıcı gizli anahtarını alır veya belirler                   |
| ReturningRedirectUrl     | string   | Geri dönüş URL'ini alır veya belirler                        |
| TurkishNationalId        | long     | TC Kimlik numarasını alır veya belirler                      |

### PaymentListOptions

`PaymentListOptions` `Payment`  servisi tarafından istek parametrelerini sağlamak için kullanılır

| **Değişken Adı** | **Tip** | **Açıklama**                                                 |
| ---------------- | ------- | ------------------------------------------------------------ |
| PageIndex        | int     | Sayfa dizinini alır veya belirler. Bir sayfada gösterilmek istenen kayıt sayısına (pageItemCount) göre hesaplanan sayfalardan gösterilmek istenen sayfanın indeks numarasıdır. Not: ilk sayfa her zaman 1'dir |
| PageItemCount    | Int     | Sayfa öğesi sayısını alır veya belirler. Bir sayfada gösterilmesi istenen kayıtların sayısıdır. |

### Servis Methodu

#### Kullanım Amacı

Yetkili satıcılar için tamamlanmış ödemeleri yeniden eskiye doğru sıralayacal bir şekilde görüntülemek için kullanılır

| **Method** | **Parametreler**   | **Geri Dönüş Tipi**               |
| ---------- | ------------------ | --------------------------------- |
| List       | PaymentListOptions | PaparaListResult<PaymentListItem> |

#### Kullanım Şekli

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var paymentService = new PaymentService(requestOptions);
var paymentServiceResult = paymentService.List(new PaymentListOptions
{
    PageIndex = 1,
    PageItemCount = 20
});

if (!paymentServiceResult.Succeeded)
{
    throw new Exception(paymentServiceResult.Error.Message);
}

return paymentServiceResult;
```

## Olası Hatalar ve Hata Kodları

| **Hata Kodu** | **Hata Açıklaması**                                          |
| ------------- | ------------------------------------------------------------ |
| 997           | Ödemeleri kabul etme yetkiniz yok. Müşteri temsilcinizle iletişime geçmelisiniz. |
| 998           | Gönderdiğiniz parametreler beklenen formatta değil. Örnek: zorunlu alanlardan biri sağlanmamıştır. |
| 999           | Papara sisteminde bir hata oluştu.                           |

# <a name="validation">Doğrulama</a> 

Bir son kullanıcıyı doğrulamak için doğrulama servisi kullanılacaktır. Doğrulama, hesap numarası, e-posta adresi, telefon numarası, ulusal kimlik numarası ile yapılabilir.

## Kullanıcı ID'si ile Doğrulama

Papara kullanıcı ID'si ile kullanıcıları doğrulamak için kullanılır. Bu işlemi gerçekleştirmek için `Validation` servisinde bulunan `ValidateById`methodunu kullanın. `UserId` gönderilmelidir.

### Validation Model           

`Validation` sınıfı, `Validation` servisi tarafından API'den dönen kullanıcı değerini eşleştirmek için kullanılır

| **Değişken Adı** | **Tip** | **Açıklama**                                          |
| ---------------- | ------- | ----------------------------------------------------- |
| UserId           | string  | Kullanıcı ID'sini alır veya belirler.                 |
| FirstName        | string  | Kullanıcının ismini alır veya belirler.               |
| LastName         | string  | Kullanıcının soyismini alır veya belirler.            |
| Email            | string  | Kullanıcının e-posta adresini alır veya belirler.     |
| PhoneNumber      | string  | Kullanıcının telefon numarasını alır veya belirler.   |
| Tckn             | Long    | Kullanıcının TC kimlik numarasını alır veya belirler. |
| AccountNumber    | int?    | Kullanıcının hesap numarasını alır veya belirler.     |

### ValidationByIdOptions 

`ValidationByIdOptions` `Validation` servisi tarafından istek parametrelerini sağlamak için kullanılır.

| **Değişken Adı** | **Tip** | **Açıklama**                             |
| :--------------- | ------- | ---------------------------------------- |
| UserId           | string  | Kullanıcının ID'sini alır veya belirler. |

### Servis Methodu

#### Kullanım Amacı

Kullanıcı ID'si ile doğrulama yapılmak istenildiğinde kullanılır

| **Method**   | **Parametreler**      | **Geri Dönüş Tipi**            |
| ------------ | --------------------- | ------------------------------ |
| ValidateById | ValidationByIdOptions | PaparaSingleResult<Validation> |

#### Kullanım Şekli

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var validationService = new ValidationService(requestOptions);
var validationServiceResult = validationService.ValidateById(new ValidationByIdOptions
{
    UserId = "USER_ID"
});

if (!validationServiceResult.Succeeded)
{
    throw new Exception(validationServiceResult.Error.Message);
}

return validationServiceResult;
```

## Hesap Numarası ile Doğrulama

Papara hesap numarası ile kullanıcıları doğrulamak için kullanılır. Bu işlemi gerçekleştirmek için `Validation` servisinde bulunan `ValidateByAccountNumber` methodunu kullanın. `AccountNumber` gönderilmelidir.

### ValidationByAccountNumberOptions

`ValidationByAccountNumberOptions` `Validation ` servisi tarafından istek parametrelerini sağlamak için kullanılır.

| **Değişken Adı** | **Tip** | **Açıklama**                                |
| ---------------- | ------- | ------------------------------------------- |
| AccountNumber    | long    | Papara hesap numarasını alır veya belirler. |

### Servis Methodu

#### Kullanım Amacı

Papara hesap numarası ile doğrulama yapılmak istenildiğinde kullanılır

| **Method**              | **Parametreler**                 | **Geri Dönüş Tipi**            |
| ----------------------- | -------------------------------- | ------------------------------ |
| ValidateByAccountNumber | ValidationByAccountNumberOptions | PaparaSingleResult<Validation> |

#### Kullanım Şekli

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var validationService = new ValidationService(requestOptions);
var validationServiceResult = validationService.ValidateByAccountNumber(new ValidationByAccountNumberOptions
{
    AccountNumber = 1
});

if (!validationServiceResult.Succeeded)
{
    throw new Exception(validationServiceResult.Error.Message);
}

return validationServiceResult;
```

## Telefon Numarası ile Doğrulama

Paparaya kayıtlı telefon numarası ile kullanıcıları doğrulamak için kullanılır. Bu işlemi gerçekleştirmek için `Validation` servisinde bulunan `ValidateByPhoneNumber`methodunu kullanın. `PhoneNumber` gönderilmelidir.

### ValidationByPhoneNumberOptions

`ValidationByPhoneNumberOptions` `Validation` servisi tarafından istek parametrelerini sağlamak için kullanılır.

| **Değişken Adı** | **Tip** | **Açıklama**                                                |
| ---------------- | ------- | ----------------------------------------------------------- |
| PhoneNumber      | string  | Paparaya kayıtlı olan telefon numarasını alır veya belirler |

### Servis Methodu

#### Kullanım Amacı

Paparaya kayıtlı telefon numarası ile doğrulama yapılmak istenildiğinde kullanılır

| **Method**            | **Parametreler**               | **Geri Dönüş Tipi**            |
| --------------------- | ------------------------------ | ------------------------------ |
| ValidateByPhoneNumber | ValidationByPhoneNumberOptions | PaparaSingleResult<Validation> |

#### Kullanım Şekli

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var validationService = new ValidationService(requestOptions);
var validationServiceResult = validationService.ValidateByPhoneNumber(new ValidationByPhoneNumberOptions
{
    PhoneNumber = "PHONE_NUMBER"
});

if (!validationServiceResult.Succeeded)
{
    throw new Exception(validationServiceResult.Error.Message);
}

return validationServiceResult;
```

## E-Posta Adresi ile Doğrulama

Paparaya kayıtlı e-posta adresi ile kullanıcıları doğrulamak için kullanılır. Bu işlemi gerçekleştirmek için `Validation` servisinde bulunan `ValidateByEmail`methodunu kullanın. `Email` gönderilmelidir.

### ValidationByEmailOptions

`ValidationByEmailOptions` `Validation`servisi tarafından istek parametrelerini sağlamak için kullanılır.

| **Değişken Adı** | **Tip** | **Açıklama**                                              |
| ---------------- | ------- | --------------------------------------------------------- |
| Email            | string  | Paparaya kayıtlı olan e-posta adresini alır veya belirler |

### Servis Methodu

#### Kullanım Amacı

Paparaya kayıtlı e-posta adresi ile doğrulama yapılmak istenildiğinde kullanılır

| **Method**      | **Parametreler**         | **Geri Dönüş Tipi**            |
| --------------- | ------------------------ | ------------------------------ |
| ValidateByEmail | ValidationByEmailOptions | PaparaSingleResult<Validation> |

#### Kullanım Şekli

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var validationService = new ValidationService(requestOptions);
var validationServiceResult = validationService.ValidateByEmail(new ValidationByEmailOptions
{
    Email = "E-MAIL_ADDRESS"
});

if (!validationServiceResult.Succeeded)
{
    throw new Exception(validationServiceResult.Error.Message);
}

return validationServiceResult;
```

## TC Kimlik Numarası ile Doğrulama

Paparaya kayıtlı TC kimlik numarası ile kullanıcıları doğrulamak için kullanılır. Bu işlemi gerçekleştirmek için `Validation` servisinde bulunan `ValidateByTckn`methodunu kullanın. `Tckn` gönderilmelidir.

### ValidationByTcknOptions

`ValidationByPhoneNumberOptions` `Validation`servisi tarafından istek parametrelerini sağlamak için kullanılır.

| **Değişken Adı** | **Tip** | **Açıklama**                            |
| ---------------- | ------- | --------------------------------------- |
| Tckn             | long    | TC Kimlik numarasını alır veya belirler |

### Servis Methodu

#### Kullanım Amacı

Paparaya kayıtlı TC kimlik numarası ile doğrulama yapılmak istenildiğinde kullanılır

| **Method**     | **Parametreler**        | **Geri Dönüş Tipi**            |
| -------------- | ----------------------- | ------------------------------ |
| ValidateByTckn | ValidationByTcknOptions | PaparaSingleResult<Validation> |

#### Kullanım Şekli

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Test veya canlı ortamı için bağlantı yapılandırması
};

var validationService = new ValidationService(requestOptions);
var validationServiceResult = validationService.ValidateByTckn(new ValidationByTcknOptions
{
    Tckn = 12345678901
});

if (!validationServiceResult.Succeeded)
{
    throw new Exception(validationServiceResult.Error.Message);
}

return validationServiceResult;
```



# <a name="response-types">Geri Dönüş Tipleri</a>

Bu bölüm, API'den dönüş değerleri hakkında teknik bilgiler içerir.

## PaparaSingleResult

Papara Single Result tipi. API'ye gönderilen ve API'den dönen nesne veri tiplerini işler.

| **Değişken Adı** | **Tip** | **Açıklama**                                                 |
| :--------------- | ------- | ------------------------------------------------------------ |
| Data             | TEntity | Genel nesne dönüş tipi. Verilen nesne tipi değerini döndürür |

## PaparaListResult

Papara List tipi. API'ye gönderilen ve API'den dönen liste veri tiplerini işler.

| **Değişken Adı** | **Tip**                     | **Açıklama**                                                 |
| ---------------- | --------------------------- | ------------------------------------------------------------ |
| Data             | PaparaPagingResult<TEntity> | Genel liste dönüş tipi. Verilen liste tipi değerini döndürür |

## PaparaPagingResult      

Papara Paging tipi. API'ye gönderilen ve API'den dönen sayfalandırılmış veri tiplerini işler.

| **Değişken Adı** | **Tip**       | **Açıklama**                                                 |
| ---------------- | ------------- | ------------------------------------------------------------ |
| Items            | List<TEntity> | API'den dönen öğeleri alır veya ayarlar. Verilen nesne tipinin listesini döndürür |
| Page             | int           | Sayfa sayısını alır veya belirler                            |
| PageItemCount    | int           | Sayfadaki öge sayısını alır veya belirler                    |
| TotalItemCount   | int           | Toplam öge sayısını alır veya belirler                       |
| TotalPageCount   | int           | Toplam sayfa sayısını alır ve belirler                       |
| PageSkip         | int           | Kaç sayfanın atlanacağını alır veya belirler                 |

## PaparaArrayResult

Papara Array tipi. API'ye gönderilen ve API'den dönen dizi veri tiplerini işler.

| **Değişken Adı** | **Tip**   | **Açıklama**                                        |
| ---------------- | --------- | --------------------------------------------------- |
| Data             | TEntity[] | Genel dizi dönüş tipi. Verilen dizi tipini döndürür |

## PaparaServiceResult

Papara Service Result tipi. API'den dönen yanıt veri tiplerini işler.

| **Değişken Adı** | **Tip**              | **Açıklama**                                                 |
| ---------------- | -------------------- | ------------------------------------------------------------ |
| Succeeded        | bool                 | İşlemin başarıyla sonuçlanıp sonuçlanmadığını gösteren bir değer alır veya belirler |
| Error            | ServiceResultError   | İşlemin başarısız olup olmadığını gösteren bir değer alır veya belirler |
| Result           | ServiceResultSuccess | Başarılı olan işlem sonucunu alır veya belirler.             |

## ServiceResultError

Papara Service Error Result tipi. API'den dönen hata yanıtlarını işler.

| **Değişken Adı** | **Tip** | **Açıklama**                     |
| ---------------- | ------- | -------------------------------- |
| Message          | string  | Hata mesajını alır veya belirler |
| Code             | int     | Hata kodunu alır veya belirler   |

## ServiceResultSuccess

Papara Service Success Result tipi. API'den dönen başarılı yanıtları işler.

| **Değişken Adı** | **Tip** | **Açıklama**                                      |
| ---------------- | ------- | ------------------------------------------------- |
| Message          | string  | Başarılı işlem sonuç mesajını alır veya belirler  |
| Code             | int     | Başarılı işlem sonuç kodlarını alır veya belirler |

 