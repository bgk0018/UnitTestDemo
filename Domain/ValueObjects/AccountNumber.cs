using System;
using System.Linq;

namespace Domain.ValueObjects
{
    public struct AccountNumber
    {
        private readonly string number;

        public string Value { get { return number; }}

        public AccountNumber(string number)
        {
            if (number == null)
                throw new ArgumentNullException("number", "AccountNumber cannot be null");

            if(number.Length != 10)
                throw new ArgumentOutOfRangeException("number", number, "Must be exactly 10 characters");

            if(!number.Substring(0, 8).All(char.IsDigit))
                throw new ArgumentException("All characters before the hyphen need to be digits");

            if(number[8] != '-')
                throw new ArgumentException("Position 9 in the AccountNumber needs to be a hyphen");

            if (!char.IsDigit(number[9]))
                throw new ArgumentException("Character following hyphen needs to be a digit");

            if (Convert.ToInt32(number[9].ToString()) > 6)
                throw new ArgumentException("Position 10 in the AccountNumber needs to be 5 or less");

            this.number = number;
        }

        public static implicit operator AccountNumber(string value)
        {
            return new AccountNumber(value);
        }

        public static implicit operator string(AccountNumber number)
        {
            return number.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is AccountNumber && this == (AccountNumber) obj;
        }

        public bool Equals(AccountNumber other)
        {
            return string.Equals(number, other.number);
        }

        public override int GetHashCode()
        {
            return (number != null ? number.GetHashCode() : 0);
        }

        public static bool operator ==(AccountNumber first, AccountNumber second)
        {
            return first.number == second.number;
        }

        public static bool operator !=(AccountNumber first, AccountNumber second)
        {
            return first.number != second.number;
        }
    }
}
