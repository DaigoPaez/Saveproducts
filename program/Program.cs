using System.IO; 
 
class Product 
{ 
    public string code; 
    public string description; 
    public decimal price; 
    public Product(string code, string description, decimal price) 
    { 
        this.code = code; 
        this.description = description; 
        this.price = price; 
    } 
} 
 
class ProductDB 
{ 
    public static void SaveProduct(List<Product> products) 
    { 
        StreamWriter textOut = new StreamWriter( new FileStream("productos.txt", FileMode.Create, FileAccess.Write));  
        foreach(var p in products) 
        { 
            textOut.WriteLine($"{p.code}|{p.description}|{p.price}"); 
        } 
        textOut.Close(); 
    } 
 
    public static List<Product> GetProducts() 
    { 
        List<Product> productos = new List<Product>(); 
        StreamReader textIn = new StreamReader(new FileStream("products.txt", FileMode.Open, FileAccess.Read)); 
        while(textIn.Peek() != -1) 
        { 
            string? row = textIn.ReadLine(); 
            string[] columns = row.Split("|"); 
            productos.Add(new Product(columns[0] , columns[1] , Decimal.Parse(columns[2]))); 
        } 
        return productos;  
    } 
   
public static void SaveProductsBin (List<Product> products) 
{ 
FileStream fs = new FileStream("archivo.bin", FileMode.Create, FileAccess.Write); 
BinaryWriter binOut = new BinaryWriter(fs); 
foreach (Product p in products) 
{ 
binOut.Write(p.code); 
binOut.Write(p.description); 
binOut.Write(p.price); 
} 
binOut.Close(); 
} 
 
public static List<Product> GetProductsBin()
{
    FileStream fs = new FileStream("archivo.bin", FileMode.Open, FileAccess.Read);
    BinaryReader binIn = new BinaryReader(fs);
    List<Product> products = new();
    while(binIn.PeekChar() =! -1)
    {
        string code = binIn.ReadString();
        string description = binIn.ReadString();
        decimal price = binIn.ReadDecimal();
        products.Add(new Product(code,description,price)); 
    }
    binIn.Close();
}
} 

class Program 
{ 
    static void Main() 
    { 
        List<Product> productos = ProductDB.GetProducts(); 
        productos.Add(new Product("codigo","desc",1.0m)); 
        productos.Add(new Product("codigo2","desc2",2.0m)); 
        productos.Add(new Product("codigo3","desc3",3.0m)); 
        //ProductDB.SaveProduct(productos); 
        List<Product> productos = ProductDB.GetProductsBin(); 
        foreach(var P in productos)
        {
            Console.WriteLine(P.description);
        }
    } 
}