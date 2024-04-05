using System.Net;
using System.Text.Json;
using FrightForce.API.Middleware;
using FrightForce.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FrightForce.API;

public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly Dictionary<Type, (int StatusCode, string Title)> _exceptionMappings =
            new()
            {
                { typeof(BusinessException.ValueAlreadyExistException), (409, "Already Exists.") },
                { typeof(BusinessException.NotFoundException), (404, "Not Found.") }
            };

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception was thrown during the request.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var exceptionType = ex.GetType();
            if (_exceptionMappings.TryGetValue(exceptionType, out var mapping))
            {
                context.Response.StatusCode = mapping.StatusCode;
                return context.Response.WriteAsync(JsonSerializer.Serialize(new CustomProblemDetails()
                {
                    Status = context.Response.StatusCode,
                    Title = mapping.Title,
                    Detail = ex.Message,
                    Instance = _httpContextAccessor.HttpContext.Request.Path,
                    CustomData = ex.Data
                }));
            }

            // If exception is not mapped this is the default response
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails
            {
                Status = context.Response.StatusCode,
                Title = "FrightForce Application Error",
                Detail = ex.Message
            }));
        }
    }