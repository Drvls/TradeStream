namespace TradeStream.Domain.Exceptions;

public class AssetNotFoundException(string code) : Exception($"Asset with code '{code}' not found");