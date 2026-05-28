using Bogus;
using MyRecipeBook.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTestUtilities.Requests;

public class RequestRegisterUserAccountJsonBuilder
{
    public static RequestRegisterUserAccountJson Build() 
    {
        return new Faker<RequestRegisterUserAccountJson>() //bogus page for more details
            .RuleFor(request => request.Name, x => x.Person.FirstName)
            .RuleFor(request => request.Email, (x, user) => x.Internet.Email(user.Name))
            .RuleFor(request => request.Password, x => x.Internet.Password());

    }
}
