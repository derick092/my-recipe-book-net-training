using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Communication.Response;

public class ResponseErrorJson
{
    public ResponseErrorJson(List<string> errorMessages) => Errors = errorMessages;

    public ResponseErrorJson(string errorMessage) => Errors = [errorMessage];

    public List<string> Errors { get; private set; }
}
