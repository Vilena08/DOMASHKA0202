using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimalZoo {
  public abstract class Animal {
    public string Name { get; set; }
    public int Age { get; set; }
    public string Habitat { get; set; }
    public string Diet { get; set; }
    public double Weight { get; set; }
    public string Color { get; set; }

    protected Animal(string name, int age, string habitat, string diet, double weight, string color) {
      Name = name;
      Age = age;
      Habitat = habitat;
      Diet = diet;
      Weight = weight;
      Color = color;
    }

    public virtual string GetInfo() {
      return $" Name: {Name}, Age: {Age}, Habitat: {Habitat}, Diet: {Diet}, Weight: {Weight} kg, Color: {Color} ";
    }

    public abstract string GetAnimalType(); 
  }

  public class Mammal : Animal {
    public bool HasFur { get; set; }
    public Mammal(string name, int age, string habitat, string diet, double weight, string color, bool hasFur)
      : base(name, age, habitat, diet, weight, color) {
      HasFur = hasFur;
    }
    public override string GetInfo() {
      return base.GetInfo() + $" , Type: Mammal, Fur: {(HasFur ? " yes " : " no ")} ";
    }
    public override string GetAnimalType() => " Mammal "; 
  }
    public class Bird : Animal {
      public double WingSpan { get; set; }
      public Bird(string name, int age, string habitat, string diet, double weight, string color, double wingSpan)
        : base(name, age, habitat, diet, weight, color) {
        WingSpan = wingSpan;
      }
      public override string GetInfo() {
        return base.GetInfo() + $" , Type: Bird, Wingspan: {WingSpan} m ";
      }
      public override string GetAnimalType() => " Bird "; 
    }
    public class Fish : Animal {
      public string WaterType { get; set; }
      public Fish(string name, int age, string habitat, string diet, double weight, string color, string waterType)
        : base(name, age, habitat, diet, weight, color) {
        WaterType = waterType;
      }
      public override string GetInfo() {
        return base.GetInfo() + $" , Type: Fish, Water type: {WaterType} ";
      }
      public override string GetAnimalType() => " Fish ";
    }

    public class Reptile : Animal {
      public bool IsVenomous { get; set; }
      public Reptile(string name, int age, string habitat, string diet, double weight, string color, bool isVenomous)
        : base(name, age, habitat, diet, weight, color) {
        IsVenomous = isVenomous;
      }
      public override string GetInfo() {
        return base.GetInfo() + $" , Type: Reptile, Venomous: {(IsVenomous ? "yes" : "no")} ";
      }
      public override string GetAnimalType() => " Reptile ";
    }

    public class Amphibian : Animal {
      public string SkinMoisture { get; set; }
      public Amphibian(string name, int age, string habitat, string diet, double weight, string color, string skinMoisture)
        : base(name, age, habitat, diet, weight, color) {
            SkinMoisture = skinMoisture;
      }
      public override string GetInfo() {
        return base.GetInfo() + $" , Type: Amphibian, Skin moisture: {SkinMoisture} ";
      }
      public override string GetAnimalType() => " Amphibian ";  // '=>' означает "возвращает" или "выполняет"
    }

    public sealed class AnimalManager
    {
        private static readonly AnimalManager s_instance = new AnimalManager();
        private List<Animal> _animals = new List<Animal>();

        private AnimalManager() { }

        public static AnimalManager Instance // самое важное для Singleton,свойство — специальный метод в C#, который позволяет получить доступ к полю класса.
        {
            get { return s_instance; }
        }

        public void AddAnimal(Animal animal) {
        _animals.Add(animal);
        Console.WriteLine($" Animal {animal.Name} successfully added! ");
      }

      public void ShowAllAnimals() {
        if (_animals.Count == 0) {
          Console.WriteLine(" The animal list is empty. ");
          return;
        }
        Console.WriteLine(" \n=== LIST OF ALL ANIMALS === ");
        for (int indexShow = 0; indexShow < _animals.Count; ++indexShow)
            {
                Console.WriteLine($"[{indexShow + 1}] {_animals[indexShow].GetInfo()}");
            }
        }

        public void ShowAnimalByIndex(int indexAnimal) {
          if (indexAnimal >= 0 && indexAnimal < _animals.Count) {
            Console.WriteLine($"\n{_animals[indexAnimal].GetInfo()}"); }
          else {
            Console.WriteLine(" Animal with this index not found. "); }
        }

        public void ShowAnimalByName(string name) {
          var animal = _animals.FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
          if (animal != null) {
            Console.WriteLine($" \n{animal.GetInfo()} ");
          }
          else
          {
            Console.WriteLine($" Animal with name '{name}' not found. ");
          }
        }

        public int GetAnimalsCount() => _animals.Count;
    }

    class Program {
        static void S_Main() {
          AnimalManager manager = AnimalManager.Instance;

          Animal[] initialAnimals = new Animal[] {
        new Mammal(" Tigerrous ", 5, " Jungle ", " Carnivore ", 190.5, " Striped ", true),
        new Bird(" Snowy Owl ", 3, " Tundra ", " Carnivore ", 2.5, " White ", 1.5),
        new Fish(" Salmon ", 2, " River ", " Carnivore ", 15.3, " Silver ", " Fresh "),
        new Reptile(" Viper ", 4, " Tundra ", " Carnivore ", 1.2, " Gray ", true),
        new Amphibian(" Kermit ", 1, " Swamp ", " Insectivore ", 0.3, " Green ", " Moist ")
        }; 

        foreach (Animal animal in initialAnimals) {
          manager.AddAnimal(animal);
        }

          while (true) {
            Console.WriteLine(" \n=== ZOO MENU === ");
            Console.WriteLine(" 1. Show all animals ");
            Console.WriteLine(" 2. Find by index ");
            Console.WriteLine(" 3. Find by name ");
            Console.WriteLine(" 4. Add new animal ");
            Console.WriteLine(" 5. Exit ");
            Console.Write(" Choose option (1-5): ");

           string choice = Console.ReadLine();

             switch (choice) {
               case " 1 ":
                 manager.ShowAllAnimals();
                 break;

               case " 2 ":
                 if (manager.GetAnimalsCount() > 0) {
                   Console.Write($" Enter index (1-{manager.GetAnimalsCount()}): ");
                   if (int.TryParse(Console.ReadLine(), out int indexGet) && indexGet > 0) {
                     manager.ShowAnimalByIndex(indexGet - 1);
                   }
                   else {
                     Console.WriteLine(" Invalid input! ");
                   }
               }
                 else {
                   Console.WriteLine(" The animal list is empty. ");
                 }
                   break;

                case " 3 ":
                  Console.Write("Enter name: ");
                  string name = Console.ReadLine();
                  manager.ShowAnimalByName(name);
                  break;

                case " 4 ":
                  AddNewAnimal();
                  break;

                case " 5 ":
                  Console.WriteLine(" Goodbye! ");
                  return;

                default:
                  Console.WriteLine(" Invalid choice! Please try again.");
                    break;
             }
          }
        }

        static void AddNewAnimal() {
          Console.WriteLine(" \n=== ADDING NEW ANIMAL === ");
          Console.WriteLine(" 1. Mammal ");
          Console.WriteLine(" 2. Bird ");
          Console.WriteLine(" 3. Fish ");
          Console.WriteLine(" 4. Reptile ");
          Console.WriteLine(" 5. Amphibian ");
          Console.Write(" Your choice (1-5): ");

        if (!int.TryParse(Console.ReadLine(), out int type) || type < 1 || type > 5) {
          Console.WriteLine(" Invalid choice! ");
          return;
        }

        Console.Write(" Name: ");
         string name = Console.ReadLine();

        Console.Write(" Age: ");
          if (!int.TryParse(Console.ReadLine(), out int age)) {
            Console.WriteLine(" Invalid age! ");
            return;
          }

          Console.Write(" Habitat: ");
          string habitat = Console.ReadLine();

          Console.Write( "Diet: ");
          string diet = Console.ReadLine();

          Console.Write(" Weight (kg): ");
          if (!double.TryParse(Console.ReadLine(), out double weight)) {
            Console.WriteLine("Invalid weight!");
            return;
          }

          Console.Write(" Color: ");
          string color = Console.ReadLine();

          Animal animal = null;

          switch (type) {
            case 1:
              Console.Write(" Has fur? (yes/no): ");
              bool hasFur = Console.ReadLine().ToLower() == "yes";
              animal = new Mammal(name, age, habitat, diet, weight, color, hasFur);
              break;

            case 2:
              Console.Write(" Wingspan (m): ");
              if (double.TryParse(Console.ReadLine(), out double wingSpan)) {
                animal = new Bird(name, age, habitat, diet, weight, color, wingSpan);
              }
              else {
               Console.WriteLine(" Invalid wingspan! ");
               return;
              }
               break;

             case 3:
               Console.Write(" Water type (fresh/salt): ");
               string waterType = Console.ReadLine();
               animal = new Fish(name, age, habitat, diet, weight, color, waterType);
               break;

             case 4:
                Console.Write(" Is venomous? (yes/no): ");
                bool isVenomous = Console.ReadLine().ToLower() == " yes ";
                animal = new Reptile(name, age, habitat, diet, weight, color, isVenomous);
                 break;

             case 5:
                Console.Write(" Skin moisture (moist/dry): ");
                string skinMoisture = Console.ReadLine();
                animal = new Amphibian(name, age, habitat, diet, weight, color, skinMoisture);
                break;
          }

          if (animal != null) {
            AnimalManager.Instance.AddAnimal(animal);
          }
        }
    }
}