using G_Store;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;


            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            
            if (IsValidUser(username, password))
            {
                Console.WriteLine("Login successful!");

                GS_Dbcontext dbContext = new GS_Dbcontext();

                while (true)
                {
                    Console.WriteLine("\nSelect an option:");
                    Console.WriteLine("1. Add Customer");
                    Console.WriteLine("2. View Customer");
                    Console.WriteLine("3. Add Sale");
                    Console.WriteLine("4. View Sales");
                    Console.WriteLine("5. Add Inventory");
                    Console.WriteLine("6. View Inventory");
                    Console.WriteLine("7. Add Supplier");
                    Console.WriteLine("8. View Suppliers");
                    Console.WriteLine("9. Add Supplies");
                    Console.WriteLine("10. View Supplies");
                    Console.WriteLine("0. Exit");

                    string option = Console.ReadLine();

                    switch (option)
                    {
            case "1":
                AddCustomer(dbContext);
                break;
            case "2":
                ViewCustomerList(dbContext);
                break;
                          case "3":
                            AddSale(dbContext);
                            break;
                        case "4":
                            ViewSales(dbContext);
                            break;
                        case "5":
                            AddInventory(dbContext);
                            break;
                        case "6":
                            ViewInventory(dbContext);
                            break;
                        case "7":
                            AddSupplier(dbContext);
                            break;
                        case "8":
                            ViewSuppliers(dbContext);
                            break;
            case "9":
                AddSupply(dbContext);
                break;
            case "10":
                ViewSupplyList(dbContext);
                break;
            case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid username or password. Exiting...");
                return ;
    
            }
        

        static bool IsValidUser(string username, string password)
        {
            
            return username == "asdf" && password == "1234";
        }



using (var dbContext = new GS_Dbcontext())
{
    // Add customer
    AddCustomer(dbContext);

    // View customer list
    ViewCustomerList(dbContext);
}
    

    static void AddCustomer(GS_Dbcontext dbContext)
{
    Console.WriteLine("Enter customer address:");
    string customerAddress = Console.ReadLine();

    Console.WriteLine("Enter customer contact:");
    string customerContact = Console.ReadLine();

    Console.WriteLine("Enter due amount:");
    string dueAmount = Console.ReadLine();

    
    Customer customer = new Customer
    {
        cus_add = customerAddress,
        cus_contact = customerContact,
        due = dueAmount
    };

    // Save to database
    dbContext.Customers.Add(customer);
    dbContext.SaveChanges();

    Console.WriteLine("Customer added successfully!");
}

static void ViewCustomerList(GS_Dbcontext dbContext)
{
    Console.WriteLine("Customer List:");

    List<Customer> customers = dbContext.Customers.ToList();

    foreach (var customer in customers)
    {
        Console.WriteLine($"ID: {customer.cus_id}, Address: {customer.cus_add}, Contact: {customer.cus_contact}, Due: {customer.due}");
    }

}







    static void AddSale(GS_Dbcontext dbContext)
        {

    Console.WriteLine("Enter customer ID:");
    int customerId = int.Parse(Console.ReadLine());

    Console.WriteLine("Enter product ID:");
    int productId = int.Parse(Console.ReadLine());

    Console.WriteLine("Enter sale date (yyyy-MM-dd):");
    DateTime saleDate;
    while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out saleDate))
    {
        Console.WriteLine("Invalid date format. Please enter the date in yyyy-MM-dd format:");
    }

    Console.WriteLine("Enter product quantity:");
    int productQuantity = int.Parse(Console.ReadLine());

    // Assuming your entities have constructors that take necessary parameters
    Sale sale = new Sale
    {
        cus_id = customerId,
        pro_id = productId,
        S_Date = saleDate,
        P_Quantity = productQuantity
    };

    // Save to database
    dbContext.Sales.Add(sale);
            dbContext.SaveChanges();

            // Update Supplies table - reduce quantity
            var supply = dbContext.Supplies
                .Where(s => s.pro_id == sale.pro_id)
                .FirstOrDefault();

            if (supply != null)
            {
                supply.P_Quantity -= sale.P_Quantity;
                dbContext.SaveChanges();
            }

            Console.WriteLine("Sale added successfully!");
        }

        static void ViewSales(GS_Dbcontext dbContext)
        {
            var sales = dbContext.Sales.ToList();
            Console.WriteLine("Sales:");

            foreach (var sale in sales)
            {
                Console.WriteLine($"SaleId: {sale.SaleId}, Cus_Id: {sale.cus_id}, Pro_Id: {sale.pro_id}, Date: {sale.S_Date}, Quantity: {sale.P_Quantity}");
            }
        }

static void AddInventory(GS_Dbcontext dbContext)
{
    Console.WriteLine("Enter product category:");
    string productCategory = Console.ReadLine();

    Console.WriteLine("Enter date of entry (YYYY-MM-DD):");
    if (!DateTime.TryParse(Console.ReadLine(), out DateTime dateOfEntry))
    {
        Console.WriteLine("Invalid date format. Aborting...");
        return;
    }

    Console.WriteLine("Enter selling price:");
    if (!decimal.TryParse(Console.ReadLine(), out decimal sellingPrice))
    {
        Console.WriteLine("Invalid selling price. Aborting...");
        return;
    }

    // Assuming your entities have constructors that take necessary parameters
    Inventory inventory = new Inventory
    {
        pro_cat = productCategory,
        doe = dateOfEntry,
        selling_price = sellingPrice
    };

    // Save to database
    dbContext.Inventorys.Add(inventory);
    dbContext.SaveChanges();

    Console.WriteLine("Inventory added successfully!");
}


static void ViewInventory(GS_Dbcontext dbContext)
{
    var inventoryItems = dbContext.Inventorys.ToList();
    Console.WriteLine("Inventory:");

    foreach (var item in inventoryItems)
    {
        Console.WriteLine($"Pro_Id: {item.pro_id}, Category: {item.pro_cat}, Date of Entry: {item.doe}, Selling Price: {item.selling_price}");
    }
}

static void AddSupplier(GS_Dbcontext dbContext)
{
    Console.WriteLine("Enter supplier company:");
    string supplierCompany = Console.ReadLine();

    Console.WriteLine("Enter supplier contact:");
    if (!int.TryParse(Console.ReadLine(), out int supplierContact))
    {
        Console.WriteLine("Invalid supplier contact. Aborting...");
        return;
    }

    Console.WriteLine("Enter supplier location:");
    string supplierLocation = Console.ReadLine();

    // Assuming your entities have constructors that take necessary parameters
    Supplier supplier = new Supplier
    {
        sup_com = supplierCompany,
        sup_contact = supplierContact,
        sup_location = supplierLocation
    };

    // Save to database
    dbContext.Suppliers.Add(supplier);
    dbContext.SaveChanges();

    Console.WriteLine("Supplier added successfully!");
}


static void ViewSuppliers(GS_Dbcontext dbContext)
{
    var suppliers = dbContext.Suppliers.ToList();
    Console.WriteLine("Suppliers:");

    foreach (var supplier in suppliers)
    {
        Console.WriteLine($"Sup_Id: {supplier.sup_id}, Company: {supplier.sup_com}, Contact: {supplier.sup_contact}, Location: {supplier.sup_location}");
    }
}




using (var dbContext = new GS_Dbcontext())
{
    // Add supply
    AddSupply(dbContext);

    // View supplies list
    ViewSupplyList(dbContext);
}
    

    static void AddSupply(GS_Dbcontext dbContext)
{
    Console.WriteLine("Enter product ID:");
    int productId = int.Parse(Console.ReadLine());

    Console.WriteLine("Enter supplier ID:");
    int supplierId = int.Parse(Console.ReadLine());

    Console.WriteLine("Enter product quantity:");
    int productQuantity = int.Parse(Console.ReadLine());

    // Assuming your entities have constructors that take necessary parameters
    Supplie supply = new Supplie
    {
        pro_id = productId,
        sup_id = supplierId,
        P_Quantity = productQuantity
    };

    // Save to database
    dbContext.Supplies.Add(supply);
    dbContext.SaveChanges();

    Console.WriteLine("Supply added successfully!");
}

static void ViewSupplyList(GS_Dbcontext dbContext)
{
    Console.WriteLine("Supply List:");

    List<Supplie> supplies = dbContext.Supplies
        .Include(s => s.Inventory)
        .Include(s => s.Supplier)
        .ToList();

    foreach (var supply in supplies)
    {
        Console.WriteLine($"Product ID: {supply.pro_id}, Supplier ID: {supply.sup_id}, Quantity: {supply.P_Quantity}");
    }
}

