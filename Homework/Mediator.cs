using System;
using System.Collections.Generic;

public interface IMediator
{
    void SendMessage(string message, User user, User recipient = null);
    void RegisterUser(User user);
}

public class ChatRoom : IMediator
{
    private List<User> users = new List<User>();

    public void SendMessage(string message, User user, User recipient = null)
    {
        if (recipient != null)
        {
            recipient.ReceiveMessage(message, user.Name);
        }
        else
        {
            foreach (var u in users)
            {
                if (u != user)
                {
                    u.ReceiveMessage(message, user.Name);
                }
            }
        }
    }

    public void RegisterUser(User user)
    {
        users.Add(user);
        user.SetMediator(this);
    }
}

public class User
{
    public string Name { get; private set; }
    private IMediator mediator;

    public User(string name)
    {
        Name = name;
    }

    public void SetMediator(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public void SendMessage(string message, User recipient = null)
    {
        if (mediator == null)
        {
            Console.WriteLine($"{Name} не может отправлять сообщения, так как не зарегистрирован в чате.");
            return;
        }

        mediator.SendMessage(message, this, recipient);
    }

    public void ReceiveMessage(string message, string sender)
    {
        Console.WriteLine($"{Name} получил сообщение от {sender}: {message}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        ChatRoom chatRoom = new ChatRoom();

        User user1 = new User("Алекс");
        User user2 = new User("Мария");
        User user3 = new User("Иван");

        chatRoom.RegisterUser(user1);
        chatRoom.RegisterUser(user2);
        chatRoom.RegisterUser(user3);

        user1.SendMessage("Привет всем!");
        user2.SendMessage("Привет, Алекс!");
        user3.SendMessage("Привет, Мария!", user2);
    }
}
