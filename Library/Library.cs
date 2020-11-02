namespace IO_2
{
    /// <summary>
    /// Class for 
    /// </summary>
    public class Library
    {
        /// <summary>
        /// Przygotowywanie odpowiedzi dla klienta
        /// </summary>
        /// <param name="ans">Numer przesłany przez klienta.</param>
        /// <returns>Wartość ciągu Fibonacciego dla numeru od klienta.</returns>
        public static int Start(int ans)
        {
            return Fib.Fibonacci(ans);
        }
    }

    /// <summary>
    /// Obliczanie wartości ciągu.
    /// </summary>
    class Fib
    {
        /// <summary>
        /// Wykonanie funkcji rekurencyjnej dla podanej wartości.
        /// </summary>
        /// <param name="ans">Wartość int.</param>
        /// <returns>Wartość ciągu Fibonacciego dla numeru od klienta.</returns>
        public static int Fibonacci(int ans)
        {
            if ((ans == 1) || (ans == 2))
                return 1;
            else return Fibonacci(ans - 1) + Fibonacci(ans - 2);
        }
    }
}
