using System;
using System.Collections.Generic;

class Program
{
    static List<Product> products = new List<Product>
    {
        new Product(1, "X-Burguer", 15.00m),
        new Product(2, "Refrigerante", 5.00m),
        new Product(3, "Sorvete", 10.00m)
    };
    static Dictionary<Product, int> order = new Dictionary<Product, int>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Listar produtos disponíveis");
            Console.WriteLine("2. Adicionar produto ao pedido");
            Console.WriteLine("3. Remover produto do pedido");
            Console.WriteLine("4. Visualizar pedido atual");
            Console.WriteLine("5. Finalizar pedido e sair");
            Console.Write("Escolha uma opção: ");

            string choice = Console.ReadLine();
            Console.Clear();
            switch (choice)
            {
                case "1":
                    ListProducts();
                    break;
                case "2":
                    AddToOrder();
                    break;
                case "3":
                    RemoveFromOrder();
                    break;
                case "4":
                    ViewOrder();
                    break;
                case "5":
                    FinalizeOrder();
                    return;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }

    static void ListProducts()
    {
        Console.WriteLine("Produtos disponíveis:");
        foreach (var product in products)
        {
            Console.WriteLine($"{product.Id} - {product.Name} - R${product.Price:F2}");
        }
    }

    static void AddToOrder()
    {
        ListProducts();
        Console.Write("Digite o ID do produto: ");
        int productId = int.Parse(Console.ReadLine());
        Product selectedProduct = products.Find(p => p.Id == productId);

        if (selectedProduct == null)
        {
            Console.WriteLine("Produto não encontrado!");
            return;
        }

        Console.Write("Quantidade: ");
        int quantity = int.Parse(Console.ReadLine());

        if (order.ContainsKey(selectedProduct))
            order[selectedProduct] += quantity;
        else
            order[selectedProduct] = quantity;

        Console.WriteLine("Produto adicionado ao pedido!");
    }

    static void RemoveFromOrder()
    {
        ViewOrder();
        Console.Write("Digite o ID do produto para remover: ");
        int productId = int.Parse(Console.ReadLine());
        Product selectedProduct = products.Find(p => p.Id == productId);

        if (selectedProduct == null || !order.ContainsKey(selectedProduct))
        {
            Console.WriteLine("Produto não encontrado no pedido!");
            return;
        }

        Console.Write("Quantidade a remover: ");
        int quantity = int.Parse(Console.ReadLine());

        if (order[selectedProduct] > quantity)
            order[selectedProduct] -= quantity;
        else
            order.Remove(selectedProduct);

        Console.WriteLine("Produto removido!");
    }

    static void ViewOrder()
    {
        Console.WriteLine("\nSeu pedido:");
        decimal total = 0;
        int totalItems = 0;

        foreach (var item in order)
        {
            Console.WriteLine($"{item.Key.Name} - {item.Value}x - R${item.Key.Price * item.Value:F2}");
            total += item.Key.Price * item.Value;
            totalItems += item.Value;
        }

        Console.WriteLine($"Total bruto: R${total:F2}");
    }

    static void FinalizeOrder()
    {
        Console.WriteLine("Finalizando pedido...");

        decimal total = 0;
        int totalItems = 0;

        foreach (var item in order)
        {
            total += item.Key.Price * item.Value;
            totalItems += item.Value;
        }

        decimal discount = 0;
        if (total > 100)
        {
            discount = total * 0.10m;
            Console.WriteLine("Desconto de 10% aplicado!");
        }
        else if (totalItems > 5)
        {
            Console.WriteLine("Brinde aplicado por compra acima de 5 itens!");
        }

        decimal finalTotal = total - discount;

        Console.WriteLine("Resumo do pedido:");
        Console.WriteLine($"Total de itens: {totalItems}");
        Console.WriteLine($"Valor bruto: R${total:F2}");
        Console.WriteLine($"Desconto: R${discount:F2}");
        Console.WriteLine($"Valor final: R${finalTotal:F2}");

        Console.WriteLine("Obrigado pela compra!");
    }
}

class Product
{
    public int Id { get; }
    public string Name { get; }
    public decimal Price { get; }

    public Product(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}
