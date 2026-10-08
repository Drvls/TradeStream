namespace TradeStream.Domain.Exceptions;

public class UserDisabledException(Guid id) : Exception($"User with id {id} is disabled.");