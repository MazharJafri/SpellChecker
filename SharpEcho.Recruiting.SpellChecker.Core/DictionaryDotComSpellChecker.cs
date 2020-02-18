using SharpEcho.Recruiting.SpellChecker.Contracts;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;

namespace SharpEcho.Recruiting.SpellChecker.Core
{
    /// <summary>
    /// This is a dictionary based spell checker that uses dictionary.com to determine if
    /// a word is spelled correctly
    /// 
    /// The URL to do this looks like this: http://dictionary.reference.com/browse/"<word>"
    /// where <word> is the word to be checked
    /// 
    /// Example: http://dictionary.reference.com/browse/SharpEcho would lookup the word SharpEcho
    /// 
    /// We look for something in the response that gives us a clear indication whether the
    /// word is spelled correctly or not
    /// </summary>

    public class DictionaryDotComSpellChecker : ISpellChecker
    {
        private readonly IHttpClient _http;     
        private readonly string _uri;

        public DictionaryDotComSpellChecker(IHttpClient http,string uri)
        {
            _http = http;
            _uri = uri;
        }

        public bool Check(string word)
        {
            HttpResponseMessage response = this._http.GetAsync(this._uri + word).Result;
            return response.StatusCode == HttpStatusCode.OK;
        }

        
    }
}
