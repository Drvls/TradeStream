namespace TradeStream.Domain.Exceptions;

public class AssetDisabledException(Guid id, string code) : Exception($"Asset with id {id} and code {code} is disabled");