namespace Kalmia.Core.Common;

// unit represents the absence of a meaningful value, used as type argument in generic types
// that require a real type parameter (C# doesn't allow void there), ie Result<T> -> Result<Unit>
public readonly struct Unit
{
    public static readonly Unit Value = new();
}
