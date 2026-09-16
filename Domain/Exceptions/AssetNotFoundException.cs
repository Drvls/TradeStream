namespace TradeStream.Domain.Exceptions;

public class AssetNotFoundException : Exception
{
    public AssetNotFoundException(string code) : base($"Asset with code '{code}' not found")
    { }
}