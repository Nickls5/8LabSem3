using System.Reflection;
using System.Xml.Linq;
using Lab08_Library;

void Serialize(Animal animal, XElement program)
{
    XElement element = new XElement(animal.Name!);
    element.Add(new XElement("HideFromOtherAnimals", animal.HideFromOtherAnimals),
        new XElement("Country", animal.Country),
        new XElement("WhatAnimal", animal.GetClassificationAnimal()));
    program.Add(element);
}


Animal dog = new Animal("Russia", false, "Cat", eClassificationAnimal.Carnivores); //������ 3 ������� �������� � ������� ����������������
Lion lion = new Lion("Belarus");
Cow cow = new Cow("England");
Pig pig = new Pig("Russia");
XElement program = new XElement("Animals");
Serialize(dog, program); //��������� ���������� � �������� � XML-��������
Serialize(lion, program);
Serialize(cow, program);
Serialize(pig, program);
XDocument newDoc = new XDocument(program);
newDoc.Save("C:\\Users\\niklo\\source\\repos\\Lab 08\\Lab 08\\animals.xml");