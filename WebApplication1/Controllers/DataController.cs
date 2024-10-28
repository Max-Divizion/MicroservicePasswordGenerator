using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private static List<string> dataStore = new List<string>();

        // GET api/data
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(dataStore);
        }

        // POST api/data
        [HttpPost]
        public ActionResult Post([FromBody] string clientData)
        {
            if (string.IsNullOrWhiteSpace(clientData))
            {
                return BadRequest("Data cannot be empty.");
            }

            dataStore.Add(clientData);
            return Ok(new { Message = "Data added successfully.", Data = clientData });
        }

        // GET api/data/GeneratePassword
        [HttpGet("GeneratePassword")]
        public ActionResult<IEnumerable<string>> GeneratePassword(int length = 8, int quantity = 1, bool includeSymbols = true)
        {
            char[] exclusiveCharacters = { '|', '\\', '/', '!', '?', '^', '<', '>', '(', ')', '[', ']', '{', '}', '*', '+', '-', '_', '=', ';', ':' };
            Random rand = new Random();
            List<string> passwords = new List<string>();

            for (int i = 0; i < quantity; i++)
            {
                StringBuilder password = new StringBuilder();

                for (int j = 0; j < length; j++)
                {
                    int type_numb = rand.Next(0, 2);

                    if (type_numb == 0) // Генерация числа
                    {
                        int value = rand.Next(0, 10);
                        password.Append(value);
                    }
                    else if (includeSymbols) // Генерация символа
                    {
                        char value;
                        do
                        {
                            value = (char)rand.Next(33, 125);
                        }
                        while (exclusiveCharacters.Contains(value)); // Проверка на исключённые символы

                        password.Append(value);
                    }
                }

                passwords.Add(password.ToString());
            }

            return Ok(passwords);
        }
    }
}
