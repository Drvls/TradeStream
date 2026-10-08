namespace TradeStream.Domain.Entities;

public class User
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public decimal Balance { get; private set; }
    public List<Position> Positions { get; set; } = [];
    public List<Order> Orders { get; set; } = [];
    public bool IsActive { get; set; } = true;

    public void Deposit(decimal amount){
        Balance += amount;
    }

    public void Withdraw(decimal amount){
        Balance -= amount;
    }
    
    public void Enable()
    {
        IsActive = true;
    }

    public void Disable()
    {
        IsActive = false;
    }
}