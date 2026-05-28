using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Communication.Response;

public class ResponseRegisteredUserJson
{
    public string Name { get; set; } = string.Empty;
    public ResponseTokensJson Tokens { get; set; } = new();
}
