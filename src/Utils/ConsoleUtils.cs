using System.Text;
using Spectre.Console;

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
                        break;
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
                                switch (selection)
                                {
                                    case "All customers":
                                        DisplayAllCustomers();
                                        break;

                                }

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
                                break;
                            }
                        case "By name":
                            {
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
        

        // TODO: Display all customers
        public static void DisplayAllCustomers()
        {
                var = "frederik";

            var table = new Table()
                .AddColumn("Id")
                .AddColumn("First Name")
                .AddColumn("Last Name")
                .AddColumn("Country")
                .AddColumn("Postal Code")
                .AddColumn("Phone")
                .AddColumn("Email");

            foreach (var customer in customers)
            {
                table.AddRow(
                    customer.Id.ToString(),
                    customer.FirstName,
                    customer.LastName,
                    customer.Country,
                    customer.PostalCode,
                    customer.PhoneNumber,
                    customer.Email
                );
            }
            AnsiConsole.Render(table);

            
        }

        // TODO: Display customers with limit and

        // TODO: By Name

        public void DisplayCustomersByName()
        {
            string name = AnsiConsole.Ask<string>("Enter the customer name:");
            var customers = GetCustomersByName(name);

            if (customers.Any())
            {
                var table = new Table()
                    .AddColumn("Id")
                    .AddColumn("First Name")
                    .AddColumn("Last Name")
                    .AddColumn("Country")
                    .AddColumn("Postal Code")
                    .AddColumn("Phone")
                    .AddColumn("Email");

                foreach (var customer in customers)
                {
                    table.AddRow(
                        customer.Id.ToString(),
                        customer.FirstName,
                        customer.LastName,
                        customer.Country,
                        customer.PostalCode,
                        customer.PhoneNumber,
                        customer.Email
                    );
                }

                AnsiConsole.Render(table);
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]No customers found for name: {name}[/]");
            }
        }


        // TODO: Customer by ID
        public void DisplayCustomerById()
        {
            int customerId = AnsiConsole.Ask<int>("Enter the customer ID:");
            var customer = GetCustomerById(customerId);
            if (customer != null)
            {
                AnsiConsole.MarkupLine($"[bold]{customer.FirstName} {customer.LastName}[/]");
                AnsiConsole.WriteLine($"Country: {customer.Country}");
                AnsiConsole.WriteLine($"Postal Code: {customer.PostalCode}");
                AnsiConsole.WriteLine($"Phone: {customer.PhoneNumber}");
                AnsiConsole.WriteLine($"Email: {customer.Email}");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]No customer found for ID: {customerId}[/]");
            }
            // TODO: Pagination
            public void DisplayPaginatedCustomers()
            {
                int limit = AnsiConsole.Ask<int>("Enter limit");
                int offset = AnsiConsole.Ask<int>("Enter the offset:");
                var customers = GetCustomersPaginated(limit, offset);
            }
        }

    }
}