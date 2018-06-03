using System;
using System.Threading.Tasks;
namespace CustomMiddleware
{
   public delegate Task RequestDelegate(HttpContext context);
}
