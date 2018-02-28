using System;
using System.Collections.Generic;
using System.Linq;

namespace GuiraudConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            DisplayMenu();
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("Choose one task: ");
            Console.WriteLine(" 1 - EX1: Count characters");
            Console.WriteLine(" 2 - EX2: Word listing");
            Console.WriteLine(" 3 - EX3: Vehicle management");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Exercice1();
                    break;
                case "2":
                    Exercice2();
                    break;
                case "3":
                    Exercice3();
                    break;
                default:
                    Console.WriteLine("Wrong parameter");
                    break;
            }
        }


        private static void Exercice1()
        {
            while (true)
            {
                Console.WriteLine("Write something: ");
                Console.Write("> ");
                var input = Console.ReadLine();
                if (input == null) return;

                if (input.ToLower() != "exit")
                {
                    var dict = new Dictionary<char, int>();

                    foreach (var c in input)
                    {
                        var key = char.ToLower(c);
                        if (dict.ContainsKey(key))
                        {
                            dict[key]++;
                        }
                        else
                        {
                            dict[key] = 1;
                        }
                    }

                    foreach (var item in dict)
                    {
                        Console.WriteLine(item.Key + ": " + item.Value);
                    }

                    continue;
                }

                break;
            }
        }

        private static void Exercice2()
        {
            var words = new List<string>();

            DisplayCommands();

            var input = Console.ReadLine();
            while (input != "*" && words.Count <= 20)
            {
                if (IsAWord(input))
                {
                    words.Add(input);
                }
                else
                {
                    switch (input)
                    {
                        case "+":
                            words = words.OrderBy(x => x.Length).ToList();
                            DisplayList(words);
                            break;
                        case "-":
                            words = words.OrderByDescending(x => x.Length).ToList();
                            DisplayList(words);
                            break;
                        case "=":
                            words.Sort();
                            DisplayList(words);
                            break;
                        default:
                            Console.Error.WriteLine("Invalid input!");
                            break;
                    }
                }

                if (words.Count <= 20)
                {
                    input = Console.ReadLine();
                }
            }
        }

        private static bool IsAWord(string s)
        {
            return s != null && s.Count(char.IsLetter) == s.Length;
        }

        private static void DisplayCommands()
        {
            Console.WriteLine("Please input your 20 or less numbers.");
            Console.WriteLine("Commands are:");
            Console.WriteLine("    + : Sort the list, then display it");
            Console.WriteLine("    - : Sort the list in descending order, then display it");
            Console.WriteLine("    = : Display a sum of the list");
            Console.WriteLine("    * : Quit");
        }

        private static void DisplayList(List<string> list)
        {
            Console.WriteLine(string.Join(", ", list.ToArray()));
        }

        private static void Exercice3()
        {
            var vehicles = new Dictionary<string, Vehicle>();
            while (true)
            {
                Console.WriteLine("Vehicle?");
                Console.Write("> ");
                var input = Console.ReadLine();
                if (input == null) return;

                if (vehicles.ContainsKey(input))
                {
                    Console.WriteLine(vehicles[input]);
                }
                else
                {
                    var parameters = input.Split(' ');
                    if (parameters.Length != 3)
                    {
                        Console.WriteLine("Wrong amount of parameters!");
                        continue;
                    }
                    
                    var vehicleType = parameters[0];
                    var model = parameters[1];
                    var isSpeedInt = int.TryParse(parameters[2], out var speed);

                    if (!isSpeedInt)
                    {
                        Console.WriteLine("Not a valid speed!");
                        continue;
                    }
                    
                    switch (vehicleType.ToLower())
                    {
                        case "car":
                        case "voiture":
                            vehicles.Add(model, new Car(model, speed));
                            Console.WriteLine("Vehicle added!");
                            break;
                        case "bike":
                        case "moto":
                            vehicles.Add(model, new Bike(model, speed));
                            Console.WriteLine("Vehicle added!");
                            break;
                        default:
                            Console.WriteLine("This type of vehicle is not supported.");
                            break;
                    }
                }
            }
        }

        private class Vehicle
        {
            private string Model { get; }
            private int MaxSpeed { get; }

            protected Vehicle(string model, int maxSpeed)
            {
                Model = model;
                MaxSpeed = maxSpeed;
            }

            public override string ToString()
            {
                return "The " + Model + " " + GetType().Name + " can go at " + MaxSpeed + "km/h max.";
            }
        }

        private class Car : Vehicle
        {
            public Car(string model, int maxSpeed) : base(model, maxSpeed)
            {
            }
        }

        private class Bike : Vehicle
        {
            public Bike(string model, int maxSpeed) : base(model, maxSpeed)
            {
            }
        }
    }
}
