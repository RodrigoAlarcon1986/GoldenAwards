﻿using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace GA.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var friendlyErrorMessage = "Erro não tratado na aplicação, verifique os logs internos.";
                await HandleErrorAsync(context, friendlyErrorMessage, ex.ToString(), HttpStatusCode.InternalServerError);
            }
        }

        private Task HandleErrorAsync(HttpContext context, string friendlyErrorMessage, string logErrorMessage, HttpStatusCode code)
        {
            _logger.LogError(logErrorMessage);

            var problemDetails = new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", [friendlyErrorMessage] }
            });

            var result = JsonSerializer.Serialize(problemDetails);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
