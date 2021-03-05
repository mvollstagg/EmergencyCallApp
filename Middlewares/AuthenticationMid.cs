﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RequestApiService.Models.Authenticate
{
    public class AuthenticationMid
    {
        private readonly RequestDelegate _next;

        // Dependency Injection
        public AuthenticationMid(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IServiceProvider serviceProvider)
        {
            //Reading the AuthHeader which is signed with JWT
            string authHeader = context.Request.Headers["Authorization"];
            //token autheader
            //context.Request.Path.Value => "api/Login/Login" mesela

            //bu kod sadece login işlemine izin verir gerisine izin vermez
            if(context.Request.Path.Value=="api/Login/Login")
                await _next(context);
            else
                //Bu kısımda autheaderı tokenı çözümleyip claimslere yerleştiriceksin
                //Controller kısmında claimsler ile mevcut requesti yapan userId falan erişebilmek için
                context.Response.StatusCode = 401;


            //Aldığın tokenı çözümle
            //Gitmek istediği urlyi de elde edebiliyorsun burda
            //Yetkisi varsa "await _next(context);" ile geçiş izni verebilirsin
            //Yetkisi yoksa "context.Response.StatusCode = 401;" ile unothorized hatası döner


        }



    }
}
