using Quesify.SharedKernel.Utilities.Pluralizations;
using System.Security.Cryptography;
using System.Text;

namespace System;

public static class StringExtensions
{
    public static string ToBase64(this string plainText)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    public static string FromBase64(this string base64EncodedData)
    {
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public static byte[] ToHash(this string text)
    {
        using HashAlgorithm algorithm = SHA256.Create();
        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(text));
    }

    public static string ToHashString(this string text)
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (byte value in text.ToHash())
        {
            stringBuilder.Append(value.ToString("X2"));
        }
        return stringBuilder.ToString();
    }

    public static Guid ToGuid(this string input)
    {
        return Guid.Parse(input);
    }

    public static string AddSpacesToSentence(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return text;
        }

        StringBuilder newText = new StringBuilder(text.Length * 2);
        newText.Append(text[0]);

        for (int i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]) && text[i - 1] != ' ')
            {
                newText.Append(' ');
            }
            newText.Append(text[i]);
        }


        return newText.ToString();
    }

    public static string Singularize(this string word)
    {
        return PluralizationService.Singularize(word);
    }

    public static string Pluralize(this string word)
    {
        return PluralizationService.Pluralize(word);
    }

    public static string Pluralize(this string word, int count)
    {
        return PluralizationService.Pluralize(count, word);
    }

    public static bool IsPlural(this string word)
    {
        return PluralizationService.IsPlural(word);
    }

    public static bool IsSingular(this string word)
    {
        return PluralizationService.IsSingular(word);
    }

    public static string StripPrefix(this string text, string prefix)
    {
        return text.StartsWith(prefix) ? text.Substring(prefix.Length) : text;
    }

    public static string StripSuffix(this string text, string suffix)
    {
        return text.EndsWith(suffix) ? text.Substring(0, text.Length - suffix.Length) : text;
    }
    
    public static string AppendToStart(this string text, string prefix)
    {
        return prefix + text;
    }

    public static string AppendToEnd(this string text, string suffix)
    {
        return text + suffix;
    }

    public static bool IsNullOrEmpty(this string? text)
    {
        return string.IsNullOrEmpty(text);
    }

    public static bool IsNullOrWhiteSpace(this string? text)
    {
        return string.IsNullOrWhiteSpace(text);
    }
}
