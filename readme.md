# Table of Contents

<a href="#intro">Intro</a>

<a href="#enums">Enums</a>

<a href="#account">Account</a>

<a href="#banking">Banking</a>

<a href="#cash-deposit">Cash Deposit</a>

<a href="#mass-payment">Mass Payment</a>

<a href="#recurring-mass-payment">Recurring Mass Payment</a>

<a href="#payments">Payments</a>

<a href="#validation">Validation</a>

<a href="#response-types">Response Types</a>

# <a name="intro">Intro</a> 

Integrating Papara into your software requires following;

1. Obtain your API Key. So Papara can authenticate integration’s API requests. To obtain your API Key, follow https://merchant.test.papara.com/ URL. After sucessfully logged in, API Key can be viewed on https://merchant.test.papara.com/APIInfo 

2. Install client library. So your integration can interact with the Papara API. Install operations are like following.

## Nuget operations

```bash
# Install via dotnet
dotnet add package Papara.Net
dotnet restore
```
or 

```bash
# Or install via NuGet
PM> Install-Package Papara.Net
```

# Configurations

## Dotnet Core Setup

Before connecting to API, dotnet core developers should configure client settings. Create an `appsettings.json` file on the root of your project. Proper `appsettings.json` file should look like the following; 

``` json  {
{
  "Papara": {
  	"ApiKey": "INSERT_YOUR_API_KEY_HERE", // Papara Registered API KEY
  	"Env": "Test", // Target environment. Test or Live
  }
}
```

After you create the `appsetting.json` file, add following lines in `ConfigureServices` method on your `Startup.cs` file.

```csharp
services.AddPapara(o =>
                   {
                       o.ApiKey = Configuration["Papara:ApiKey"];
                       o.Env = Configuration["Papara:Env"];
                   });
```

Dependency injection can be used for setting up the client. API Key and Environment variable will be read from `appsettings.json` file.

```csharp
private readonly  IPaparaClient PaparaClient;
public AccountController(IPaparaClient  paparaClient)
{
    this.PaparaClient  = paparaClient;      
}
```

Another way to set up client is to construct manually.

```csharp
private readonly  PaparaClient paparaClient;         

public AccountController()
{
    paparaClient = new PaparaClient("INSERT_YOUR_API_KEY_HERE",PaparaEnv.Test);
} 
```

Or, `SetOptions` method can be used. API Key and Environment variable will be read from `appsettings.json` file.

```csharp
PaparaClient  paparaClient = new PaparaClient();  
paparaClient.SetOptions("INSERT_YOUR_API_KEY_HERE", PaparaEnv.Test);
```

### Dotnet Core API Test Request 

After everything is set, use code block below to test everything works perfectly. 

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



*Note: Please keep that in mind that all methods in client have both synchronous and asynchronous versions.* 

E.G `await this.PaparaClient.AccountService.GetAccountAsync()` and `this.PaparaClient.AccountService.GetAccount();` 

## .NET Framework Setup

For .NET Framework developers, insert `ApiKey` and `Env` variables into `web.config` file under `appSettings` section.

``` xml  
<add key="Papara:ApiKey" value="INSERT_YOUR_API_KEY_HERE"/>

<add key="Papara:Env" value="Test"/>
```

### .NET Framework API Test Request

After everything is set, use code block below to test everything works perfectly for .Net Framework. 

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

# <a name="enums">Enums</a>

# CashDepositProvisionStatus

When a cash deposit request was made, following statuses will return and display the status of provision.

| **Key**         | **Value** | **Description**                      |
| --------------- | --------- | ------------------------------------ |
| Pending         | 0         | Cash deposit is pending provision.   |
| Complete        | 1         | Cash Deposit is completed            |
| Cancel          | 2         | Cash Deposit is cancelled            |
| ReadyToComplete | 3         | Cash Deposit is ready for completion |

 

# Currency

All currencies on the API are listed below.

| **Key** | **Value** | **Description** |
| ------- | --------- | --------------- |
| TRY     | 0         | Turkish Lira    |
| USD     | 1         | U.S Dollar      |
| EUR     | 2         | Euro            |

 

# EntryType

Entry types are used in ledgers and cash deposits in order to track the money in the operation. Possible entry types are listed below.

| **Key**                       | **Value** | **Description**                                              |
| ----------------------------- | --------- | ------------------------------------------------------------ |
| BankTransfer                  | 1         | Bank Transfer: Cash deposit or withdrawal                    |
| CorporateCardTransaction      | 2         | Papara Corporate Card Transaction:  Transaction which was operated by corporation card assigned to merchant |
| LoadingMoneyFromPhysicalPoint | 6         | Loading Money From Physical Point: Cash  deposit operation from contracted location |
| MerchantPayment               | 8         | Merchant Payment: Checkout via Papara                        |
| PaymentDistribution           | 9         | Payment Distribution: Masspayment via  papara                |
| EduPos                        | 11        | Offline payment. EDU POS via Papara                          |

 

# PaymentMethod

Three types of payment is accepted in the system. Possible payment methods are listed below. 

| **Key**       | **Value** | **Description**        |
| ------------- | --------- | ---------------------- |
| PaparaAccount | 0         | Papara Account Balance |
| Card          | 1         | Registered Credit Card |
| Mobile        | 2         | Mobile Payment         |

 

# PaymentStatus

After a payment was done, API returns the payment status which are shown below.

| **Key**   | **Value** | **Description**            |
| --------- | --------- | -------------------------- |
| Pending   | 0         | Payment waiting            |
| Completed | 1         | User completed the payment |
| Refunded  | 2         | Order refunded             |

# <a name="account">Account</a>

This part contains the technical integration information prepared for the use of the account and balance information of the merchant. Account and balance information on Papara account can be retrieved by Account service. Developers can also retrieve the balance history, which contains a list of transactions that contributed to the balance.

## Get Account Information

Returns the merchant account and balance information. Balance information contains current balance, available funds and unavailable funds, whilst account information contains brand name and full title of the merchant. To perform this operation use `GetAccount` method on `Account` service. 

### Account Model

`Account` class is used by account service to match returning account value from API and contains account information.

| **Variable Name**   | **Type**                 | **Description**                        |
| ------------------- | ------------------------ | -------------------------------------- |
| LegalName           | string                   | Gets or sets merchant’s company title. |
| BrandName           | string                   | Gets or sets brand name.               |
| AllowedPaymentTypes | List<AllowedPaymentType> | Gets or sets allowed payment types.    |
| Balances            | List<AccountBalance>     | Gets or sets account balances          |

### AllowedPaymentType Model

`AllowedPaymentType` class is used by account service to match returning account value from API. `AllowedPaymentType` displays allowed payment types.

| **Variable Name** | **Type** | **Description**                                              |
| ----------------- | -------- | ------------------------------------------------------------ |
| PaymentMethod     | int      | Gets or sets payment method.<br />0 - Papara Account Balance  <br />1 – Credit/Debit Card <br />2- Mobile - Mobile Payment. |

### AccountBalance Model

`AccountBalance` class is used by account service to match returning account balance value from API. Account balance shows current balance figures and lists three types of balances and general currency.

| **Variable Name** | **Type** | **Description**                |
| ----------------- | -------- | ------------------------------ |
| Currency          | int      | Gets or sets currency          |
| TotalBalance      | decimal  | Gets or sets total balance     |
| LockedBalance     | decimal  | Gets or sets locked balance    |
| AvailableBalance  | decimal  | Gets or sets available balance |

### Service Method

#### Purpose

Return account information and current balance for authorized merchant.

| **Method**                  | **Params** | **Return Type**             |
| --------------------------- | ---------- | --------------------------- |
| GetAccount, GetAccountAsync | None       | PaparaSingleResult<Account> |

#### Usage

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



## List Ledgers

Returns the merchant account history (list of transactions) in paged format. This method is used for listing all transactions made for a merchant including resulting balance for each transaction.  To perform this operation use `ListLedgers` method on `Account` service. `StartDate` and `EndDate` should be provided.

### AccountLedger Model

`AccountLedger` class is used by account service to match returning ledger values from API. Represents a transaction itself.

| **Variable Name**   | **Type**     | **Description**                                              |
| ------------------- | ------------ | ------------------------------------------------------------ |
| ID                  | int?         | Gets or sets merchant ID                                     |
| CreatedAt           | DateTime     | Gets or sets created date of a ledger                        |
| EntryType           | EntryType?   | Gets or sets entry type                                      |
| EntryTypeName       | string       | Gets or sets entry type name                                 |
| Amount              | decimal?     | Gets or sets amount                                          |
| Fee                 | decimal?     | Gets or sets fee                                             |
| Currency            | int?         | Gets or sets currency                                        |
| CurrencyInfo        | CurrencyInfo | Gets or sets currency information                            |
| ResultingBalance    | decimal?     | Gets or sets resulting balance                               |
| Description         | string       | Gets or sets description                                     |
| MassPaymentId       | string       | Gets or sets mass payment Id. It is the  unique value sent by the merchant to prevent duplicate repetition in payment  transactions. It is displayed in transaction records of masspayment type in  account transactions to ensure control of the transaction. It will be null in  other payment types. |
| CheckoutPaymentId   | string       | Gets or sets checkout payment ID. It is  the ID field in the data object in the payment record transaction. It is the  unique identifier of the payment transaction. It is displayed in transaction  records of checkout type in account transactions. It will be null in other  payment types. |
| CheckoutReferenceID | string       | Gets or sets checkout reference ID. This  is the referenceId field sent when creating the payment transaction record.  It is the reference information of the payment transaction in the merchant  system. It is displayed in transaction records of checkout type in account  transactions. It will be null in other payment types |

### CurrencyInfo Model

`CurrencyInfo` class is used by account ledger model to get or set returning currency values from API. Represents the currency information available in a ledger.

| **Variable Name**    | **Type** | **Description**                                |
| -------------------- | -------- | ---------------------------------------------- |
| CurrencyEnum         | Currency | Gets or sets currency type.                    |
| Symbol               | string   | Gets or sets currency symbol                   |
| Code                 | string   | Gets or sets currency code                     |
| PreferredDisplayCode | string   | Gets or sets currency's preferred display code |
| Name                 | string   | Gets or sets currency name                     |
| IsCryptoCurrency     | bool?    | Gets or sets if it is a cryptocurrency or not  |
| Precision            | int      | Gets or sets currency precision                |
| IconUrl              | string   | Gets or sets currency icon URL                 |

### LedgerListOptions Model

`LedgerListOptions`  is used by account service for providing request parameters for ledger listing operation. 

| **Variable Name** | **Type** | **Description**                                              |
| ----------------- | -------- | ------------------------------------------------------------ |
| StartDate         | DateTime | Gets or sets start date for transactions                     |
| EndDate           | DateTime | Gets or sets end date for transactions                       |
| EntryType         | enum     | Gets or sets entry types                                     |
| AccountNumber     | int?     | Gets or sets merchant account number                         |
| Page              | int      | Gets or sets the requested page number. If  the requested date has more than 1 page of results for the requested  PageSize, use this to iterate through pages |
| PageSize          | int      | Gets or sets number of elements you want  to receive per request page. Min=1, Max=50 |

### Service Method

#### Purpose

Returns list of ledgers for authorized merchant.

| **Method**                    | **Params**        | **Return Type**                 |
| ----------------------------- | ----------------- | ------------------------------- |
| ListLedgers, ListLedgersAsync | LedgerListOptions | PaparaListResult<AccountLedger> |

#### Usage

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

## Get Settlement

Calculates the count and volume of transactions within the given time period. To perform this operation use `GetSettlement` method on `Account` service. `StartDate` and `EndDate` should be provided.

### Settlement Model

`Settlement` class is used by account service to match returning settlement values API.

| **Variable Name** | **Type** | **Description**                 |
| ----------------- | -------- | ------------------------------- |
| Count             | int?     | Gets or sets transaction count  |
| Volume            | int?     | Gets or sets transaction volume |

### SettlementGetOptions Model

`SettlementGetOptions` is used by account service for providing settlement request parameters.

| **Variable Name** | **Type**  | **Description**                          |
| ----------------- | --------- | ---------------------------------------- |
| StartDate         | DateTime  | Gets or sets start date for transactions |
| EndDate           | DateTime  | Gets or sets end date for transactions   |
| EntryType         | EntryType | Gets or sets entry types                 |

### Service Method

#### Purpose

Returns settlement for authorized merchant.

| **Method**                         | **Params**           | **Return Type**                |
| ---------------------------------- | -------------------- | ------------------------------ |
| GetSettlement,  GetSettlementAsync | SettlementGetOptions | PaparaSingleResult<Settlement> |

#### Usage

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



# <a name="banking">Banking</a> 

This part contains technical integration information prepared for merchants those who want to quickly and securely list their bank accounts with Papara and/or create a withdrawal request to their bank accounts.

## Get Bank Accounts

Retrieves registered bank accounts of the merchant. To perform this operation use `GetBankAccounts` method on `Banking` service.

### BankAccount Model

`BankAccount` class is used by banking service to match returning bank accounts from API

| **Variable Name** | **Type** | **Description**                         |
| ----------------- | -------- | --------------------------------------- |
| BankAccountId     | int?     | Gets or sets merchant's bank account ID |
| BankName          | string   | Gets or sets merchant bank name         |
| BranchCode        | string   | Gets or sets merchant branch code       |
| Iban              | string   | Gets or sets IBAN Number                |
| AccountCode       | string   | Gets or sets merchant account code      |
| Description       | string   | Gets or sets description                |
| Currency          | string   | Gets or sets currency                   |

### Service Method

#### Purpose

Returns bank accounts for authorized merchant.

| **Method**      | **Params** | **Return Type**                |
| --------------- | ---------- | ------------------------------ |
| GetBankAccounts |            | PaparaArrayResult<BankAccount> |

#### Usage

``` csharp 
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
};

var bankingService = new BankingService(requestOptions);
var bankingServiceResult = bankingService.GetBankAccounts();

if (!bankingServiceResult.Succeeded)
{
    throw new Exception(bankingServiceResult.Error.Message);
}

return bankingServiceResult;
```

## Withdrawal

Generates withdrawal requests for merchants. To perform this operation use `Withdrawal` method on `Banking` service.

### BankingWithdrawalOptions 

`BankingWithdrawalOptions` is used by banking service for providing request parameters.

| **Variable Name** | **Type** | **Description**                                              |
| ----------------- | -------- | ------------------------------------------------------------ |
| BankAccountId     | int?     | Gets or sets target bank account id which  money will be transferred to when withdrawal is completed.It will be obtained  as a result of the request to list bank accounts. |
| Amount            | decimal  | Gets or sets withdrawal amount                               |

### Service Method

#### Purpose

Creates a withdrawal request from given bank account for authorized merchant.

| **Method** | **Params**               | **Return Type**     |
| ---------- | ------------------------ | ------------------- |
| Withdrawal | BankingWithdrawalOptions | PaparaServiceResult |

#### Usage

``` csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Possible Errors and Error Codes

| **Error Code** | **Error Description**                          |
| -------------- | ---------------------------------------------- |
| 105            | Insufficient funds.                            |
| 115            | Requested amount is lower then  minimum limit. |
| 120            | Bank account not found.                        |
| 247            | Merchant's account is not active.              |



# <a name="cash-deposit">Cash Deposit</a> 

With the integration of Papara physical point, you can become a money loading point and earn money from which end users can load balance to their Papara accounts. Physical point integration methods should only be used in scenarios where users load cash to Papara accounts.

## Get Cash Deposit Information

Returns cash deposit information. To perform this operation use `GetCashDeposit` method on `Cash Deposit` service. `Id` should be provided.

### CashDeposit Model

`CashDeposit` class is used by cash deposit service to match returning cash deposit values from API

| **Variable Name** | **Type**  | **Description**                                       |
| ----------------- | --------- | ----------------------------------------------------- |
| MerchantReference | string    | Gets or sets merchant reference code                  |
| Id                | int?      | Gets or sets cash deposit ID                          |
| CreatedAt         | DateTime? | Gets or sets created date of cash deposit             |
| Amount            | decimal?  | Gets or sets amount of cash deposit                   |
| Currency          | int?      | Gets or sets currency of cash deposit                 |
| Fee               | decimal?  | Gets or sets fee of cash deposit                      |
| ResultingBalance  | decimal?  | Gets or sets resulting balance in  merchant's account |
| Description       | string    | Gets or sets description                              |

### CashDepositGetOptions

`CashDepositGetOptions` is used by cash deposit service for providing request parameters

| **Variable Name** | **Type** | **Description**              |
| ----------------- | -------- | ---------------------------- |
| Id                | long     | Gets or sets cash deposit ID |

### Service Method

#### Purpose

Returns a cash deposit information

| **Method**     | **Params**            | **Return Type**                 |
| -------------- | --------------------- | ------------------------------- |
| GetCashDeposit | CashDepositGetOptions | PaparaSingleResult<CashDeposit> |

####   Usage

``` csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Get Cash Deposit By Reference

Returns the information of the money loading process from the physical point with the merchant reference information. To perform this operation use `GetCashDepositByReference` method on `Cash Deposit` service. `Reference` should be provided.

### CashDepositByReferenceOptions

`CashDepositByReferenceOptions` is used by cash deposit service for providing request parameters.

| **Variable Name** | **Type** | **Description**                                              |
| ----------------- | -------- | ------------------------------------------------------------ |
| Reference         | string   | Gets or sets cash deposit reference no.  Reference no is required. |

### Service Method

#### Purpose

Returns a cash deposit object using merchant's unique reference number.

| **Method**                | **Params**                    | **Return Type**                 |
| ------------------------- | ----------------------------- | ------------------------------- |
| GetCashDepositByReference | CashDepositByReferenceOptions | PaparaSingleResult<CashDeposit> |

#### Usage

``` csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Create Cash Deposit With Phone Number

It deposits money to the user from the physical point. using user’s phone number. To perform this operation use `CreateWithPhoneNumber` method on `Cash Deposit` service. `PhoneNumber`, `Amount` and `MerchantReference` should be provided.

### CashDepositToPhoneOptions

`CashDepositToPhoneOptions` is used by cash deposit service for providing request parameters.

| **Variable Name** | **Type** | **Description**                                              |
| ----------------- | -------- | ------------------------------------------------------------ |
| PhoneNumber       | string   | Gets or sets phone number. The mobile  phone number registered in the Papara account of the user to be loaded with  cash. |
| Amount            | decimal  | Gets or sets amount. The amount of the  cash deposit. This amount will be transferred to the account of the user who  received the payment. The amount to be deducted from the merchant account  will be exactly this number. |
| MerchantReference | string   | Gets or sets merchant reference. The  unique value sent by the merchant to prevent false repetitions in cash  loading transactions. If a previously submitted and successful  merchantReference is resubmitted with a new request, the request will fail.  MerchantReference sent with failed requests can be resubmitted. |

### Service Method

#### Purpose

Creates a cash deposit request using end users's phone number.

| **Method**            | **Params**                | **Return Type**                 |
| --------------------- | ------------------------- | ------------------------------- |
| CreateWithPhoneNumber | CashDepositToPhoneOptions | PaparaSingleResult<CashDeposit> |

#### Usage

``` csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Create Cash Deposit With Account Number

Deposits money to the user with Papara number from the physical point. To perform this operation use `CreateWithAccountNumber` on `Cash Deposit` service. `AccountNumber`, `Amount` and `MerchantReference` should be provided.

### CashDepositToAccountNumberOptions

`CashDepositToAccountNumberOptions` is used by cash deposit service for providing request parameters.

| **Variable Name** | **Type** | **Description**                                              |
| ----------------- | -------- | ------------------------------------------------------------ |
| AccountNumber     | int      | Gets or sets account number. Papara  account number of the user who will be loaded with cash. |
| Amount            | decimal  | Gets or sets amount. The amount of the  cash deposit. This amount will be transferred to the account of the user who  received the payment. The amount to be deducted from the merchant account  will be exactly this number. |
| MerchantReference | string   | Gets or sets merchant reference. The  unique value sent by the merchant to prevent false repetitions in cash  loading transactions. If a previously submitted and successful  merchantReference is resubmitted with a new request, the request will fail.  MerchantReference sent with failed requests can be resubmitted. |

### Service Method

#### Purpose

Creates a cash deposit request using end user's account number.

| **Method**              | **Params**                        | **Return Type**                 |
| ----------------------- | --------------------------------- | ------------------------------- |
| CreateWithAccountNumber | CashDepositToAccountNumberOptions | PaparaSingleResult<CashDeposit> |

#### Usage


```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Create Cash Deposit With National Identity Number

Deposits money to the user with national identity number registered in Papara from the physical point. To perform this operation use `CreateWithTckn` on `Cash Deposit` service.  `Tckn`, `Amount` and `MerchantReference` should be provided.

### CashDepositToTcknOptions

`CashDepositToTcknOptions` is used by cash deposit service for providing request parameters.

| **Variable Name** | **Type** | **Description**                                              |
| ----------------- | -------- | ------------------------------------------------------------ |
| Tckn              | long     | Gets or sets national identity number  which is linked to user's Papara account |
| Amount            | decimal  | Gets or sets amount. The amount of the  cash deposit. This amount will be transferred to the account of the user who  received the payment. The amount to be deducted from the merchant account  will be exactly this number |
| MerchantReference | string   | Gets or sets merchant reference. The  unique value sent by the merchant to prevent false repetitions in cash  loading transactions. If a previously submitted and successful  merchantReference is resubmitted with a new request, the request will fail.  MerchantReference sent with failed requests can be resubmitted |

### Service Method

#### Purpose

Creates a cash deposit request using end users's national identity number.

| **Method**     | **Params**               | **Return Type**                 |
| -------------- | ------------------------ | ------------------------------- |
| CreateWithTckn | CashDepositToTcknOptions | PaparaSingleResult<CashDeposit> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Create Cash Deposit Provision With National Identity Number

Creates a request to deposit money from the physical point using national identity number registered in Papara without provision. To perform this operation use `CreateProvisionWithTckn` on `Cash Deposit` service. `PhoneNumber`, `Tckn`, `Amount` and `MerchantReference` should be provided.

### CashDepositProvision Model

`CashDepositProvision` class is used by cash deposit service to match returning cash deposit provision values from API.

| **Variable Name** | **Type** | **Description**                           |
| ----------------- | -------- | ----------------------------------------- |
| Id                | int      | Gets or sets cash deposit ID              |
| CreatedAt         | DateTime | Gets or sets created date of cash deposit |
| Amount            | decimal? | Amount                                    |
| Currency          | int      | Currency                                  |
| MerchantReference | string   | Gets or sets merchant reference code      |
| UserFullName      | string   | Gets or sets end user's full name         |

### CashDepositToTcknOptions

`CashDepositToTcknOptions` is used by cash deposit service for providing request parameters. 

| **Variable Name** | **Type** | **Description**                                              |
| ----------------- | -------- | ------------------------------------------------------------ |
| Tckn              | int      | Gets or sets national identity number  which is linked to user's Papara account |
| Amount            | decimal  | Gets or sets amount. The amount of the  cash deposit. This amount will be transferred to the account of the user who  received the payment. The amount to be deducted from the merchant account  will be exactly this number. |
| MerchantReference | string   | Gets or sets merchant reference. The  unique value sent by the merchant to prevent false repetitions in cash loading  transactions. If a previously submitted and successful merchantReference is  resubmitted with a new request, the request will fail. MerchantReference sent  with failed requests can be resubmitted. |

### Service Method

#### Purpose

Creates a cash deposit request without upfront payment using end user's national identity number.

| **Method**              | **Params**               | **Return Type**                          |
| ----------------------- | ------------------------ | ---------------------------------------- |
| CreateProvisionWithTckn | CashDepositToTcknOptions | PaparaSingleResult<CashDepositProvision> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Create Cash Deposit Provision With Phone Number

Creates a request to deposit money from the physical point using phone number registered in Papara without provision. To perform this operation use `CreateProvisionWithPhoneNumber` on `Cash Deposit` service. `PhoneNumber`, `Amount` and `MerchantReference` should be provided.

### Service Method

#### Purpose

Creates a cash deposit request without upfront payment using end users's phone number.

| **Method**                     | **Params**                | **Return Type**                          |
| ------------------------------ | ------------------------- | ---------------------------------------- |
| CreateProvisionWithPhoneNumber | CashDepositToPhoneOptions | PaparaSingleResult<CashDepositProvision> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Create Cash Deposit Provision With Account Number

Creates a request to deposit money from the physical point using Papara number without provision. To perform this operation use `CreateProvisionWithAccountNumber` on `Cash Deposit` service. `AccountNumber`, `Amount` and `MerchantReference` should be provided.

### Service Method

#### Purpose

Creates a cash deposit request without upfront payment using end users's phone number.

| **Method**                       | **Params**                        | **Return Type**                          |
| -------------------------------- | --------------------------------- | ---------------------------------------- |
| CreateProvisionWithAccountNumber | CashDepositToAccountNumberOptions | PaparaSingleResult<CashDepositProvision> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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



## Cash Deposit Provision Control By Reference Code

With the reference code created by the user, it checks the deposit request without prepayment from the physical point and makes it ready to be approved. To perform this operation, use `provisionByReferenceControl` on `Cash Deposit` service. `referenceCode` and `amount` should be provided.

### Service Method

#### Purpose

Makes  a cash deposit request ready to be completed without upfront payment.

| **Method**                  | **Params**                | **Return Type** |
| --------------------------- | ------------------------- | --------------- |
| ProvisionByReferenceControl | CashDepositControlOptions | ServiceResult   |

#### Usage

```java
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
};

var cashdepositControlOptions = new CashDepositControlOptions
{
    Amount = 10,
    ReferenceCode = "CASH_DEPOSIT_REFERENCE_NUMBER"
};

var result = paparaClient.CashDepositService.ProvisionByReferenceControl(cashdepositControlOptions);

return result;
```

## Complete Cash Deposit Provision By Reference Code

With the reference code created by the user, it approves the deposit request without prepayment from the physical point and transfers the balance to the user. To perform this operation, use `completeProvisionByReference` on `Cash Deposit` service. `referenceCode` and `amount` should be provided.

### Service Method

#### Purpose

Completes a cash deposit request without upfront payment.

| **Method**                   | **Params**                | **Return Type** |
| ---------------------------- | ------------------------- | --------------- |
| CompleteProvisionByReference | CashDepositControlOptions | ServiceResult   |

#### Usage

```java
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
};

var cashdepositControlOptions = new CashDepositControlOptions
{
    Amount = 10,
    ReferenceCode = "CASH_DEPOSIT_REFERENCE_NUMBER"
};

var result = paparaClient.CashDepositService.CompleteProvisionByReference(cashdepositControlOptions);

return result;
```



## Cash Deposit Completion

Confirms the deposit request created from the physical point to the user without prepayment. To perform this operation, use `CompleteProvision` on `Cash Deposit` service. `Id` and `TransactionDate` should be provided.

### CashDepositCompleteOptions

`CashDepositCompleteOptions` is used by cash deposit service for providing request parameters.

| **Variable Name** | **Type** | **Description**                                |
| ----------------- | -------- | ---------------------------------------------- |
| Id                | int      | Gets or sets ID of cash deposit request        |
| TransactionDate   | DateTime | Gets or sets date of cash deposit  transaction |

### Service Method

#### Purpose

Completes a cash deposit request without upfront payment.

| **Method**        | **Params**                 | **Return Type**                 |
| ----------------- | -------------------------- | ------------------------------- |
| CompleteProvision | CashDepositCompleteOptions | PaparaSingleResult<CashDeposit> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Get Cash Deposit By Date

Retrieves information of money deposits from the physical point. To perform this operation, use `GetCashDepositByDate` on `Cash Deposit` service. `StartDate`, `EndDate`, `PageIndex` and `PageItemCount` should be provided.

### CashDepositByDateOptions

`CashDepositByDateOptions` is used by cash deposit service for providing request parameters.

| **Variable Name** | **Type** | **Description**                                              |
| ----------------- | -------- | ------------------------------------------------------------ |
| StartDate         | DateTime | Gets or sets start date of cash deposit                      |
| EndDate           | DateTime | Gets or sets end date of cash deposit                        |
| PageIndex         | int      | Gets or sets page index. It is the index  number of the page that is wanted to display from the pages calculated on the  basis of the number of records (pageItemCount) desired to be displayed on a  page. Note: the first page is always 1 |
| PageItemCount     | int      | Gets or sets page item count. The number  of records that are desired to be displayed on a page |

### Service Method

#### Purpose

Returns a cash deposit information by given date.

| **Method**           | **Params**               | **Return Type**                |
| -------------------- | ------------------------ | ------------------------------ |
| GetCashDepositByDate | CashDepositByDateOptions | PaparaArrayResult<CashDeposit> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Provision Settlements

Returns the total number and volume of transactions performed within the given dates. Both start and end dates are included in the calculation. To perform this operation, use `ProvisionSettlements` on `Cash Deposit` service. `StartDate` and `EndDate` should be provided.

### CashDepositSettlementOptions

`CashDepositSettlementOptions` is used by cash deposit service for providing request parameters.

| **Variable Name** | **Type**   | **Description**                        |
| ----------------- | ---------- | -------------------------------------- |
| StartDate         | DateTime   | Gets or sets start date for settlement |
| EndDate           | DateTime   | Gets or sets end date for settlement   |
| EntryType         | EntryType? | Gets or sets entry type for settlement |

### Service Method

#### Purpose

Returns total transaction volume and count between given dates. Start and end dates are included.

| **Method**           | **Params**                   | **Return Type**                           |
| -------------------- | ---------------------------- | ----------------------------------------- |
| ProvisionSettlements | CashDepositSettlementOptions | PaparaSingleResult<CashDepositSettlement> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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



## Settlements

Returns the total number and volume of transactions performed within the given dates. Both start and end dates are included in the calculation. To perform this operation, use `Settlements` on `Cash Deposit` service. `StartDate` and `EndDate` should be provided.

### CashDepositSettlementOptions

`CashDepositSettlementOptions` is used by cash deposit service for providing request parameters.

| **Variable Name** | **Type**   | **Description**                        |
| ----------------- | ---------- | -------------------------------------- |
| StartDate         | DateTime   | Gets or sets start date for settlement |
| EndDate           | DateTime   | Gets or sets end date for settlement   |
| EntryType         | EntryType? | Gets or sets entry type for settlement |

### Service Method

#### Purpose

Returns total transaction volume and count between given dates. Start and end dates are included.

| **Method**           | **Params**                   | **Return Type**                           |
| -------------------- | ---------------------------- | ----------------------------------------- |
| ProvisionSettlements | CashDepositSettlementOptions | PaparaSingleResult<CashDepositSettlement> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Possible Errors and Error Codes

| **Error Code** | **Error Description**                                        |
| -------------- | ------------------------------------------------------------ |
| 100            | User  not found.                                             |
| 101            | Merchant  information could not found.                       |
| 105            | Insufficient  funds.                                         |
| 107            | The  user exceeds the balance limit with this transaction.   |
| 111            | The  user exceeds the monthly transaction limit with this transaction |
| 112            | An  amount has been sent below the minimum deposit limit.    |
| 203            | The  user account is blocked.                                |
| 997            | The  authorization to make a cash deposit is not defined in your account. You  should contact your customer representative. |
| 998            | The  parameters you submitted are not in the expected format. Example: one of the  mandatory fields is not provided. |
| 999            | An  error occurred in the Papara system.                     |



# <a name="mass-payment">Mass Payment</a> 

This part is the technical integration statement prepared for merchants those want to distribute payments to their users quickly, safely and widely through Papara.

## Get Mass Payment

Returns information about the payment distribution process. To perform this operation use `GetMassPayment` method on `MassPayment` service. `Id` should be provided.

### Mass Payment Model

`MassPayment` class is used by mass payment service to match returning mass payment values from API.

| **Variable Name** | **Type** | **Description**                                         |
| ----------------- | -------- | ------------------------------------------------------- |
| MassPaymentId     | string   | Gets or sets mass payment ID                            |
| Id                | int?     | Gets or sets ID which is created after  payment is done |
| CreatedAt         | DateTime | Gets or sets created date                               |
| Amount            | decimal? | Gets or sets amount of payment                          |
| Currency          | int?     | Gets or sets currency. Values are “0”,  “1”, “2”, “3”   |
| Fee               | decimal? | Gets or sets fee                                        |
| ResultingBalance  | decimal? | Gets or sets resulting balance                          |
| Description       | string   | Gets or sets description                                |

### MassPaymentGetOptions

`MassPaymentGetOptions` is used by mass payment service for providing request parameters.

| **Variable Name** | **Type** | **Description**              |
| ----------------- | -------- | ---------------------------- |
| Id                | long     | Gets or sets mass payment ID |

### Service Method

#### Purpose

Returns mass payment information for authorized merchant.

| **Method**     | **Params**            | **Return Type**                 |
| -------------- | --------------------- | ------------------------------- |
| GetMassPayment | MassPaymentGetOptions | PaparaSingleResult<MassPayment> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Get Mass Payment By Reference

Returns information about the payment distribution process. To perform this operation use `GetMassPaymentByReference` method on `MassPayment` service. `Reference` should be provided.

### MassPaymentByReferenceOptions

`MassPaymentByIdOptions` is used by mass payment service for providing request parameters.

| **Variable Name** | **Type** | **Description**                            |
| ----------------- | -------- | ------------------------------------------ |
| Reference         | String   | Gets or sets mass payment reference number |

### Service Method

#### Purpose

Returns mass payment information for authorized merchant.

| **Method**                | **Params**                    | **Return Type**                 |
| ------------------------- | ----------------------------- | ------------------------------- |
| GetMassPaymentByReference | massPaymentByReferenceOptions | PaparaSingleResult<MassPayment> |

#### Usage

```java
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Create Mass Payment To Account Number

Send money to Papara number. To perform this operation use `PostMassPayment` method on `MassPayment` service. `AccountNumber`, `Amount` and `MassPaymentId` should be provided.

### MassPaymentToPaparaNumberOptions

`MassPaymentToPaparaNumberOptions` is used by mass payment service for providing request parameters.

| **Variable Name**  | **Type** | **Description**                                              |
| ------------------ | -------- | ------------------------------------------------------------ |
| AccountNumber      | string   | Gets or sets Papara account number. The  10-digit Papara number of the user who will receive the payment. It can be in  the format 1234567890 or PL1234567890. Before the Papara version transition,  the Papara number was called the wallet number.Old wallet numbers have been  changed to Papara number. Payment can be distributed to old wallet numbers. |
| ParseAccountNumber | int?     | Gets or sets parse account number. Parses  the account number to long type. In old papara integrations, account / wallet  number was made by starting with PL. The service was written in such a way  that it accepts numbers starting with PL, in order not to cause problems to  the member merchants that receive the papara number from their users. |
| Amount             | decimal? | Gets or sets amount. The amount of the  payment transaction. This amount will be transferred to the account of the  user who received the payment. This figure plus transaction fee will be  charged to the merchant account. |
| MassPaymentId      | string   | ets or sets mass payment ID. Unique value  sent by merchant to prevent erroneous repetition in payment transactions. If  a massPaymentId that was sent previously and succeeded is sent again with a new  request, the request will fail. |
| TurkishNationalId  | long     | Gets or sets national identity number.It  provides the control of the identity information sent by the user who will  receive the payment, in the Papara system. In case of a conflict of  credentials, the transaction will not take place. |
| Description        | string   | Gets or sets description. Description of  the transaction provided by the merchant. It is not a required field. If  sent, the customer sees in the transaction descriptions. |
| Currency | int | Gets or sets currency. Values are “0”,  “1”, “2”, “3” |

### Service Method

#### Purpose

Creates a mass payment to given account number for authorized merchant.

| **Method**      | **Params**                       | **Return Type**                 |
| --------------- | -------------------------------- | ------------------------------- |
| PostMassPayment | MassPaymentToPaparaNumberOptions | PaparaSingleResult<MassPayment> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Create Mass Payment To E-Mail Address

Send money to e-mail address registered in Papara. To perform this operation use `PostMassPaymentToEmail` method on `MassPayment` service. `Email`, `Amount` and `MassPaymentId` should be provided.

### MassPaymentToEmailOptions

`MassPaymentToEmailOptions` is used by mass payment service for providing request parameters.

| **Variable Name** | **Type** | **Description**                                              |
| ----------------- | -------- | ------------------------------------------------------------ |
| Email             | string   | Gets or sets e-mail address. Registered  email address of the user receiving the payment. |
| Amount            | decimal? | Gets or sets amount. The amount of the  payment transaction. This amount will be transferred to the account of the  user who received the payment. This figure plus transaction fee will be  charged to the merchant account. |
| MassPaymentId     | string   | Gets or sets mass payment ID. Unique value  sent by merchant to prevent erroneous repetition in payment transactions. If  a massPaymentId that was sent previously and succeeded is sent again with a  new request, the request will fail. |
| TurkishNationalId | long     | Gets or sets national identity number.It  provides the control of the identity information sent by the user who will  receive the payment, in the Papara system. In case of a conflict of  credentials, the transaction will not take place. |
| Description       | string   | Gets or sets description. Description of  the transaction provided by the merchant. It is not a required field. If  sent, the customer sees in the transaction descriptions. |
| Currency | int | Gets or sets currency. Values are “0”,  “1”, “2”, “3” |

### Service Method

#### Purpose

Creates a mass payment to given e-mail address for authorized merchant.

| **Method**             | **Params**                | **Return Type**                 |
| ---------------------- | ------------------------- | ------------------------------- |
| PostMassPaymentToEmail | MassPaymentToEmailOptions | PaparaSingleResult<MassPayment> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Create Mass Payment To Phone Number

Send money to phone number registered in Papara. To perform this operation use `PostMassPaymentToPhone` method on `MassPayment` service. `PhoneNumber`, `Amount` and `MassPaymentId` should be provided.

### MassPaymentToPhoneNumberOptions

`MassPaymentToPhoneNumberOptions` is used by mass payment service for providing request parameters.

| **Variable Name** | **Type** | **Description**                                              |
| ----------------- | -------- | ------------------------------------------------------------ |
| PhoneNumber       | string   | Gets or sets user's phone number. The  mobile number of the user who will receive the payment, registered in Papara.  It should contain a country code and start with + |
| Amount            | decimal? | Gets or sets amount. The amount of the  payment transaction. This amount will be transferred to the account of the  user who received the payment. This figure plus transaction fee will be  charged to the merchant account |
| MassPaymentId     | string   | Gets or sets mass payment ID. Unique value  sent by merchant to prevent erroneous repetition in payment transactions. If  a MassPaymentId that was sent previously and succeeded is sent again with a new  request, the request will fail |
| TurkishNationalId | long     | Gets or sets national identity number.It  provides the control of the identity information sent by the user who will  receive the payment, in the Papara system. In case of a conflict of  credentials, the transaction will not take place |
| Description       | string   | Gets or sets description. Description of  the transaction provided by the merchant. It is not a required field. If  sent, the customer sees in the transaction descriptions |
| Currency | int | Gets or sets currency. Values are “0”,  “1”, “2”, “3” |

### Service Method

#### Purpose

Creates a mass payment to given phone number for authorized merchant.

| **Method**             | **Params**                      | **Return Type**                 |
| ---------------------- | ------------------------------- | ------------------------------- |
| PostMassPaymentToPhone | MassPaymentToPhoneNumberOptions | PaparaSingleResult<MassPayment> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Possible Errors and Error Codes

| **Error Code** | **Error Description**                                        |
| -------------- | ------------------------------------------------------------ |
| 100            | User not found.                                              |
| 105            | Insufficient  funds                                          |
| 107            | Receiver exceeds balance limit. The highest possible balance for simple accounts is  750 TL. |
| 111            | Receiver exceeds monthly transaction limit. Simple accounts can receive payments from a total of 2000 TL of defined resources per month. |
| 133            | MassPaymentID was used recently.                             |
| 997            | You  are not authorized to distribute payments. You can contact your customer representative and request a payment distribution definition to your merchant  account. |
| 998            | The  parameters you submitted are not in the expected format. Example: Customer number less than 10 digits. In this case, the error message contains details of the format error. |
| 999            | An error  occurred in the Papara system.                     |


# <a name="recurring-mass-payment">Recurring Mass Payment</a>

This section is the technical integration document prepared for the merchants who want to distribute payments to their users in a fast, secure and widespread through Papara.

## Recurring Mass Payment Model

`RecurringMassPayment` class is used by recurring mass payment service to match returning recurring mass payment values from API.

| **Variable Name** | **Type** | **Description**                                                             |
| ----------------- | -------- | --------------------------------------------------------------------------- |
| MerchantId        | string   | Gets or sets merchant id.                                                   |
| UserId            | string   | Gets or sets user id.                                                       |
| Period            | int      | Gets or sets period. Values are "0" (Monthly), "1" (Weekly), "2" (Daily).   |
| ExecutionDay      | int      | Gets or sets ...th day of period. (Weeks start with Monday).                |
| AccountNumber     | int      | Gets or sets account number.                                                |
| Message           | string   | Gets or sets message.                                                       |
| Amount            | decimal  | Gets or sets amount.                                                        |
| Currency          | Currency | Gets or sets currency.Values are “0” (TRY), “1” (USD), “2” (EUR), “3” (GBP).|

## Create Recurring Mass Payment To Account Number

To perform this operation use `CreateRecurringMassPaymentWithAccountNumber` method on `MassPayment` service. `AccountNumber`, `Amount`, `ExecutionDay`, `Description`  and `Period` should be provided.

### RecurringMassPaymentToAccountNumberOptions

`RecurringMassPaymentToAccountNumberOptions` is used by mass payment service for providing request parameters.

| **Variable Name** | **Type** | **Description**                                                             |
| ----------------- | -------- | --------------------------------------------------------------------------- |
| AccountNumber     | string   | Gets or sets Papara account number. The 10-digit Papara number of the user who will receive the payment. It can be in the format 1234567890 or PL1234567890. Before the Papara version transition, the Papara number was called the wallet number.Old wallet numbers have been changed to Papara number. Payment can be distributed to old wallet numbers.                                                                                                     |
| Amount            | decimal  | Gets or sets amount. The amount of the payment transaction. This amount will be transferred to the account of the user who received the payment. This figure plus transaction fee will be charged to the merchant account.                                                |
| TurkishNationalId | long?    | Gets or sets national identity number.It provides the control of the identity information sent by the user who will receive the payment, in the Papara system. In case of a conflict of credentials, the transaction will not take place.                                   |
| Currency          | Currency? | Gets or sets currency.Values are “0” (TRY), “1” (USD), “2” (EUR), “3” (GBP).|
| Period            | int      | Gets or sets period. Values are "0" (Monthly), "1" (Weekly), "2" (Daily).   |
| ExecutionDay      | int      | Gets or sets ...th day of period. (Weeks start with Monday).                |
| Description       | string   | Gets or sets description. Description of the transaction provided by the merchant. It is not a required field. If sent, the customer sees in the transaction descriptions.                                                                                                |

### Service Method

#### Purpose

Creates a recurring mass payment to given account number for authorized merchant.

| **Method**      | **Params**                       | **Return Type**                 |
| --------------- | -------------------------------- | ------------------------------- |
| CreateRecurringMassPaymentWithAccountNumber | RecurringMassPaymentToAccountNumberOptions | PaparaSingleResult<RecurringMassPayment> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
};

var massPaymentService = new MassPaymentService(requestOptions);
var recurringMassPaymentServiceResult = massPaymentService.CreateRecurringMassPaymentWithAccountNumber(new RecurringMassPaymentToAccountNumberOptions
{
    AccountNumber = "ACCOUNT_NUMBER",
    Amount = 99.99,
    TurkishNationalId = 12345678901, //optional
    Currency = 0, //optional
    Period = 1,
    ExecutionDay =1,
    Description = "test"
});

if (!recurringMassPaymentServiceResult.Succeeded)
{
    throw new Exception(recurringMassPaymentServiceResult.Error.Message);
}

return recurringMassPaymentServiceResult;
```
## Create Recurring Mass Payment To Email

To perform this operation use `CreateRecurringMassPaymentWithEmail` method on `MassPayment` service. `Email`, `Amount`, `TurkishNationalId`, `Period`, `Currency`, `ExecutionDay` and `Description` should be provided.

### RecurringMassPaymentToEmailOptions

`RecurringMassPaymentToEmailOptions` is used by mass payment service for providing request parameters.

| **Variable Name** | **Type** | **Description**                                                             |
| ----------------- | -------- | --------------------------------------------------------------------------- |
| Email             | string   | Gets or sets e-mail address. Registered email address of the user receiving the payment.                                                                                                     |
| Amount            | decimal  | Gets or sets amount. The amount of the payment transaction. This amount will be transferred to the account of the user who received the payment. This figure plus transaction fee will be charged to the merchant account.                                                |
| TurkishNationalId | long?    | Gets or sets national identity number.It provides the control of the identity information sent by the user who will receive the payment, in the Papara system. In case of a conflict of credentials, the transaction will not take place.                                   |
| Currency          | Currency | Gets or sets currency.Values are “0” (TRY), “1” (USD), “2” (EUR), “3” (GBP).|
| Period            | int      | Gets or sets period. Values are "0" (Monthly), "1" (Weekly), "2" (Daily).   |
| ExecutionDay      | int      | Gets or sets ...th day of period. (Weeks start with Monday).                |
| Description       | string   | Gets or sets description. Description of the transaction provided by the merchant. It is not a required field. If sent, the customer sees in the transaction descriptions.    

### Service Method

#### Purpose

Creates a recurring mass payment to given email address for authorized merchant.

| **Method**      | **Params**                       | **Return Type**                 |
| --------------- | -------------------------------- | ------------------------------- |
| CreateRecurringMassPaymentWithEmail | RecurringMassPaymentToEmailOptions | PaparaSingleResult<RecurringMassPayment> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
};

var massPaymentService = new MassPaymentService(requestOptions);
var recurringMassPaymentServiceResult = massPaymentService.CreateRecurringMassPaymentWithEmail(new RecurringMassPaymentToEmailOptions
{
    Email = "example@example.com",
    Amount = 99.99,
    TurkishNationalId = 12345678901, //optional
    Currency = 0, //optional
    Period = 1,
    ExecutionDay =1,
    Description = "test"
});

if (!recurringMassPaymentServiceResult.Succeeded)
{
    throw new Exception(recurringMassPaymentServiceResult.Error.Message);
}

return recurringMassPaymentServiceResult;
```

## Create Recurring Mass Payment To Phone Number

To perform this operation use `CreateRecurringMassPaymentWithPhoneNumber` method on `MassPayment` service. `PhoneNumber`, `Amount`, `TurkishNationalId`, `Period`, `Currency`, `ExecutionDay` and `Description` should be provided.

### RecurringMassPaymentToPhoneNumberOptions

`RecurringMassPaymentToPhoneNumberOptions` is used by mass payment service for providing request parameters.

| **Variable Name** | **Type** | **Description**                                                             |
| ----------------- | -------- | --------------------------------------------------------------------------- |
| PhoneNumber       | string   | Gets or sets user's phone number. The mobile number of the user who will receive the payment, registered in Papara. It should contain a country code and start with +                                                                                        |
| Amount            | decimal  | Gets or sets amount. The amount of the payment transaction. This amount will be transferred to the account of the user who received the payment. This figure plus transaction fee will be charged to the merchant account.                                                |
| TurkishNationalId | long?    | Gets or sets national identity number.It provides the control of the identity information sent by the user who will receive the payment, in the Papara system. In case of a conflict of credentials, the transaction will not take place.                                   |
| Currency          | Currency | Gets or sets currency.Values are “0” (TRY), “1” (USD), “2” (EUR), “3” (GBP).|
| Period            | int      | Gets or sets period. Values are "0" (Monthly), "1" (Weekly), "2" (Daily).   |
| ExecutionDay      | int      | Gets or sets ...th day of period. (Weeks start with Monday).                |
| Description       | string   | Gets or sets description. Description of the transaction provided by the merchant. It is not a required field. If sent, the customer sees in the transaction descriptions.    

### Service Method

#### Purpose

Creates a recurring mass payment to given phone number for authorized merchant.

| **Method**      | **Params**                       | **Return Type**                 |
| --------------- | -------------------------------- | ------------------------------- |
| CreateRecurringMassPaymentWithPhoneNumber | RecurringMassPaymentToPhoneNumberOptions | PaparaSingleResult<RecurringMassPayment> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
};

var massPaymentService = new MassPaymentService(requestOptions);
var recurringMassPaymentServiceResult = massPaymentService.CreateMassPaymentWithPhoneNumber(new RecurringMassPaymentToEmailOptions
{
    PhoneNumber = "+905012345678",
    Amount = 99.99,
    TurkishNationalId = 12345678901, //optional
    Currency = 0, //optional
    Period = 1,
    ExecutionDay =1,
    Description = "test"
});

if (!recurringMassPaymentServiceResult.Succeeded)
{
    throw new Exception(recurringMassPaymentServiceResult.Error.Message);
}

return recurringMassPaymentServiceResult;
```

## Possible Errors and Error Codes

| **Error Code** | **Error Description**                                        |
| -------------- | ------------------------------------------------------------ |
| 100            | User not found.                                              |
| 105            | Insufficient  funds                                          |
| 107            | Receiver exceeds balance limit. The highest possible balance for simple accounts is  750 TL. |
| 111            | Receiver exceeds monthly transaction limit. Simple accounts can receive payments from a total of 2000 TL of defined resources per month. |
| 133            | MassPaymentID was used recently.                             |
| 398            | The transaction could not be performed because the user you want to send foreign currency to does not have a verified account.|
| 997            | You  are not authorized to distribute payments. You can contact your customer representative and request a payment distribution definition to your merchant  account. |
| 998            | The  parameters you submitted are not in the expected format. Example: Customer number less than 10 digits. In this case, the error message contains details of the format error. |
| 999            | An error  occurred in the Papara system.                     |

# <a name="payments">Payments</a> 

Payment service will be used for getting, creating or listing payments and refunding. Before showing the payment button to users, the merchant must create a payment transaction on Papara. Payment records are time dependent. Transaction records that are not completed and paid by the end user are deleted from Papara system after 1 hour. Completed payment records are never deleted and can always be queried with the API.

## Get Payment

Returns payment information. To perform this operation use `GetPayment` method on `Payment` service. `Id` should be provided.

### Payment Model

`Payment` class is used by payment service to match returning payment values from API.

| **Variable Name**        | **Type** | **Description**                                              |
| ------------------------ | -------- | ------------------------------------------------------------ |
| Merchant                 | Account  | Gets or sets merhcant                                        |
| Id                       | string   | Gets or sets ID                                              |
| CreatedAt                | DateTime | Gets or sets created date                                    |
| MerchantId               | string   | Gets or sets merchant ID                                     |
| UserId                   | string   | Gets or sets user ID                                         |
| PaymentMethod            | int      | Gets or sets payment Method.  0 -  User completed transaction with existing Papara balance  1 -  User completed the transaction with a debit / credit card that was previously  defined.  2 -  User completed transaction via mobile payment. |
| PaymentMethodDescription | string   | Gets or sets payment method description                      |
| ReferenceId              | string   | Gets or sets referance ID                                    |
| OrderDescription         | string   | Gets or sets order description                               |
| Status                   | int      | Gets or sets status.  0 -  Awaiting, payment is not done yet.  1 -  Payment is done, transaction is completed.  2 -  Transactions is refunded by merchant. |
| StatusDescription        | string   | Gets or sets status description                              |
| Amount                   | decimal  | Gets or sets amount                                          |
| Feed                     | decimal  | Gets or sets fee                                             |
| Currency                 | int      | Gets or sets currency. Values are “0”,  “1”, “2”, “3”        |
| NotificationUrl          | string   | Gets or sets notification URL                                |
| NotificationDone         | bool?    | Gets or sets if notification was made                        |
| RedirectUrl              | string   | Gets or sets redirect URL                                    |
| PaymentUrl               | string   | Gets or sets payment URL                                     |
| MerchantSecretKey        | string   | Gets or sets merchant secret key                             |
| ReturningRedirectUrl     | string   | Gets or sets returning Redirect URL                          |
| TurkishNationalId        | long     | Gets or sets national identity number                        |

### PaymentGetOptions

`PaymentGetOptions` will be used as parameter while acquiring payment information.

| **Variable Name** | **Type** | **Description**                |
| ----------------- | -------- | ------------------------------ |
| Id                | string   | Gets or sets unique payment ID |

### Service Method

#### Purpose

Returns payment and balance information for authorized merchant.

| **Method** | **Params**        | **Return Type**             |
| ---------- | ----------------- | --------------------------- |
| GetPayment | PaymentGetOptions | PaparaSingleResult<Payment> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Get Payment By Payment Reference

Returns payment information. To perform this operation use `GetPaymentByReference` method on `Payment` service. `ReferenceId` should be provided.

### PaymentByReferenceOptions

`PaymentGetOptions` will be used as parameter while acquiring payment information.

| **Variable Name** | **Type** | **Description**                |
| ----------------- | -------- | ------------------------------ |
| ReferenceId       | string   | Gets or sets unique payment ID |

### Service Method

#### Purpose

Returns payment and balance information for authorized merchant.

| **Method**            | **Params**                | **Return Type**             |
| --------------------- | ------------------------- | --------------------------- |
| GetPaymentByReference | PaymentByReferenceOptions | PaparaSingleResult<Payment> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Create Payment

Creates a new payment record. To perform this operation use `CreatePayment` method on `Payment` service. `Amount`, `ReferenceId`, `OrderDescription`, `NotificationUrl` and `RedirectUrl` should be provided.

### PaymentCreateOptions

`PaymentCreateOptions` is used by payment service for providing request parameters.

| **Variable Name** | **Type** | **Description**                                              |
| ----------------- | -------- | ------------------------------------------------------------ |
| Amount            | decimal  | Gets or sets amount. The amount of the  payment transaction. Exactly this amount will be taken from the account of  the user who made the payment, and this amount will be displayed to the user  on the payment screen. Amount field can be minimum 1.00 and maximum 500000.00 |
| ReferenceId       | string   | Gets or sets reference ID. Reference  information of the payment transaction in the merchant system. The  transaction will be returned to the merchant without being changed in the  result notifications as it was sent to Papara. Must be no more than 100  characters. This area does not have to be unique and Papara does not make  such a check |
| OrderDescription  | string   | Gets or sets order description.  Description of the payment transaction. The sent value will be displayed to  the user on the Papara checkout page. Having a description that accurately  identifies the transaction initiated by the user, will increase the chance of  successful payment |
| NotificationUrl   | string   | Gets or sets notification URL. The URL to  which payment notification requests (IPN) will be sent. With this field, the  URL where the POST will be sent to the payment merchant must be sent. To the  URL sent with "notificationUrl", Papara will send a payment object  containing all information of the payment with an HTTP POST request  immediately after the payment is completed. Make sure that the payment notification (IPN) coming to "NotificationURL" comes from Papara's IP addresses. You can check the payment by calling HTTP GET /payments API method with the "id" field in the submitted JSON. If the merchant returns 200 OK to  this request, no notification will be made again. If the merchant does not  return 200 OK to this notification, Papara will continue to make payment  notification (IPN) requests for 24 hours until the merchant returns to 200 OK |
| RedirectUrl       | string   | Gets or sets redirect URL. URL to which  the user will be redirected at the end of the process |
| TurkishNationalId | long     | Gets or sets national identity number.It  provides the control of the identity information sent by the user who will  receive the payment, in the Papara system. In case of a conflict of  credentials, the transaction will not take place |
| Currency | int | Gets or sets currency. Values are “0”,  “1”, “2”, “3” |

### Important Warning

Make sure that the payment notification (IPN) coming to "NotificationURL" comes from Papara's IP addresses. You can check the payment by calling HTTP GET /payments API method with the "id" field in the submitted JSON.

### Service Method

#### Purpose

Creates a payment for authorized merchant.

| **Method**    | **Params**           | **Return Type**             |
| ------------- | -------------------- | --------------------------- |
| CreatePayment | PaymentCreateOptions | PaparaSingleResult<Payment> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

###  Validating Payment Result 

Following the user's successful completion of the transaction **before the user is directed to the merchant**, Papara makes a **HTTP POST** request to the `notificationUrl` sent by the merchant with the payment request.

In the `body` part of the request, there will be a JSON object with the same structure as the `data` object of the return value creating a payment request. Sample:

```json
{
    "merchantId": "123-4564-8484",
    "userId": "123-987-654",
    "paymentMethod": 1,
    "paymentMethodDescription": "Credit/Debit Card",
    "referenceId": "Merchant Reference",
    "orderDescription": "Description that will be displayed to user on payment page",
    "status": 1,
    "statusDescription": "Completed",    
    "amount": 99.99,
    "fee": 1.98,
    "currency": "TRY",
    "notificationUrl": "https://www.papara.com/notification",
    "notificationDone": false,
    "redirectUrl": "https://www.papara.com/userredirect",
    "merchantSecretKey": "Secret key on the merchant panel",
    "paymentUrl": "www.papara.com/pid?6666-5555-ABCD",
    "returningRedirectUrl": "",
    "id": "6666-5555-ABCD",
    "createdAt": "2017-06-09T06:26:15.100Z",
    "turkishNationalId": 12345678901,
}
```

## Refund 

Refunds a completed payment of the merchant with the provided payment ID .To perform this operation use `Refund` method on `Payment` service. `Id` should be provided.

### PaymentRefundOptions

`PaymentRefundOptions` is used by payment service for providing request parameters.

| **Variable Name** | **Type** | **Description**         |
| ----------------- | -------- | ----------------------- |
| Id                | string   | Gets or sets payment ID |

### Service Method

#### Purpose

Creates a refund for a completed payment for authorized merchant.

| **Method** | **Params**           | **Return Type**     |
| ---------- | -------------------- | ------------------- |
| Refund     | PaymentRefundOptions | PaparaServiceResult |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## List Payments

Lists the completed payments of the merchant in a sequential order. To perform this operation use `List` method on `Payment` service. `PageIndex` and `PageItemCount` should be provided.

### PaymentListItem

`PaymentListItem` class is used by payment service to match returning completed payment list values list API.

| **Variable Name**        | **Type** | **Description**                                              |
| ------------------------ | -------- | ------------------------------------------------------------ |
| Id                       | string   | Gets or sets payment ID                                      |
| CreatedAt                | DateTime | Gets or sets created date                                    |
| MerchantId               | string   | Gets or sets merchant ID                                     |
| UserId                   | string   | Gets or sets user ID                                         |
| PaymentMethod            | int?     | Gets or sets payment Method.  0 -  User completed transaction with existing Papara balance  1 -  User completed the transaction with a debit / credit card that was previously  defined.  2 -  User completed transaction via mobile payment. |
| PaymentMethodDescription | string   | Gets or sets payment method description                      |
| ReferenceId              | string   | Gets or sets reference ID                                    |
| OrderDescription         | string   | Gets or sets order description                               |
| Status                   | int?     | Gets or sets status.  0 -  Awaiting, payment is not done yet.  1 -  Payment is done, transaction is completed.  2 -  Transactions is refunded by merchant. |
| StatusDescription        | string   | Gets or sets status description                              |
| Amount                   | decimal? | Gets or sets amount                                          |
| Fee                      | decimal? | Gets or sets fee                                             |
| Currency                 | int?     | Gets or sets currency. Values are “0”,  “1”, “2”, “3”        |
| NotificationUrl          | string   | Gets or sets notification URL                                |
| NotificationDone         | bool?    | Gets or sets if notification was made                        |
| RedirectUrl              | string   | Gets or sets redirect URL                                    |
| PaymentUrl               | string   | Gets or sets payment URL                                     |
| MerchantSecretKey        | string   | Gets or sets merchant secret key                             |
| ReturningRedirectUrl     | string   | Gets or sets returning Redirect URL                          |
| TurkishNationalId        | long     | Gets or sets national identity number                        |

### PaymentListOptions

`PaymentListOptions` is used by payment service for providing request parameters

| **Variable Name** | **Type** | **Description**                                              |
| ----------------- | -------- | ------------------------------------------------------------ |
| PageIndex         | int      | Gets or sets page index. It is the index  number of the page that is wanted to display from the pages calculated on the  basis of the number of records (pageItemCount) desired to be displayed on a  page. Note: the first page is always 1 |
| PageItemCount     | Int      | Gets or sets page item count. The number  of records that are desired to be displayed on a page |

### Service Method

#### Purpose

Returns a list of completed payments sorted by newest to oldest for authorized merchant.

| **Method** | **Params**         | **Return Type**                   |
| ---------- | ------------------ | --------------------------------- |
| List       | PaymentListOptions | PaparaListResult<PaymentListItem> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Possible Errors and Error Codes

| **Error Code** | **Error Description**                                        |
| -------------- | ------------------------------------------------------------ |
| 997            | You  are not authorized to accept payments. You should contact your customer  representative. |
| 998            | The  parameters you submitted are not in the expected format. Example: one of the  mandatory fields is not provided. |
| 999            | An  error occurred in the Papara system.                     |



# <a name="validation">Validation</a> 

Validation service will be used for validating an end user. Validation can be performed by account number, e-mail address, phone number, national identity number.

## Validate By Id

It is used to validate users with Papara UserId. To perform this operation use `ValidateById` method on `Validation` service. `UserId` should be provided.

### Validation Model           

`Validation` class is used by validation service to match returning user value from API

| **Variable Name** | **Type** | **Description**                            |
| ----------------- | -------- | ------------------------------------------ |
| UserId            | string   | Gets or sets unique User ID                |
| FirstName         | string   | Gets or sets user first name               |
| LastName          | string   | Gets or sets user last name                |
| Email             | string   | Gets or sets user e-mail address           |
| PhoneNumber       | string   | Gets or sets user phone number             |
| Tckn              | Long     | Gets or sets user national identity number |
| AccountNumber     | int?     | Gets or sets user account number           |

### ValidationByIdOptions 

`ValidationByIdOptions` is used by validation service for providing request parameters.

| **Variable Name** | **Type** | **Description**             |
| :---------------- | -------- | --------------------------- |
| UserId            | string   | Gets or sets Papara User ID |

### Service Method

#### Purpose

Returns end user information for validation by given user ID.

| **Method**   | **Params**            | **Return Type**                |
| ------------ | --------------------- | ------------------------------ |
| ValidateById | ValidationByIdOptions | PaparaSingleResult<Validation> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Validate By Account Number

It is used to validate users with Papara account number. To perform this operation use `ValidateByAccountNumber` method on `Validation` service. `AccountNumber` should be provided.

### ValidationByAccountNumberOptions

`ValidationByAccountNumberOptions` is used by validation service for providing request parameters

| **Variable Name** | **Type** | **Description**                    |
| ----------------- | -------- | ---------------------------------- |
| AccountNumber     | long     | Gets or sets Papara account number |

### Service Method

#### Purpose

Returns end user information for validation by given user account number.

| **Method**              | **Params**                       | **Return Type**                |
| ----------------------- | -------------------------------- | ------------------------------ |
| ValidateByAccountNumber | ValidationByAccountNumberOptions | PaparaSingleResult<Validation> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Validate By Phone Number

It is used to validate users with phone number registered in Papara. To perform this operation use `ValidateByPhoneNumber` method on `Validation` service. `PhoneNumber` should be provided.

### ValidationByPhoneNumberOptions

`ValidationByPhoneNumberOptions` is used by validation service for providing request parameters

| **Variable Name** | **Type** | **Description**                                |
| ----------------- | -------- | ---------------------------------------------- |
| PhoneNumber       | string   | Gets or sets phone number registered to Papara |

### Service Method

#### Purpose

Returns end user information for validation by given user phone number.

| **Method**            | **Params**                     | **Return Type**                |
| --------------------- | ------------------------------ | ------------------------------ |
| ValidateByPhoneNumber | ValidationByPhoneNumberOptions | PaparaSingleResult<Validation> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Validate By E-Mail Address

It is used to validate users with e-mail address registered in Papara. To perform this operation use `ValidateByEmail` method on `Validation` service. `Email` should be provided.

### ValidationByEmailOptions

`ValidationByEmailOptions` is used by validation service for providing request parameters

| **Variable Name** | **Type** | **Description**                                  |
| ----------------- | -------- | ------------------------------------------------ |
| Email             | string   | Gets or sets e-mail address registered to Papara |

### Service Method

#### Purpose

Returns end user information for validation by given user e-mail address

| **Method**      | **Params**               | **Return Type**                |
| --------------- | ------------------------ | ------------------------------ |
| ValidateByEmail | ValidationByEmailOptions | PaparaSingleResult<Validation> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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

## Validate By National Identity Number

It is used to validate users with national identity number registered in Papara. To perform this operation use `ValidateByTckn` method on `Validation` service. `Tckn` should be provided.

### ValidationByTcknOptions

`ValidationByPhoneNumberOptions` is used by validation service for providing request parameters.

| **Variable Name** | **Type** | **Description**                       |
| ----------------- | -------- | ------------------------------------- |
| Tckn              | long     | Gets or sets national identity number |

### Service Method

#### Purpose

Returns end user information for validation by given user national identity number

| **Method**     | **Params**              | **Return Type**                |
| -------------- | ----------------------- | ------------------------------ |
| ValidateByTckn | ValidationByTcknOptions | PaparaSingleResult<Validation> |

#### Usage

```csharp
var requestOptions = new RequestOptions
{
    ApiKey = "YOUR_API_KEY",
    IsTest = true //Connection configuration for test or production environment.
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



# <a name="response-types">Response Types</a>

This part contains technical information about return values from API.

## PaparaSingleResult

Papara Single Result type. Handles object data types sending to and returning from API.

| **Variable Name** | **Type** | **Description**                                              |
| :---------------- | -------- | ------------------------------------------------------------ |
| Data              | TEntity  | Generic object return type. Returns the  value of the given object type |

## PaparaListResult

Papara List type. Handles list data types sending to and returning from API.

| **Variable Name** | **Type**                    | **Description**                                              |
| ----------------- | --------------------------- | ------------------------------------------------------------ |
| Data              | PaparaPagingResult<TEntity> | Generic list return type. Returns the list  of the given object type |

## PaparaPagingResult      

Papara Paging type. Handles paging data types sending to and returning from API.

| **Variable Name** | **Type**      | **Description**                                              |
| ----------------- | ------------- | ------------------------------------------------------------ |
| Items             | List<TEntity> | Gets or sets items returning from API.  Returns the list of the given object type |
| Page              | int           | Gets or sets page number                                     |
| PageItemCount     | int           | Gets or sets listed item counts on a page                    |
| TotalItemCount    | int           | Gets or sets total item count in a request  or response      |
| TotalPageCount    | int           | Gets or sets total page count in a request  or response      |
| PageSkip          | int           | Gets or sets how many pages to be skipped                    |

## PaparaArrayResult

Papara Array type. Handles array data types sending to and returning from API.

| **Variable Name** | **Type**  | **Description**                                              |
| ----------------- | --------- | ------------------------------------------------------------ |
| Data              | TEntity[] | Generic array return type. Returns the array  of the given object type |

## PaparaServiceResult

Papara Service Result type. Handles response data types returning from API.

| **Variable Name** | **Type**             | **Description**                                              |
| ----------------- | -------------------- | ------------------------------------------------------------ |
| Succeeded         | bool                 | Gets or sets a value indicating whether  operation resulted successfully or not |
| Error             | ServiceResultError   | Gets or sets a value indicating whether  operation failed or not |
| Result            | ServiceResultSuccess | Gets or sets success result                                  |

## ServiceResultError

Papara Service Error Result type. Handles error responses returning from API.

| **Variable Name** | **Type** | **Description**             |
| ----------------- | -------- | --------------------------- |
| Message           | string   | Gets or sets error messages |
| Code              | int      | Gets or sets error codes    |

## ServiceResultSuccess

Papara Service Success Result type. Handles success responses returning from API.

| **Variable Name** | **Type** | **Description**               |
| ----------------- | -------- | ----------------------------- |
| Message           | string   | Gets or sets success messages |
| Code              | int      | Gets or sets success codes    |

 
