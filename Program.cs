using System;
using System.IO;



namespace BMI
{
    class Program
    {
        static void Main(string[] args)
        {
            ControlApp control = new ControlApp(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user_data.txt"));
            control.StartBMIConsole();
        }
    }  
}
