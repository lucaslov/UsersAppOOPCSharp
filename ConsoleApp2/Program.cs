namespace ConsoleApp2
{
    // Base User class (demonstrates encapsulation with properties)
    public class User
    {
        public int Id { get; protected set; } // Encapsulation: Id can be only set inside this class or its descendants
        public string Name { get; set; }
        public string Email { get; set; }

        public User(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        // Polymorphism: This method can be overridden in derived classes
        public virtual string GetUserType()
        {
            return "User";
        }
    }

    // AdminUser class that inherits from User (demonstrates inheritance)
    public class AdminUser : User
    {
        public AdminUser(int id, string name, string email) : base(id, name, email)
        {
        }

        // Polymorphism: This method overrides the base class method
        public override string GetUserType()
        {
            return "Admin";
        }
    }

    public class Program
    {
        static List<User> users = new List<User>();
        static int nextId = 1;

        static void Main(string[] args)
        {
            string command = "";
            do
            {
                Console.WriteLine("Enter a command (new/view/update/delete/exit):");
                command = Console.ReadLine();

                try // Error handling: catch any errors that occur in the switch block
                {
                    switch (command)
                    {
                        case "new":
                            CreateUser();
                            break;
                        case "view":
                            ViewUsers();
                            break;
                        case "update":
                            UpdateUser();
                            break;
                        case "delete":
                            DeleteUser();
                            break;
                    }
                }
                catch (Exception ex) // Error handling: catch any type of exception
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            } while (command != "exit");
        }

        static void CreateUser()
        {
            Console.WriteLine("Enter a name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter an email:");
            string email = Console.ReadLine();
            Console.WriteLine("Is admin? (y/n):");
            string isAdmin = Console.ReadLine();

            User user;
            if (isAdmin.ToLower() == "y")
            {
                user = new AdminUser(nextId++, name, email); // Inheritance: AdminUser is a User
            }
            else
            {
                user = new User(nextId++, name, email);
            }
            users.Add(user);

            Console.WriteLine($"User {user.Name} ({user.GetUserType()}) created with id {user.Id}"); // Polymorphism: GetUserType() could return different results depending on the actual type of the user
        }

        static void ViewUsers()
        {
            foreach (var user in users)
            {
                Console.WriteLine($"Id: {user.Id}, Name: {user.Name}, Email: {user.Email}, Type: {user.GetUserType()}"); // Polymorphism: GetUserType() could return different results depending on the actual type of the user
            }
        }

        static void UpdateUser()
        {
            Console.WriteLine("Enter the user id:");
            int id = ParseId();

            var user = users.Find(u => u.Id == id);
            if (user == null)
            {
                Console.WriteLine("User not found");
                return;
            }
            Console.WriteLine("Enter a new name:");
            user.Name = Console.ReadLine();
            Console.WriteLine("Enter a new email:");
            user.Email = Console.ReadLine();

            Console.WriteLine($"User {user.Id} updated");
        }

        static void DeleteUser()
        {
            Console.WriteLine("Enter the user id:");
            int id = ParseId();

            var user = users.Find(u => u.Id == id);
            if (user == null)
            {
                Console.WriteLine("User not found");
                return;
            }

            users.Remove(user);
            Console.WriteLine($"User {user.Id} deleted");
        }

        // Validation and error handling: This method tries to parse the input as an integer, and throws an exception if it fails
        static int ParseId()
        {
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                throw new Exception("Invalid id");
            }
            return id;
        }
    }
}
