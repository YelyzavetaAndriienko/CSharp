namespace LI.CSharp.Lab
{
    public abstract class Entity
    {
        public abstract bool Validate();

        public bool IsValid
        {
            get { return Validate(); }
        }

        //public bool IsValidId(Guid id)
        //{
        //    bool result = id.CompareTo(0) >= 0;
        //    if (!result)
        //    {
        //        Console.WriteLine("Invalid id!");
        //    }
        //    return result;
        //}

        public static decimal TransformCurrencyToUSD(Currencies? from, decimal sum)
        {
            switch (from)
            {
                case Currencies.UAH:
                    return sum / (decimal)27.7;
                case Currencies.EUR:
                    return sum / (decimal)1.19;
                case Currencies.USD:
                    return sum;
                case Currencies.GBP:
                    return sum * (decimal)1.37;
                case Currencies.PLN:
                    return sum / (decimal)3.73;
                case Currencies.RUB:
                    return sum / (decimal)71.7;
                default:
                    return sum;
            }
        }

        public static decimal TransformCurrencyFromUSD(Currencies? to, decimal sum)
        {
            switch (to)
            {
                case Currencies.UAH:
                    return sum * (decimal)27.7;
                case Currencies.EUR:
                    return sum * (decimal)1.19;
                case Currencies.USD:
                    return sum;
                case Currencies.GBP:
                    return sum / (decimal)1.37;
                case Currencies.PLN:
                    return sum * (decimal)3.73;
                case Currencies.RUB:
                    return sum * (decimal)71.7;
                default:
                    return sum;
            }
        }

        public static decimal TransformCurrency(Currencies? from, Currencies? to, decimal sum)
        {
            return TransformCurrencyFromUSD(to, TransformCurrencyToUSD(from, sum));
        }
    }
}