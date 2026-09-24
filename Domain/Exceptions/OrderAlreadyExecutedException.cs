namespace TradeStream.Domain.Exceptions;

public class OrderAlreadyExecutedException(Guid orderId) : Exception($"Order with id '{orderId}' has already been executed");