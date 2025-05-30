﻿using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Text.Unicode;

namespace TicketManagement.API.Extensions
{
    //  this class to make RequestPipeLine to be able to work , and here must implement this class called GlobalExceptionHandler
    public static class GlobalExceptionHandler
    {
        public static void HandleException(this WebApplication app) { 
        
            app.UseExceptionHandler( o=>
            o.Run( async context=>
            {
                var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                var exception = errorFeature.Error;
                if (!(exception is FluentValidation.ValidationException validationException))
                    throw exception;
                var errors = validationException.Errors.Select(error => new
                {
                    Property = error.PropertyName,
                    Message = error.ErrorMessage
                });
                var errorContent = JsonSerializer.Serialize(errors);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(errorContent, System.Text.Encoding.UTF8);
            } ));
        }
    }
}
