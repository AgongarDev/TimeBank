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
            string[] validOptions = new string[] { "I", "R", "L", "P", "F", "B" };
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
                Console.WriteLine("P - PRINT TOKENS");
                Console.WriteLine("F - FILL TOKENS");
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
                    ListTokens();
                    ShowTokenMenu();
                    break;
                case "P":
                    PrintTokens();
                    ShowTokenMenu();
                    break;
                case "F":
                    FillTokens();
                    ShowTokenMenu();
                    break;
                case "B":
                    Program.ShowHomeMenu();
                    break;
            }
        }

        private void FillTokens()
        {
            try
            {
                Console.WriteLine("INSERT FILE NAME OR BLANK FOR DEFAULT");
                string insertfile = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(insertfile))
                {
                    insertfile = "Tokens";
                }
                List<Token> gotTokens = CommonLib.FromXml<List<Token>>("xml", insertfile);
                try
                {
                    foreach (Token t in gotTokens)
                    {
                        _adminMgm.InsertOrUpdate(t);
                    }
                    Console.WriteLine("TOKENS UPDATED");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR UPDATING TOKEN DATA (" + ex.Message + ")");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("NOT VALID TOKENS FOUND ON FILE (" + e.Message + ")");
            }
        }

        private void ListTokens()
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

        private void PrintTokens()
        {
            try
            {
                List<Token> tokens = _adminMgm.GetTokens();
                if (tokens.Count == 0)
                {
                    Console.WriteLine("NOT TOKENS FOUND");
                    return;
                }
                CommonLib.ToXml(tokens, "xml", "Tokens");
            }
            catch (Exception e)
            {
                Console.WriteLine("UNNABLE TO PRINT TOKENS TO Tokens.xml FILE (" + e.Message + ")");
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

