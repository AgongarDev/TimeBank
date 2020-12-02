using System;
using TimeBank.Bussines.Utilities;
using TimeBank.Core.Models;

namespace TimeBank.ConsoleApp.UserMenu
{
    internal class AddressForm
    {
        public static Address NewAddress(Address address)
        {
            string insertData;

            Console.WriteLine("ADDRESS FORM: ");
            Console.WriteLine("--------------------");

            Console.WriteLine("TYPE STREET : " + address.TypeStreet);
            insertData = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(insertData))
            {
                address.TypeStreet = insertData;
            }

            Console.WriteLine("STREET NAME: " + address.Street);
            insertData = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(insertData))
            {
                address.Street = insertData;
            }

            Console.WriteLine("NUMBER : " + address.Number);
            insertData = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(insertData))
            {
                int n = CommonLib.ValidateNumEntrance(insertData) ? int.Parse(insertData) : 1;
                address.Number = n;
            }

            Console.WriteLine("CITY : " + address.City);
            insertData = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(insertData))
            {
                address.City = insertData;
            }

            Console.WriteLine("POSTAL CODE : " + address.PostalCode);
            insertData = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(insertData))
            {
                int pc = CommonLib.ValidateNumEntrance(insertData) ? int.Parse(insertData) : 1;
                address.PostalCode = pc;
            }

            Console.WriteLine("COMMENTS : " + address.Comments);
            insertData = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(insertData))
            {
                int pc = CommonLib.ValidateNumEntrance(insertData) ? int.Parse(insertData) : 1;
                address.PostalCode = pc;
            }

            return address;
        }
    }
}