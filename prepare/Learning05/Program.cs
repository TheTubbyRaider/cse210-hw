using System;
using System.Collections.Generic;

class Shape
{
    public string Color { get; set; }

    public virtual double GetArea()
    {
        return 0.0;
    }
}

class Square : Shape
{
    private double Side { get; set; }

    public Square(string color, double side)
    {
        Color = color;
        Side = side;
    }

    public override double GetArea()
    {
        return Side * Side;
    }
}

class Rectangle : Shape
{
    private double Length { get; set; }
    private double Width { get; set; }

    public Rectangle(string color, double length, double width)
    {
        Color = color;
        Length = length;
        Width = width;
    }

    public override double GetArea()
    {
        return Length * Width;
    }
}

class Circle : Shape
{
    private double Radius { get; set; }

    public Circle(string color, double radius)
    {
        Color = color;
        Radius = radius;
    }

    public override double GetArea()
    {
        return Math.PI * Radius * Radius;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var shapes = new List<Shape>
        {
            new Square("Red", 5.0),
            new Rectangle("Blue", 4.0, 6.0),
            new Circle("Green", 3.0)
        };

        foreach (var shape in shapes)
        {
            Console.WriteLine($"Shape Color: {shape.Color}");
            Console.WriteLine($"Area: {shape.GetArea()}");
            Console.WriteLine();
        }
    }
}
