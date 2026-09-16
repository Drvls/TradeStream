namespace TradeStream.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Balance { get; private set; }
    public List<Position> Positions { get; set; } = [];
    public List<Order> Orders { get; set; } = [];

    public void Deposit(decimal amount){
        Balance += amount;
    }

    public void Withdraw(decimal amount){
        Balance -= amount;
    }
}