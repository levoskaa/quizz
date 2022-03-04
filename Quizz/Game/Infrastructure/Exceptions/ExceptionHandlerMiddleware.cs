using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Quizz.Common;
using Quizz.Common.ViewModels;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Quizz.GameService.Infrastructure.Exceptions
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IWebHostEnvironment env;
        private readonly IMapper mapper;

        public ExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env, IMapper mapper)
        {
            this.next = next;
            this.env = env;
            this.mapper = mapper;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Call the next delegate/middleware in the pipeline.
                await next(httpContext);
            }
            catch (GameServiceDomainException e)
            {
                await SendResponseAsync(
                    httpContext,
                    HttpStatusCode.BadRequest,
                    mapper.Map<ErrorViewModel>(e));
            }
            catch (Exception e)
            {
                await SendResponseAsync(
                    httpContext,
                    HttpStatusCode.InternalServerError,
                    new ErrorViewModel
                    {
                        Message = "Unexpected error occured",
                        StackTrace = e.StackTrace,
                    });
            }
        }

        private async Task SendResponseAsync(HttpContext httpContext, HttpStatusCode statusCode, ErrorViewModel errorViewModel)
        {
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = (int)statusCode;
            httpContext.Response.ContentType = "application/json";

            var json = JsonConvert.SerializeObject(
                errorViewModel,
                new JsonSerializerSettings
                {
                    ContractResolver = new ErrorContractResolver(env.IsDevelopment()),
                });

            await httpContext.Response.WriteAsync(json);
        }
    }
}