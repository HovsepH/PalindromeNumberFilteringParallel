using System.Collections.Concurrent;

namespace PalindromeNumberFiltering;

/// <summary>
/// A static class containing methods for filtering palindrome numbers from a collection of integers.
/// </summary>
public static class Selector
{
    /// <summary>
    /// Retrieves a collection of palindrome numbers from the given list of integers using concurrent filtering.
    /// </summary>
    /// <param name="numbers">The list of integers to filter.</param>
    /// <returns>A collection of palindrome numbers.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the input list 'numbers' is null.</exception>
    public static IList<int> GetPalindromes(IList<int> numbers)
    {
        ConcurrentBag<int> palindromes = [];
        _ = Parallel.ForEach(numbers, number =>
        {
            if (IsPalindrome(number))
            {
                palindromes.Add(number);
            }
        });

        return palindromes.ToList();
    }

    /// <summary>
    /// Checks whether the given integer is a palindrome number.
    /// </summary>
    /// <param name="number">The integer to check.</param>
    /// <returns>True if the number is a palindrome, otherwise false.</returns>
    private static bool IsPalindrome(int number)
    {
        if (number < 0)
        {
            return false;
        }

        int left = 0;
        int right = GetLength(number) - 1;
        return IsPositiveNumberPalindrome(number, left, right);
    }

    /// <summary>
    /// Recursively checks whether a positive number is a palindrome.
    /// </summary>
    /// <param name="number">The positive number to check.</param>
    /// <param name="left">The index of the leftmost digit to compare.</param>
    /// <param name="right">The index of the rightmost digit to compare.</param>
    /// <returns>True if the positive number is a palindrome, otherwise false.</returns>
    private static bool IsPositiveNumberPalindrome(int number, int left, int right)
    {
        if (left < 0 || right < 0 || left >= GetLength(number) || right >= GetLength(number))
        {
            throw new InvalidOperationException();
        }

        if (left >= right)
        {
            return true;
        }

        if (GetDigitInDecimalPlace(number, left) == GetDigitInDecimalPlace(number, right))
        {
            return IsPositiveNumberPalindrome(number, left + 1, right - 1);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Retrieves the digit at a specified decimal place in a given number.
    /// </summary>
    /// <param name="number">The number from which to retrieve the digit.</param>
    /// <param name="decimalPlace">The decimal place (index) of the desired digit, starting from the rightmost digit (ones place).</param>
    /// <returns>The digit at the specified decimal place.</returns>
    private static int GetDigitInDecimalPlace(int number, int decimalPlace)
    {
        if (decimalPlace < 0 || GetLength(number) <= decimalPlace)
        {
            throw new InvalidOperationException();
        }

        int remainder = 0;

        while (decimalPlace >= 0)
        {
            remainder = number % 10;
            number /= 10;
            --decimalPlace;
        }

        return remainder;
    }

    /// <summary>
    /// Gets the number of digits in the given integer.
    /// </summary>
    /// <param name="number">The integer to count digits for.</param>
    /// <returns>The number of digits in the integer.</returns>
    private static byte GetLength(int number)
    {
        if (number < 0)
        {
            number = -number;
        }

        switch (number)
        {
            case >= 1000000000:
                return 10;
            case >= 100000000:
                return 9;
            case >= 10000000:
                return 8;
            case >= 1000000:
                return 7;
            case >= 100000:
                return 6;
            case >= 10000:
                return 5;
            case >= 1000:
                return 4;
            case >= 100:
                return 3;
            case >= 10:
                return 2;
            default:
                return 1;
        }
    }
}
