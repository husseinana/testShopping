using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            Next = next;
            Logger = logger;
            Env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch(Exception ex)
            {
                this.Logger.LogError(ex,ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
               
               var response=new object();
                if(Env.IsDevelopment()) 
                    response = (new ApiException((int)HttpStatusCode.InternalServerError),ex.Message,ex.StackTrace);
                else
                    response = (new ApiException((int)HttpStatusCode.InternalServerError));

                var json = System.Text.Json.JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }
        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment env) 
        {
            this.Next = next;
            this.Env = env;
   
        }
     
   
                                                                                                                                             public RequestDelegate Next { get; }
        public ILogger<ExceptionMiddleware> Logger { get; }
        public IHostEnvironment Env { get; }
    }
}