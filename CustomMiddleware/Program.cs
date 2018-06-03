using System;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace CustomMiddleware
{
    class Program
    {

        private static IList<Func<RequestDelegate, RequestDelegate>> _list = new List<Func<RequestDelegate, RequestDelegate>>();
        static void Main(string[] args)
        {
            Use(next =>
            {
                return context =>
                {
                    Console.WriteLine("1");
                    return next(context);
                };
            });

            Use(next =>
            {
                return context =>
                {
                    Console.WriteLine("2");
                    return next.Invoke(context);
                };
            });

            RequestDelegate end = (context) =>
            {
                Console.WriteLine("End ...");
                return Task.CompletedTask;
            };

            foreach (var item in _list)
            {
                end = item.Invoke(end);
            }

            end.Invoke(new HttpContext());
            Console.ReadLine();



        }

        public static void Use(Func<RequestDelegate, RequestDelegate> middleWare)
        {
            _list.Add(middleWare);
        }

    }
}
