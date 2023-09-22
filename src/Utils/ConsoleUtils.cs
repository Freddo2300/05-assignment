using System.Text;
using System.Linq;
using Spectre.Console;

using Chinook.Src.Model;
using Chinook.Src.Repositories;
using Chinook.Src.Repositories.CustomerRepo;
using Dapper;

namespace Chinook.Src.Utils
{   
    // Enum for menu options
    enum MenuOptions
    {
        Create,
        Read,
        Update,
        Delete,
        Exit
    }

    class ConsoleUtils
    {
        public static void MainMenu()
        {
            MenuOptions selection;

            do
            {
                selection = AnsiConsole.Prompt(
                    new SelectionPrompt<MenuOptions>()
                        .Title("[blue bold slowblink]choose your operation[/]".ToUpper())
                        .PageSize(10)
                        .AddChoices(new[] {
                            MenuOptions.Create, 
                            MenuOptions.Read,
                            MenuOptions.Update,
                            MenuOptions.Delete,
                            MenuOptions.Exit
                        })
                );

                switch (selection)
                {
                    case MenuOptions.Create:
                        {
                            InitiateCreate();
                            break;
                        }
                    case MenuOptions.Read:
                        {
                            ReadMenu();
                            break;
                        }
                    case MenuOptions.Update:
                        break;
                    case MenuOptions.Delete:
                        break;
                    default: break;
                }

            } while (selection != MenuOptions.Exit);
            
            AnsiConsole.Write(new Markup("[red bold]b[/][green bold]y[/][blue bold]e[/][yellow bold]![/]"));
            AnsiConsole.WriteLine();
        }

        /// <summary>
        /// Function to receive user input and call create in database function
        /// </summary>
        public static void InitiateCreate()
        {
            try 
            {   
                bool choice; // declare choice variable

                do 
                {   
                    AnsiConsole.Clear();

                    string firstName = AnsiConsole.Ask<string>("ENTER [blue]first name[/]: ");
                    string lastName = AnsiConsole.Ask<string>("ENTER [blue]last name[/]: ");
                    string country = AnsiConsole.Ask<string>("ENTER [blue]country[/]: ");
                    int postalCode = AnsiConsole.Ask<int>("ENTER [blue]postal code[/]");
                    string phone = AnsiConsole.Ask<string>("ENTER [blue]phone number[/]");
                    string email = AnsiConsole.Ask<string>("ENTER [blue]email[/]");

                    string format = "{0,-15}:{1,15}";

                    StringBuilder sb = new();
                    sb.AppendFormat(format, "First name:", firstName);
                    sb.AppendLine();
                    sb.AppendFormat(format, "Last name:", lastName);
                    sb.AppendLine();
                    sb.AppendFormat(format, "Country:", country);
                    sb.AppendLine();
                    sb.AppendFormat(format, "Postal code:", postalCode);
                    sb.AppendLine();
                    sb.AppendFormat(format, "Phone:", phone);
                    sb.AppendLine();
                    sb.AppendFormat(format, "Email:", email);
                    sb.AppendLine();

                    Console.Write(sb);
                    
                    choice = AnsiConsole.Confirm("Confirm and create user?");
                } while (!choice);

                AnsiConsole.Clear();
            } 
            catch (Exception e)
            {
                AnsiConsole.Write(e.Message);
            }
        }

        public static void ReadMenu()
        {
            try
            {   
                string selection;

                do
                {
                    selection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[blue bold slowblink]Read customers[/]".ToUpper())
                        .PageSize(10)
                        .AddChoices(new[] {
                            "All customers",
                            "By limit and offset",
                            "By country",
                            "By id",
                            "By name",
                            "By net spend",
                            "Exit"
                        })
                );

                switch (selection)
                {
                    case "All customers":
                    {
                        // Implement function to get all customers and display in console
                        DisplayAllCustomers();
                        break;
                    }
                    case "By limit and offset":
                    {
                        break;
                    }
                    case "By country":
                    {
                        break;
                    }
                    case "By id":
                    {   
                        DisplayCustomerById();
                        break;
                    }
                    case "By name":
                    {
                        DisplayCustomerByName();
                        break;
                    }
                    case "By net spend":
                    {
                        break;
                    }
                    default: break;
                    
                }

                } while (selection != "Exit");
            }
            catch (Exception e)
            {
                AnsiConsole.Write(e.Message);
            }
        }

        /// <summary>
        /// AnsiConsole print all of the customers
        /// </summary>
        public static void DisplayAllCustomers()
        {   
            ICustomer customerService = new CustomerService(new ChinookContext());
            ICollection<Customer> customers = customerService.GetAll();

            var table = new Table()
                .AddColumn("Id")
                .AddColumn("First Name")
                .AddColumn("Last Name")
                .AddColumn("Country")
                .AddColumn("Postal Code")
                .AddColumn("Phone")
                .AddColumn("Email");

            foreach (Customer customer in customers)
            {
                table.AddRow(
                    customer.CustomerId.ToString(),
                    customer.FirstName,
                    customer.LastName,
                    customer.Country ?? "-",
                    customer.PostalCode ?? "-",
                    customer.Phone ?? "-",
                    customer.Email
                );
            }
            AnsiConsole.Write(table);
        }

        public static void DisplayCustomerById()
        {   
            ICustomer customerService = new CustomerService(new ChinookContext());

            int id;

            do
            {
                id = AnsiConsole.Ask<int>("ENTER [blue bold]customer id[/] (0 to return): ");

                var table = new Table()
                .AddColumn("Id")
                .AddColumn("First Name")
                .AddColumn("Last Name")
                .AddColumn("Country")
                .AddColumn("Postal Code")
                .AddColumn("Phone")
                .AddColumn("Email");

                // TODO: Insert id into service from marc
                Customer customer = customerService.GetById(id);

                if (customer == null)
                    {
                        AnsiConsole.WriteLine($"No customer found by id {id}");
                        break;
                    }

                table.AddRow(
                    customer.CustomerId.ToString(),
                    customer.FirstName,
                    customer.LastName,
                    customer.Country ?? "-",
                    customer.PostalCode ?? "-",
                    customer.Phone ?? "-",
                    customer.Email
                );

                AnsiConsole.Write(table);

            } while (id != 0);
            
            AnsiConsole.Write("Ok bye");
        }

        public static void DisplayCustomerByName()
        {
            ICustomer customerService = new CustomerService(new ChinookContext());
            
            string name;

            do
            {
                name = AnsiConsole.Ask<string>("ENTER the [blue bold]customer name[/] (q to return): ");

                var table = new Table()
                .AddColumn("Id")
                .AddColumn("First Name")
                .AddColumn("Last Name")
                .AddColumn("Country")
                .AddColumn("Postal Code")
                .AddColumn("Phone")
                .AddColumn("Email");

                Customer customer = customerService.GetByName(name);

                if (customer == null)
                    {
                        AnsiConsole.WriteLine($"No customer found by name {name}");
                        break;
                    }

                table.AddRow(
                    customer.CustomerId.ToString(),
                    customer.FirstName,
                    customer.LastName,
                    customer.Country ?? "-",
                    customer.PostalCode ?? "-",
                    customer.Phone ?? "-",
                    customer.Email
                );

                AnsiConsole.Write(table);

            } while (name != "q");
            
            AnsiConsole.Write("Ok bye");
        }
    }
}