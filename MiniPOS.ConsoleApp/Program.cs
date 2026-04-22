using System.Net.Http.Json;

// API လိပ်စာ (သင့် API port ကို အစားထိုးပါ)
var baseUrl = "http://localhost:7110/api/Product";

using var client = new HttpClient();

Console.WriteLine("Welcome to MiniPOS Console!");
Console.WriteLine("Commands: read  | create | update | delete | exit");

while (true)
{
    Console.Write("\nEnter command: ");
    string command = Console.ReadLine()?.ToLower();

    if (command == "exit") break;

    switch (command)
    {
        case "create":
            await CreateProduct(client);
            break;
        case "read":
            await ReadProducts(client);
            break;
        case "update":
            await UpdateProduct(client);
            break;
        case "delete":
            await DeleteProduct(client);
            break;
        default:
            Console.WriteLine("Unknown command!");
            break;
    }
}

//Read Function

async Task ReadProducts(HttpClient client)
{
    var products = await client.GetFromJsonAsync<List<Product>>(baseUrl);
    Console.WriteLine($"{"ID",-5} | {"Code",-10} | {"Name",-20} | {"Price",-10}");
    Console.WriteLine(new string('-', 50));
    foreach (var p in products)
    {
        Console.WriteLine($"{p.ProductId} | {p.ProductCode} | {p.ProductName} | {p.Price}");
    }
}

// --- Create Function ---
async Task CreateProduct(HttpClient client)
{
    try
    {
        Console.Write("Enter Code: "); string code = Console.ReadLine();
        Console.Write("Enter Name: "); string name = Console.ReadLine();
        Console.Write("Enter Price: "); decimal price = decimal.Parse(Console.ReadLine());

        var newProd = new {
            productCode = code,
            productName = name,
            price = price,
            deleteFlag = false };
        var response = await client.PostAsJsonAsync(baseUrl, newProd);

        if (response.IsSuccessStatusCode) Console.WriteLine("Product Created!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

// --- Update Function ---
async Task UpdateProduct(HttpClient client)
{
    Console.Write("Enter ProductId to Update: "); int id = int.Parse(Console.ReadLine());
    Console.Write("Enter New Code: "); string code = Console.ReadLine() ;
    Console.Write("Enter New Name: "); string name = Console.ReadLine();
    Console.Write("Enter New Price: "); string price = Console.ReadLine();


    var updateProd = new {
        productId = id,
        productName = name,
        productCode = code,
        price = price,
        deleteFlag = false };

    var response = await client.PatchAsJsonAsync($"{baseUrl}/{id}", updateProd);

    if (response.IsSuccessStatusCode) Console.WriteLine("Product Updated!");
    else
    {
        // ဘာကြောင့် BadRequest ဖြစ်လဲဆိုတာကို ဤနေရာမှာ ဖတ်ပါ
        string error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Update Failed: {response.StatusCode}");
        Console.WriteLine($"Details: {error}");
    }
}

// --- Delete Function ---
async Task DeleteProduct(HttpClient client)
{
    Console.Write("Enter ID to Delete: "); int id = int.Parse(Console.ReadLine());
    var response = await client.DeleteAsync($"{baseUrl}/{id}");

    if (response.IsSuccessStatusCode) Console.WriteLine("Product Deleted!");
}

public class Product
{
    public int ProductId { get; set; }
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
}

// try
// {
//     Console.WriteLine("Fetching data from API...");

//     // API ကနေ Data ဆွဲထုတ်ခြင်း
//     // var products = await client.GetFromJsonAsync<List<dynamic>>(baseUrl);

//     // Console ထဲမှာ ပြသခြင်း
//     Console.WriteLine($"{"ID",-5} | {"Code",-10} | {"Name",-20} | {"Price",-10}");
//     Console.WriteLine(new string('-', 50));

//     // List<dynamic> အစား List<Product> ကို သုံးပါ
//     var products = await client.GetFromJsonAsync<List<Product>>(baseUrl);

//     foreach (var p in products)
//     {
//         // အခုဆိုရင် စာလုံးအကြီး/အသေး ပြဿနာ မရှိတော့ပါ
//         Console.WriteLine($"{p.ProductId} | {p.ProductCode} | {p.ProductName} | {p.Price}");
//     }
// }
// catch (Exception ex)
// {
//     Console.WriteLine($"Error: {ex.Message}");
// }

// // Program.cs ၏ အောက်ဆုံးတွင် ထည့်ပါ
// public class Product
// {
//     public int ProductId { get; set; }
//     public string ProductCode { get; set; }
//     public string ProductName { get; set; }
//     public decimal Price { get; set; }
// }


// // API ကို POST request နဲ့ အသစ်ထည့်ခြင်း
// var newProduct = new { 
//     ProductCode = "P008", 
//     ProductName = "Americano", 
//     Price = 3200.00, 
//     DeleteFlag = false 
// };

// var response = await client.PostAsJsonAsync(baseUrl, newProduct);

// if (response.IsSuccessStatusCode)
//     Console.WriteLine("Product created successfully!");


// // API ကို PATCH request နဲ့ ပြင်ဆင်ခြင်း
// var id = 1002;
// var updatedProduct = new
// {
//     ProductCode = "P008",
//     ProductName = "Updated Espresso Name", // နာမည်အသစ်
//     Price = 3200.00,                        // ဈေးနှုန်းအသစ်
//     DeleteFlag = false
// };


// var url = $"http://localhost:7110/api/Product/{id}"; // Swagger URL နဲ့ တူအောင် စစ်ပါ

// // ၂။ API သို့ PUT Request ပို့ခြင်း
// var response = await client.PatchAsJsonAsync(url, updatedProduct);

// // ၃။ ရလဒ်စစ်ဆေးခြင်း
// if (response.IsSuccessStatusCode)
// {
//     Console.WriteLine($"Product ID {id} has been updated successfully!");
// }
// else
// {
//     Console.WriteLine($"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
// }