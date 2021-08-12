using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebShell.Models;

namespace WebShell.Controllers
{
    [ApiController] 
    [Route("api/[controller]")]
    public class OutputController : ControllerBase 
    {
        private readonly CommandContext _dbContext;

        public OutputController(CommandContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public string Index(string command)
        {
            if (command != null) //добавление команды в бд
            {
                _dbContext.Add(new Command() {Text = command});

                _dbContext.SaveChanges();
            }

            Process cmd = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = "cmd.exe",
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                }
            };

            cmd.Start();
            cmd.StandardInput.WriteLine(command);
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            var output = cmd.StandardOutput.ReadToEnd();//сохранение ответа на команду в переменную 

            output = output.Replace("<", "&lt;").Replace(">", "&gt;").Replace("\n", "</br>"); //форматирование текста для вывода в html

            return output;
        }
    }
}