using System;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using SharpEcho.Recruiting.SpellChecker.Contracts;
using SharpEcho.Recruiting.SpellChecker.Core;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharpEcho.Recruiting.SpellCheckerConsole
{
    /// <summary>
    /// Thank you for your interest in a position at SharpEcho.  The following are the "requirements" for this project:
    /// 
    /// 1. Implent Main() below so that a user can input a sentance.  Each word in that
    ///    sentance will be evaluated with the SpellChecker, which returns true for a word
    ///    that is spelled correctly and false for a word that is spelled incorrectly.  Display
    ///    out each *distnict* word that is misspelled.  That is, if a user uses the same misspelled
    ///    word more than once, simply output that word one time.
    ///    
    ///    Example:
    ///    Please enter a sentance: Salley sells seashellss by the seashore.  The shells Salley sells are surely by the sea.
    ///    Misspelled words: Salley seashellss
    ///    
    /// 2. The concrete implementation of SpellChecker depends on two other implementations of ISpellChecker, DictionaryDotComSpellChecker
    ///    and MnemonicSpellCheckerIBeforeE.  You will need to implement those classes.  See those classes for details.
    ///    
    /// 3. There are covering unit tests in the SharpEcho.Recruiting.SpellChecker.Tests library that should be implemented as well.
    /// </summary>
    class Program
    {
        
        /// <summary>
        /// This application is intended to allow a user enter some text (a sentence)
        /// and it will display a distinct list of incorrectly spelled words
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.Write("Please enter a sentence: ");
            var sentence = Console.ReadLine();           
            initiateSSLConfiguration();

            // first break the sentance up into words, 
            // then iterate through the list of words using the spell checker
            // capturing distinct words that are misspelled
     
            // use this spellChecker to evaluate the words
            var spellChecker = new SpellChecker.Core.SpellChecker
                (
                    new ISpellChecker[]
                    {
                        new MnemonicSpellCheckerIBeforeE(),
                        new DictionaryDotComSpellChecker(new HttpHandler(),getDictionaryUri()),
                    }
                );

            var words = Regex.Replace(sentence, @"[ ](?=[ ])|[^-_,A-Za-z0-9 ]+", "")  //remove special characters & numbers 
                             .Split(' ')  //sentence to words
                             .Distinct(); //remove duplicates

            var mistakes = words.Where( x => !spellChecker.Check(x));

            if (mistakes.Count() > 0)
            {
                Console.Write("Misspelled words: ");
                Console.WriteLine(string.Join(" ", mistakes.Select(x => x)));               
            }
            Console.ReadLine();
        }

        
        //we can used it if we have multiple clients to choose dictionary endpoints
        private static string getDictionaryUri() => "https://dictionary.reference.com/browse/";


        private static IEnumerable<string> convertIntoWords(string data) => data
                        .Split(' ')
                        .Distinct();

        private static void initiateSSLConfiguration()
        {
            const SslProtocols _Tls12 = (SslProtocols)0x00000C00;
            const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;
            ServicePointManager.SecurityProtocol = Tls12;
        }
    } 
}

