namespace TradeStream.Domain.Exceptions;

public class AssetAlreadyDisableException(Guid id, string code) : Exception($"Asset with id {id} and code {code} was already disabled");