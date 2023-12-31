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
        Exit
    }

    class ConsoleUtils
    {
        public static void MainMenu()
        {
            MenuOptions selection;

            do
            {
                // Display list of options for user that they can select from
                AnsiConsole.WriteLine();
                selection = AnsiConsole.Prompt(
                    new SelectionPrompt<MenuOptions>()
                        .Title("[blue bold slowblink]choose your operation[/]".ToUpper())
                        .PageSize(10)
                        .AddChoices(new[] {
                            MenuOptions.Create,
                            MenuOptions.Read,
                            MenuOptions.Update,
                            MenuOptions.Exit
                        })
                );

                // Switch on the user selection, depending function call
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
                        {
                            UpdateCustomer();
                            break;
                        }
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
                ICustomer customerService = new CustomerService();

                bool choice; // declare choice variable

                string firstName;
                string lastName;
                string country;
                string postalCode;
                string phone;
                string email;

                do
                {
                    AnsiConsole.Clear();

                    // Assign user input from Ask method
                    firstName = AnsiConsole.Ask<string>("ENTER [blue]first name[/]: ");
                    lastName = AnsiConsole.Ask<string>("ENTER [blue]last name[/]: ");
                    country = AnsiConsole.Ask<string>("ENTER [blue]country[/]: ");
                    postalCode = AnsiConsole.Ask<string>("ENTER [blue]postal code[/]");
                    phone = AnsiConsole.Ask<string>("ENTER [blue]phone number[/]");
                    email = AnsiConsole.Ask<string>("ENTER [blue]email[/]");

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

                    // Confirm the user input with the user
                    choice = AnsiConsole.Confirm("Confirm and create user?");
                } while (!choice);

                Customer customer = new()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Country = country,
                    PostalCode = postalCode,
                    Phone = phone,
                    Email = email
                };

                // Create new customer, if user confirmed
                _ = customerService.Add(customer);

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
                            "By id (most popular genre)",
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
                                DisplayPaginatedCustomers();
                                break;
                            }
                        case "By country":
                            {
                                DisplayCustomersByCountry();
                                break;
                            }
                        case "By id":
                            {
                                DisplayCustomerById();
                                break;
                            }
                        case "By id (most popular genre)":
                        {
                            DisplayCustomerMostPopularGenre();
                            break;
                        }
                        case "By name":
                            {
                                DisplayCustomerByName();
                                break;
                            }
                        case "By net spend":
                            {
                                DisplayCustomersByNetSpend();
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
            ICustomer customerService = new CustomerService();
            List<Customer> customers = customerService.GetAll();

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

        /// <summary>
        /// READ CUSTOMER: by id
        /// </summary>
        public static void DisplayCustomerById()
        {
            ICustomer customerService = new CustomerService();

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

        public static void DisplayCustomerMostPopularGenre()
        {
            ICustomer customerService = new CustomerService();

            int id;

            do
            {
                id = AnsiConsole.Ask<int>("ENTER [blue bold]customer id[/] (0 to return): ");

                var table = new Table()
                .AddColumn("Id")
                .AddColumn("Genre id");

                // TODO: Insert id into service from marc
                Customer customer = customerService.GetById(id);

                if (customer == null)
                {
                    AnsiConsole.WriteLine($"No customer found by id {id}");
                    break;
                }

                Tuple<int, int> customerGenre = customerService.FavoriteGenre(customer);

                table.AddRow(
                    customerGenre.Item1.ToString(),
                    customerGenre.Item2.ToString()
                );

                AnsiConsole.Write(table);

            } while (id != 0);

            AnsiConsole.Write("Ok bye");
        }

        /// <summary>
        /// READ CUSTOMER: by name
        /// </summary>
        public static void DisplayCustomerByName()
        {
            ICustomer customerService = new CustomerService();

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

            AnsiConsole.WriteLine("Ok bye");
            AnsiConsole.WriteLine();
        }


        public static void DisplayCustomersByCountry()
        {
            ICustomer customerService = new CustomerService();

            Dictionary<string, int> customers = customerService.CustomerCountry();

            var table = new Table()
                .AddColumn("Country")
                .AddColumn("Count");
            foreach (KeyValuePair<string, int> customer in customers)
            {
                Console.WriteLine($"{customer.Key}:{customer.Value}");
                table.AddRow(
                    customer.Key ?? "din far",
                    customer.Value.ToString() ?? "-"
                );
            }
            AnsiConsole.Write(table);
        }

        // TODO : Get customers by net spend
        public static void DisplayCustomersByNetSpend()
        {
            ICustomer customerService = new CustomerService();

            Dictionary<string, decimal> customers = customerService.BigSpenders();

            var table = new Table()
                .AddColumn("First Name")
                .AddColumn("Net spend");

            foreach (KeyValuePair<string, decimal> pair in customers)
            {
                table.AddRow(
                    pair.Key ?? "-",
                    pair.Value.ToString() ?? "-"
                );
            }
            AnsiConsole.Write(table);
            AnsiConsole.WriteLine();
        }

        /// <summary>
        /// Display paginated customers by offset and limit
        /// </summary>
        public static void DisplayPaginatedCustomers()
        {
            ICustomer customerService = new CustomerService();

            int offset;
            int limit;

            bool choice;

            do
            {
                List<Customer> customers = customerService.GetAll();

                offset = AnsiConsole.Ask<int>($"ENTER [bold blue]offset[/] (max {customers.Count}): ");
                limit = AnsiConsole.Ask<int>($"ENTER [bold blue]limit[/] (max {customers.Count}): ");

                if (offset + limit > customers.Count)
                {
                    AnsiConsole.WriteLine($"Out of bounds.\nMax = {customers.Count}");
                    break;
                }

                List<Customer> paginatedCustomers = customerService.GetCustomerPage(limit, offset);

                var table = new Table()
                .AddColumn("Id")
                .AddColumn("First Name")
                .AddColumn("Last Name")
                .AddColumn("Country")
                .AddColumn("Postal Code")
                .AddColumn("Phone")
                .AddColumn("Email");

                foreach (Customer customer in paginatedCustomers)
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

                choice = AnsiConsole.Confirm("Keep paginating?");

            } while (choice);

            AnsiConsole.WriteLine("OK bye");
            AnsiConsole.WriteLine();

            AnsiConsole.Clear();
        }

        /// <summary>
        /// Update an existing customer within the database
        /// </summary>
        public static void UpdateCustomer()
        {
            ICustomer customerService = new CustomerService();

            int id;

            do
            {
                List<Customer> customers = customerService.GetAll();

                id = AnsiConsole.Ask<int>($"ENTER [blue]customer id[/] of max {customers.Count} (0 to return): ");

                if (id > customers.Count)
                {
                    AnsiConsole.WriteLine("No customer with that id exists");
                    break;
                }

                if (id == 0)
                {
                    AnsiConsole.WriteLine("Ok bye");
                    break;
                }

                Customer selectedCustomer = customerService.GetById(id);

                string selection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title($"[blue bold slowblink]SELECT PROPERTY TO UPDATE FOR CUSTOMER {id}[/]".ToUpper())
                        .PageSize(10)
                        .AddChoices(
                            "First name",
                            "Last name",
                            "Country",
                            "Postal code",
                            "Phone",
                            "Email",
                            "Exit"
                        )
                );

                switch (selection)
                {
                    case "First name":
                        {
                            string value = AnsiConsole.Ask<string>($"ENTER new [blue]{selection}[/]: ");

                            selectedCustomer.FirstName = value;

                            _ = customerService.Update(selectedCustomer);

                            break;
                        }
                    case "Last name":
                        {
                            string value = AnsiConsole.Ask<string>($"ENTER new [blue]{selection}[/]: ");

                            selectedCustomer.LastName = value;

                            _ = customerService.Update(selectedCustomer);

                            break;
                        }
                    case "Country":
                        {
                            string value = AnsiConsole.Ask<string>($"ENTER new [blue]{selection}[/]: ");

                            selectedCustomer.Country = value;

                            _ = customerService.Update(selectedCustomer);

                            break;
                        }
                    case "Postal code":
                        {
                            string value = AnsiConsole.Ask<string>($"ENTER new [blue]{selection}[/]: ");

                            selectedCustomer.PostalCode = value;

                            _ = customerService.Update(selectedCustomer);

                            break;
                        }
                    case "Phone":
                        {
                            string value = AnsiConsole.Ask<string>($"ENTER new [blue]{selection}[/]: ");

                            selectedCustomer.Phone = value;

                            _ = customerService.Update(selectedCustomer);

                            break;
                        }
                    case "Email":
                        {
                            string value = AnsiConsole.Ask<string>($"ENTER new [blue]{selection}[/]: ");

                            selectedCustomer.Email = value;

                            _ = customerService.Update(selectedCustomer);

                            break;
                        }
                    default: break;
                }

            } while (id != 0);

            AnsiConsole.WriteLine();
            AnsiConsole.Clear();
        }

        /// <summary>
        /// Delete existing customer within database
        /// </summary>
        /*
        public static void DeleteCustomer()
        {
            ICustomer customerService = new CustomerService(new ChinookContext());

            int id;
            bool choice;

            Customer selectedCustomer = new();

            do
            {
                ICollection<Customer> customers = customerService.GetAll();

                id = AnsiConsole.Ask<int>($"ENTER [bold blue]customer id[/] to delete of max {customers.Count} (0 to return): ");

                if (id > customers.Count)
                {
                    AnsiConsole.WriteLine("No customer has that id");
                    break;
                }

                if (id == 0)
                {
                    AnsiConsole.WriteLine("Ok bye");
                    break;
                }

                selectedCustomer = customerService.GetById(id);

                choice = AnsiConsole.Confirm($"Are you sure to delete customer: {selectedCustomer.FirstName} with id {selectedCustomer.CustomerId}");

            } while (!choice);

            customerService.Delete(selectedCustomer);
        }
        */
    }
}