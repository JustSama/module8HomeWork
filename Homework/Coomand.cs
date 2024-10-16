using System;
using System.Collections.Generic;

public interface ICommand
{
    void Execute();
    void Undo();
}

public class Light
{
    public void TurnOn()
    {
        Console.WriteLine("Свет включен.");
    }

    public void TurnOff()
    {
        Console.WriteLine("Свет выключен.");
    }
}

public class Door
{
    public void Open()
    {
        Console.WriteLine("Дверь открыта.");
    }

    public void Close()
    {
        Console.WriteLine("Дверь закрыта.");
    }
}

public class Thermostat
{
    private int temperature;

    public void SetTemperature(int newTemperature)
    {
        temperature = newTemperature;
        Console.WriteLine($"Температура установлена на {temperature}°C.");
    }

    public void IncreaseTemperature()
    {
        temperature++;
        Console.WriteLine($"Температура увеличена на 1°C. Текущая: {temperature}°C.");
    }

    public void DecreaseTemperature()
    {
        temperature--;
        Console.WriteLine($"Температура уменьшена на 1°C. Текущая: {temperature}°C.");
    }
}

public class LightOnCommand : ICommand
{
    private Light light;

    public LightOnCommand(Light light)
    {
        this.light = light;
    }

    public void Execute()
    {
        light.TurnOn();
    }

    public void Undo()
    {
        light.TurnOff();
    }
}

public class LightOffCommand : ICommand
{
    private Light light;

    public LightOffCommand(Light light)
    {
        this.light = light;
    }

    public void Execute()
    {
        light.TurnOff();
    }

    public void Undo()
    {
        light.TurnOn();
    }
}

public class DoorOpenCommand : ICommand
{
    private Door door;

    public DoorOpenCommand(Door door)
    {
        this.door = door;
    }

    public void Execute()
    {
        door.Open();
    }

    public void Undo()
    {
        door.Close();
    }
}

public class DoorCloseCommand : ICommand
{
    private Door door;

    public DoorCloseCommand(Door door)
    {
        this.door = door;
    }

    public void Execute()
    {
        door.Close();
    }

    public void Undo()
    {
        door.Open();
    }
}

public class ThermostatIncreaseCommand : ICommand
{
    private Thermostat thermostat;

    public ThermostatIncreaseCommand(Thermostat thermostat)
    {
        this.thermostat = thermostat;
    }

    public void Execute()
    {
        thermostat.IncreaseTemperature();
    }

    public void Undo()
    {
        thermostat.DecreaseTemperature();
    }
}

public class ThermostatDecreaseCommand : ICommand
{
    private Thermostat thermostat;

    public ThermostatDecreaseCommand(Thermostat thermostat)
    {
        this.thermostat = thermostat;
    }

    public void Execute()
    {
        thermostat.DecreaseTemperature();
    }

    public void Undo()
    {
        thermostat.IncreaseTemperature();
    }
}

public class CommandInvoker
{
    private List<ICommand> commandHistory = new List<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        commandHistory.Add(command);
    }

    public void UndoLastCommand()
    {
        if (commandHistory.Count > 0)
        {
            ICommand lastCommand = commandHistory[^1];
            lastCommand.Undo();
            commandHistory.RemoveAt(commandHistory.Count - 1);
        }
        else
        {
            Console.WriteLine("Нет команд для отмены.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Light light = new Light();
        Door door = new Door();
        Thermostat thermostat = new Thermostat();
        CommandInvoker invoker = new CommandInvoker();

        ICommand lightOn = new LightOnCommand(light);
        ICommand lightOff = new LightOffCommand(light);
        ICommand doorOpen = new DoorOpenCommand(door);
        ICommand doorClose = new DoorCloseCommand(door);
        ICommand tempUp = new ThermostatIncreaseCommand(thermostat);
        ICommand tempDown = new ThermostatDecreaseCommand(thermostat);

        invoker.ExecuteCommand(lightOn);
        invoker.ExecuteCommand(doorOpen);
        invoker.ExecuteCommand(tempUp);
        invoker.ExecuteCommand(lightOff);
        invoker.ExecuteCommand(tempDown);

        invoker.UndoLastCommand();
        invoker.UndoLastCommand();
        invoker.UndoLastCommand();
    }
}
