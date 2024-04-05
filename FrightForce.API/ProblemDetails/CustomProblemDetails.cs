using System.Collections;
using Microsoft.AspNetCore.Mvc;

namespace FrightForce.API.Middleware;

public class CustomProblemDetails : ProblemDetails
{
    public IDictionary CustomData { get; set; }

}