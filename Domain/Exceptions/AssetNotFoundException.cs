namespace TradeStream.Domain.Exceptions;

public class AssetNotFoundException : Exception
{
    public AssetNotFoundException(string code) : base($"Asset with code '{code}' not found"){}
    public AssetNotFoundException(Guid id) : base($"Asset with id '{id}' not found"){}
}