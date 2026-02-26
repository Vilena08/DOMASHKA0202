using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimalZoo {
  public abstract class S_Animal {
    public string S_Name { get; set; }
    public int S_Age { get; set; }
    public string S_Habitat { get; set; }
    public string S_Diet { get; set; }
    public double S_Weight { get; set; }
    public string S_Color { get; set; }

    protected S_Animal(string name, int age, string habitat, string diet, double weight, string color) {
      S_Name = name;
      S_Age = age;
      S_Habitat = habitat;
      S_Diet = diet;
      S_Weight = weight;
      S_Color = color;
    }

    public virtual string S_GetInfo() {
      return $" Name: {S_Name}, Age: {S_Age}, Habitat: {S_Habitat}, Diet: {S_Diet}, Weight: {S_Weight} kg, Color: {S_Color} ";
    }

    public abstract string S_GetAnimalType(); 
  }

  public class S_Mammal : S_Animal {
    public bool S_HasFur { get; set; }
    public S_Mammal(string name, int age, string habitat, string diet, double weight, string color, bool hasFur)
      : base(name, age, habitat, diet, weight, color) {
      S_HasFur = hasFur;
    }
    public override string S_GetInfo() {
      return base.S_GetInfo() + $" , Type: Mammal, Fur: {(S_HasFur ? " yes " : " no ")} ";
    }
    public override string S_GetAnimalType() => " Mammal "; 
  }
    public class S_Bird : S_Animal {
      public double S_WingSpan { get; set; }
      public S_Bird(string name, int age, string habitat, string diet, double weight, string color, double wingSpan)
        : base(name, age, habitat, diet, weight, color) {
        S_WingSpan = wingSpan;
      }
      public override string S_GetInfo() {
        return base.S_GetInfo() + $" , Type: Bird, Wingspan: {S_WingSpan} m ";
      }
      public override string S_GetAnimalType() => " Bird "; 
    }
    public class S_Fish : S_Animal {
      public string S_WaterType { get; set; }
      public S_Fish(string name, int age, string habitat, string diet, double weight, string color, string waterType)
        : base(name, age, habitat, diet, weight, color) {
        S_WaterType = waterType;
      }
      public override string S_GetInfo() {
        return base.S_GetInfo() + $" , Type: Fish, Water type: {S_WaterType} ";
      }
      public override string S_GetAnimalType() => " Fish ";
    }

    public class S_Reptile : S_Animal {
      public bool S_IsVenomous { get; set; }
      public S_Reptile(string name, int age, string habitat, string diet, double weight, string color, bool isVenomous)
        : base(name, age, habitat, diet, weight, color) {
        S_IsVenomous = isVenomous;
      }
      public override string S_GetInfo() {
        return base.S_GetInfo() + $" , Type: Reptile, Venomous: {(S_IsVenomous ? "yes" : "no")} ";
      }
      public override string S_GetAnimalType() => " Reptile ";
    }

    public class S_Amphibian : S_Animal {
      public string S_SkinMoisture { get; set; }
      public S_Amphibian(string name, int age, string habitat, string diet, double weight, string color, string skinMoisture)
        : base(name, age, habitat, diet, weight, color) {
            S_SkinMoisture = skinMoisture;
      }
      public override string S_GetInfo() {
        return base.S_GetInfo() + $" , Type: Amphibian, Skin moisture: {S_SkinMoisture} ";
      }
      public override string S_GetAnimalType() => " Amphibian ";
    }

    public sealed class S_AnimalManager {
      private static readonly S_AnimalManager _instance = new S_AnimalManager();
      private List<S_Animal> _animals = new List<S_Animal>();

      private S_AnimalManager() { }

      public static S_AnimalManager Instance => _instance;

      public void S_AddAnimal(S_Animal animal) {
        _animals.Add(animal);
        Console.WriteLine($" Animal {animal.S_Name} successfully added! ");
      }

      public void S_ShowAllAnimals() {
        if (_animals.Count == 0) {
          Console.WriteLine(" The animal list is empty. ");
          return;
        }
        Console.WriteLine(" \n=== LIST OF ALL ANIMALS === ");
        for (int indexShow = 0; indexShow < _animals.Count; ++indexShow)
            {
                Console.WriteLine($"[{indexShow + 1}] {_animals[indexShow].S_GetInfo()}");
            }
        }

        public void S_ShowAnimalByIndex(int indexAnimal) {
          if (indexAnimal >= 0 && indexAnimal < _animals.Count) {
            Console.WriteLine($"\n{_animals[indexAnimal].S_GetInfo()}"); }
          else {
            Console.WriteLine(" Animal with this index not found. "); }
        }

        public void S_ShowAnimalByName(string name) {
          var animal = _animals.FirstOrDefault(a => a.S_Name.Equals(name, StringComparison.OrdinalIgnoreCase));
          if (animal != null) {
            Console.WriteLine($" \n{animal.S_GetInfo()} ");
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
          S_AnimalManager manager = S_AnimalManager.Instance;

          S_Animal[] initialAnimals = new S_Animal[] {
        new S_Mammal(" Tigerrous ", 5, " Jungle ", " Carnivore ", 190.5, " Striped ", true),
        new S_Bird(" Snowy Owl ", 3, " Tundra ", " Carnivore ", 2.5, " White ", 1.5),
        new S_Fish(" Salmon ", 2, " River ", " Carnivore ", 15.3, " Silver ", " Fresh "),
        new S_Reptile(" Viper ", 4, " Tundra ", " Carnivore ", 1.2, " Gray ", true),
        new S_Amphibian(" Kermit ", 1, " Swamp ", " Insectivore ", 0.3, " Green ", " Moist ")
        }; 

        foreach (S_Animal animal in initialAnimals) {
          manager.S_AddAnimal(animal);
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
                 manager.S_ShowAllAnimals();
                 break;

               case " 2 ":
                 if (manager.GetAnimalsCount() > 0) {
                   Console.Write($" Enter index (1-{manager.GetAnimalsCount()}): ");
                   if (int.TryParse(Console.ReadLine(), out int indexGet) && indexGet > 0) {
                     manager.S_ShowAnimalByIndex(indexGet - 1);
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
                  manager.S_ShowAnimalByName(name);
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

          S_Animal animal = null;

          switch (type) {
            case 1:
              Console.Write(" Has fur? (yes/no): ");
              bool hasFur = Console.ReadLine().ToLower() == "yes";
              animal = new S_Mammal(name, age, habitat, diet, weight, color, hasFur);
              break;

            case 2:
              Console.Write(" Wingspan (m): ");
              if (double.TryParse(Console.ReadLine(), out double wingSpan)) {
                animal = new S_Bird(name, age, habitat, diet, weight, color, wingSpan);
              }
              else {
               Console.WriteLine(" Invalid wingspan! ");
               return;
              }
               break;

             case 3:
               Console.Write(" Water type (fresh/salt): ");
               string waterType = Console.ReadLine();
               animal = new S_Fish(name, age, habitat, diet, weight, color, waterType);
               break;

             case 4:
                Console.Write(" Is venomous? (yes/no): ");
                bool isVenomous = Console.ReadLine().ToLower() == " yes ";
                animal = new S_Reptile(name, age, habitat, diet, weight, color, isVenomous);
                 break;

             case 5:
                Console.Write(" Skin moisture (moist/dry): ");
                string skinMoisture = Console.ReadLine();
                animal = new S_Amphibian(name, age, habitat, diet, weight, color, skinMoisture);
                break;
          }

          if (animal != null) {
            S_AnimalManager.Instance.S_AddAnimal(animal);
          }
        }
    }
}