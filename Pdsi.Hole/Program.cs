using System;

namespace Pdsi.Hole
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) {
                try {
                    Console.WriteLine("Enter an equation or Ctrl+C to exit:");
                    var text = Console.ReadLine();
                    var interpreter = new Interpreter(new Lexer(text));
                    var result = interpreter.Expr();
                    Console.WriteLine(result);
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
