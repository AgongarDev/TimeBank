﻿using System.Collections.Generic;

namespace TimeBank.Core.Models
{
    public class Wallet
    {
        public Wallet()
        {
            Debit = new List<Token>();
            Credit = new List<Token>();
        }
        public long ID { get; set; }
        public int MaxDebit { get; set; }
        public int MinCredit { get; set; }
        

        public long TBankUserID { get; set; }
        public User User { get; set; }

        public List<Token> Debit { get; set; }
        public List<Token> Credit { get; set; }

        public override string ToString()
        {
            if (Debit == null) Debit = new List<Token>();
            string debit = string.Empty;
            foreach (Token t in Debit)
            {
                debit += "\n" + t; 
            }
            if (Credit == null) Credit = new List<Token>();
            string credit = string.Empty;
            foreach (Token t in Credit)
            {
                credit += "\n" + t;
            }

            return $"Max Debit : {MaxDebit} - Min Credit : {MinCredit} - Current Debit : {debit} \n Current Credit : {credit}"; 
        }
    }
}