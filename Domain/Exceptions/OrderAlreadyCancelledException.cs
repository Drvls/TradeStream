namespace TradeStream.Domain.Exceptions;

public class OrderAlreadyCancelledException(Guid orderId) : Exception($"Order with id '{orderId}' has already been cancelled");