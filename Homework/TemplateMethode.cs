using System;

public abstract class Beverage
{
    public void PrepareRecipe()
    {
        BoilWater();
        Brew();
        PourInCup();
        if (CustomerWantsCondiments())
        {
            AddCondiments();
        }
    }

    protected abstract void Brew();
    protected abstract void AddCondiments();

    private void BoilWater()
    {
        Console.WriteLine("Кипячение воды.");
    }

    private void PourInCup()
    {
        Console.WriteLine("Наливание в чашку.");
    }

    protected virtual bool CustomerWantsCondiments()
    {
        return true;
    }
}

public class Tea : Beverage
{
    protected override void Brew()
    {
        Console.WriteLine("Заваривание чая.");
    }

    protected override void AddCondiments()
    {
        Console.WriteLine("Добавление лимона.");
    }

    protected override bool CustomerWantsCondiments()
    {
        Console.Write("Хотите добавить лимон в чай? (y/n): ");
        string answer = Console.ReadLine();
        return answer?.ToLower() == "y";
    }
}

public class Coffee : Beverage
{
    protected override void Brew()
    {
        Console.WriteLine("Заваривание кофе.");
    }

    protected override void AddCondiments()
    {
        Console.WriteLine("Добавление сахара и молока.");
    }

    protected override bool CustomerWantsCondiments()
    {
        Console.Write("Хотите добавить сахар и молоко в кофе? (y/n): ");
        string answer = Console.ReadLine();
        return answer?.ToLower() == "y";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Beverage tea = new Tea();
        tea.PrepareRecipe();

        Console.WriteLine();

        Beverage coffee = new Coffee();
        coffee.PrepareRecipe();
    }
}
