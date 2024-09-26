using System;

// Product Interface
public interface IPizza
{
    void Prepare();
}

// Concrete Product (Margherita Pizza)
public class MargheritaPizza : IPizza
{
    public void Prepare()
    {
        Console.WriteLine("Preparing Margherita Pizza");
    }
}

// Concrete Product (Pepperoni Pizza)
public class PepperoniPizza : IPizza
{
    public void Prepare()
    {
        Console.WriteLine("Preparing Pepperoni Pizza");
    }
}

// Creator (Pizza Factory)
public class PizzaFactory
{
    public static IPizza CreatePizza(string type)
    {
        switch (type)
        {
            case "Margherita":
                return new MargheritaPizza();
            case "Pepperoni":
                return new PepperoniPizza();
            default:
                throw new ArgumentException("Unknown pizza type");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Factory creates pizzas
        IPizza pizza1 = PizzaFactory.CreatePizza("Margherita");
        IPizza pizza2 = PizzaFactory.CreatePizza("Pepperoni");

        pizza1.Prepare();
        pizza2.Prepare();
    }
}
