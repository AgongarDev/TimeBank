using System;
using System.Collections.Generic;
using System.Text;
using TimeBank.Bussines.UseCases;
using TimeBank.Bussines.Utilities;
using TimeBank.Core.Models;

namespace TimeBank.ConsoleApp.TokenMenu
{
    class TokenActions
    {
        private AdminManagement _adminMgm;
        public TokenActions()
        {
            _adminMgm = new AdminManagement();
        }

        public void ShowTokenMenu()
        {
            string[] validOptions = new string[] { "I", "R", "L", "B" };
            List<string> Options = new List<string>();
            Options.AddRange(validOptions);
            string choice;

            Console.WriteLine("TOKENS ACTIONS TESTER");
            Console.WriteLine("---------------------");

            do
            {
                Console.WriteLine("SELECT AN OPTION: \n");
                Console.WriteLine("I - INSERT TOKEN");
                Console.WriteLine("R - REMOVE TOKEN");
                Console.WriteLine("L - LIST TOKENS");
                Console.WriteLine("B - BACK");

                choice = Console.ReadLine().ToUpper();
            }
            while (!Options.Contains(choice));

            Token aux = null;
            switch (choice)
            {
                case "I":
                    TokenForm.NewToken();
                    ShowTokenMenu();
                    break;
                case "R":
                    aux = GetToken();
                    if (aux != null)
                    {
                        RemoveToken(aux);
                    }
                    ShowTokenMenu();
                    break;
                case "L":
                    PrintTokens();
                    ShowTokenMenu();
                    break;
                case "B":
                    Program.ShowHomeMenu();
                    break;
            }
        }

        private void PrintTokens()
        {
            List<Token> tokens = _adminMgm.GetTokens();
            if (tokens.Count == 0)
            {
                Console.WriteLine("NOT TOKENS FOUND");
                return;
            }

            foreach (Token token in tokens)
            {
                Console.WriteLine(token.ToString());
            }
        }

        private void RemoveToken(Token aux)
        {
            try
            {
                _adminMgm.RemoveToken(aux);
                Console.WriteLine("TOKEN REMOVED");
            }
            catch (Exception)
            {
                Console.WriteLine("IMPOSSIBLE TO REMOVE TOKEN");
            }
        }

        private Token GetToken()
        {
            Token token = null;
            string o;
            Console.WriteLine("GET TOKEN TO REMOVE");
            Console.WriteLine("-------------------");
            do
            {
                Console.WriteLine("INSERT EXISTING TOKEN ID OR BLANK TO EXIT : ");
                o = Console.ReadLine().ToUpper();
                if (String.IsNullOrWhiteSpace(o))
                {
                    ShowTokenMenu();
                    return null;
                }
                if (CommonLib.ValidateNumEntrance(o))
                {
                    token = _adminMgm.GetTokenById(int.Parse(o));
                }
            } while (token == null);

            return token;
        }

    }
}

