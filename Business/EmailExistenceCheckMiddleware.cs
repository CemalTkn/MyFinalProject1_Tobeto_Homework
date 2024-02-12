using Core.Entities.Concrete;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    //public class EmailExistenceCheckMiddleware
    //{
    //    private readonly RequestDelegate _next;
    //    private readonly IUserDal _userDal;

    //    public EmailExistenceCheckMiddleware(RequestDelegate next, IUserDal userDal)
    //    {
    //        _next = next;
    //        _userDal = userDal;
    //    }

    //    public async Task Invoke(HttpContext httpContext)
    //    {
    //        // Kullanıcı e-postasını al
    //        var userEmail = httpContext.Request.Headers["User-Email"].FirstOrDefault();

    //        if (!string.IsNullOrEmpty(userEmail))
    //        {
    //            // E-posta ile kullanıcı kontrolü
    //            User? existingUser = await _userDal.Get(i => i.Email == userEmail);

    //            if (existingUser != null)
    //            {
    //                // Eğer aynı e-posta ile kayıtlı bir kullanıcı varsa, hata döndür
    //                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
    //                await httpContext.Response.WriteAsync("Bu e-posta ile bir kayıt zaten var.");
    //                return;
    //            }
    //        }

    //        // Eğer herhangi bir sorun yoksa, diğer middleware veya controller'a geç
    //        await _next(httpContext);
    //    }
    //}
}
