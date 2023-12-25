using System.Xml;
using System.Xml.Linq;
using Lab08_Library;

List<Animal> Deserialize(XmlDocument xDocument)
{
    string[] temp = { "Cow", "Lion", "Pig" };
    List<Animal> animals = new List<Animal>();
    XmlElement? xRoot = xDocument.DocumentElement; //получение корневого элемента
    if (xRoot is not null)
    {
        foreach (XmlNode node in xRoot) //цикл по элементу в структуре XML
        {
            
            eClassificationAnimal ClassificationAnimal(string? str) //определение внутренней функции для преобразования строкового значения в перечисление 
            {
                switch (str)
                {
                    case "Carnivores":
                        return eClassificationAnimal.Carnivores;
                    case "Herbivores":
                        return eClassificationAnimal.Herbivores;
                    default:
                        return eClassificationAnimal.Omnivores;
                }
            }

            var country = "";
            var hideFromOtherAnimals = false;
            eClassificationAnimal whatAnimal = eClassificationAnimal.Carnivores;
            
            foreach (XmlNode childnode in node.ChildNodes)
            {
                switch (childnode.Name)
                {
                    case "Country":
                    {
                        country = childnode.InnerText;
                        break;
                    }
                    case "HideFromOtherAnimals":
                    {
                        hideFromOtherAnimals = Convert.ToBoolean(childnode.InnerText); //преобразуем из строки в булево значение
                        break;
                    }
                    case "WhatAnimal":
                        whatAnimal = ClassificationAnimal(childnode.InnerText);
                        break;
                }
            }
            
            if (!temp.Contains(node.Name)) //проверка на наследие от класса Animal
            {
                animals.Add(new Animal(country, hideFromOtherAnimals, node.Name, whatAnimal)); //если имя элемента не находится в temp, то создаём новый объект и добавляем в список
            }
            else
            {
                switch (node.Name)
                {
                    case "Cow": animals.Add(new Cow(country));
                        break;
                    case "Lion": animals.Add(new Lion(country));
                        break;
                    default: animals.Add(new Pig(country)); break;
                }
            }
        }
    }

    return animals;
}

XmlDocument xmlDocument = new XmlDocument();
xmlDocument.Load("C:\\Users\\niklo\\source\\repos\\Lab 08\\Lab 08\\animals.xml");
List<Animal> animals = Deserialize(xmlDocument);
foreach (var animal in animals)
{
    Console.WriteLine($"{animal.Name} {animal.Country} {animal.HideFromOtherAnimals} {animal.WhatAnimal}");
}