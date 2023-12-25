namespace Lab08_Library;

[MyComment("Base class Animal")]
public class Animal
{
    public string? Country { get; set; }
    public bool HideFromOtherAnimals { get; set; }
    public string? Name { get; set; }
    public eClassificationAnimal WhatAnimal { get; set; }

    protected Animal()
    {
    }

    public Animal(string? country, bool hideFromOtherAnimals, string? name, eClassificationAnimal whatAnimal)
    {
        Country = country;
        HideFromOtherAnimals = hideFromOtherAnimals;
        Name = name;
        WhatAnimal = whatAnimal;
    }

    public eClassificationAnimal GetClassificationAnimal()
    {
        return WhatAnimal;
    }

    public eFavoriteFood GetFavoriteFood()
    {
        return WhatAnimal switch
        {
            eClassificationAnimal.Carnivores => eFavoriteFood.Meat,
            eClassificationAnimal.Herbivores => eFavoriteFood.Plants,
            _ => eFavoriteFood.Everything
        };
    }

    public void SayHello()
    {
        Console.WriteLine($"Hello, i'm {Name}!");
    }
}