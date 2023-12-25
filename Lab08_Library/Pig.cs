namespace Lab08_Library;

[MyComment("Inheritable class Pig")]
public class Pig : Animal
{
    public Pig(string? country)
    {
        Country = country;
        HideFromOtherAnimals = true;
        Name = "Pig";
        WhatAnimal = eClassificationAnimal.Omnivores;
    }
}