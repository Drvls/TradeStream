namespace TradeStream.Domain.Exceptions;

public class AssetAlreadyEnableException(Guid id, string code) : Exception($"Asset with id {id} and code {code} was already enabled");