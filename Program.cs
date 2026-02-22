using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        int choice;
        do
        {
            ClearScreen();
            Console.WriteLine("=======================================");
            Console.WriteLine("          TUTOR LINK SYSTEM");
            Console.WriteLine("=======================================");
            Console.WriteLine("1. Student Registration");
            Console.WriteLine("2. Tutor Registration");
            Console.WriteLine("3. View All Students");
            Console.WriteLine("4. View All Tutors");
            Console.WriteLine("5. Exit");
            Console.WriteLine("----------------------------------------");
            Console.Write("Enter choice: ");
            int.TryParse(Console.ReadLine(), out choice);

            switch (choice)
            {
                case 1:
                    StudentRegistration();
                    break;
                case 2:
                    TutorRegistration();
                    break;
                case 3:
                    ViewAllStudents();
                    break;
                case 4:
                    ViewAllTutors();
                    break;
                case 5:
                    Console.WriteLine("Exit!");
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();

        } while (choice != 5);
    }

    static void ClearScreen()
    {
        Console.Clear();
    }

    static void StudentRegistration()
    {
        Console.WriteLine("\n1. Create Account\n2. Login");
        Console.Write("Enter choice: ");
        int option;
        int.TryParse(Console.ReadLine(), out option);

        if (option == 1)
        {
            string name, subject, contact, password;
            bool valid;

            do
            {
                valid = true;
                Console.Write("Enter Name: ");
                name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name)) valid = false;
                foreach (char c in name)
                    if (!char.IsLetter(c) && c != ' ') valid = false;
                if (!valid) Console.WriteLine("Enter characters only.");
            } while (!valid);

            do
            {
                valid = true;
                Console.Write("Enter Subject: ");
                subject = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(subject)) valid = false;
                foreach (char c in subject)
                    if (!char.IsLetter(c) && c != ' ') valid = false;
                if (!valid) Console.WriteLine("Enter characters only.");
            } while (!valid);

            do
            {
                valid = true;
                Console.Write("Enter Contact (11 digits): ");
                contact = Console.ReadLine();
                if (contact.Length != 11) valid = false;
                foreach (char c in contact)
                    if (!char.IsDigit(c)) valid = false;
                if (!valid) Console.WriteLine("Enter 11 digit number only.");
            } while (!valid);

            do
            {
                valid = true;
                Console.Write("Enter Password (max 8 digits, numeric): ");
                password = Console.ReadLine();
                if (string.IsNullOrEmpty(password) || password.Length > 8) valid = false;
                foreach (char c in password)
                    if (!char.IsDigit(c)) valid = false;
                if (!valid) Console.WriteLine("Password must be numeric & max 8 digits.");
            } while (!valid);

            File.AppendAllText("students.txt", name + "|" + subject + "|" + contact + "|" + password + Environment.NewLine);

            Console.WriteLine("\nAccount created successfully!");
            Console.WriteLine("\nSearching tutor for your subject...");
            MatchStudentWithTutor(subject);
        }
        else if (option == 2)
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            bool found = false;

            if (File.Exists("students.txt"))
            {
                string[] lines = File.ReadAllLines("students.txt");
                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 4)
                    {
                        if (parts[0] == name && parts[3] == password)
                        {
                            found = true;
                            Console.WriteLine("\nLogin successful!");
                            MatchStudentWithTutor(parts[1]);
                            break;
                        }
                    }
                }
            }

            if (!found)
                Console.WriteLine("\nAccount not found!");
        }
    }

}