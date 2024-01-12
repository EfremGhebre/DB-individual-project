using Microsoft.EntityFrameworkCore;
using NewAgeHS.Models;
using Spectre.Console;

namespace NewAgeHS
{
    internal class Program
    {
        static void Main(string[] args)
        {          
            App app = new();
            app.RunApp();            
        }
    }
}