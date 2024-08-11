using Rental.WebApi.Shared.Domain.Exceptions;
using Rental.WebApi.Shared.Extensions;

namespace Rental.WebApi.Shared.Domain.Objects
{
    public class Cpf
    {
        public const int CpfMaxLenght = 11;
        public string Numero { get; private set; }

        protected Cpf() { }

        public Cpf(string numero)
        {
            if (!Validate(numero)) 
                throw new DomainException("Invalid CPF !");

            Numero = numero;
        }

        public static bool Validate(string cpf)
        {
            cpf = cpf.OnlyNumber(cpf);

            if (cpf.Length != 11)
                cpf = cpf.PadLeft(11, '0');

            if (cpf.Distinct().Count() == 1 || cpf == "12345678909")
                return false;
            

            int[] numbers = cpf.Select(c => int.Parse(c.ToString())).ToArray();

            if (!IsValidDigit(numbers, 9) || !IsValidDigit(numbers, 10))
                return false;

            return true;
        }

        private static bool IsValidDigit(int[] numbers, int position)
        {
            int sum = 0;
            int factor = position + 1;

            for (int i = 0; i < position; i++)
            {
                sum += numbers[i] * (factor - i);
            }

            int result = sum % 11;
            int expectedDigit = (result < 2) ? 0 : 11 - result;

            return numbers[position] == expectedDigit;
        }
    }
}
