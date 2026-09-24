namespace TradeStream.Domain.Exceptions;

public class OrderAlreadyRejectedException(Guid id) : Exception($"Order with id '{id}' has already been rejected");