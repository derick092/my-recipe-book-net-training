using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MyRecipeBook.Domain.Extensions;

public static class StringExtension
{
    public static bool IsNotEmpty([NotNullWhen(true)] this string? value) 
    {
        return !string.IsNullOrWhiteSpace(value);
    }
}
