using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

public class TextValidation
{
	
    public static string RepairText(string t)
    {
        Char[] tc = t.ToCharArray();
        int byteCount = Encoding.UTF8.GetByteCount(tc);
        byte[] b = new byte[byteCount];
        b = Encoding.UTF8.GetBytes(tc);
        int charCount = Encoding.UTF8.GetCharCount(b);
        Char[] tchars = new Char[charCount];
        tchars = Encoding.UTF8.GetChars(b);
        for (int i = 0; i < charCount; i++)
        {
            if (tchars[i] == '”') { tchars[i] = '"'; }
            else if (tchars[i] == '“') { tchars[i] = '"'; }
            else if (tchars[i] == '’') { tchars[i] = '\''; }
            else if (tchars[i] == '‘') { tchars[i] = '\''; }
        }
        b = Encoding.ASCII.GetBytes(tchars);
        tc = Encoding.ASCII.GetChars(b);
        string x = new string(tc);
        return x;
    }
    public static string ProperCase(string src)
    {
        int len = src.Length;

        if (len == 0) return String.Empty;
        if (len == 1) return src.ToUpper();
        string[] words = src.Split(new char[] { ' ' });
        if (words.Length == 1)
        {
            string word = src;
            string firstletter = src.Substring(0, 1);
            string lastletter = src.Substring(len - 1, 1);
            if (firstletter == firstletter.ToLower() || lastletter == lastletter.ToUpper())
                word = src.Substring(0, 1).ToUpper() + src.Substring(1, len - 1).ToLower();
            return word;
        }
        // If multiple words, ProperCase each one and re-assemble the string
        string ret = String.Empty;
        string sep = "";
        foreach (string word in words)
        {
            ret += sep + TextValidation.ProperCase(word);
            sep = " ";
        }
        return ret;
    }
    public static string StripQuotes(string TextField)
    {
        return TextValidation.StripQuotes(TextField, true);
    }
    public static string StripQuotes(string TextField, bool Lower)
    {
        if (TextField == null || TextField == String.Empty) return String.Empty;
        string pattern = TextField.Trim();
        pattern = HttpUtility.HtmlDecode(pattern);
        pattern = pattern.Replace("\"", "");
        pattern = pattern.Replace("!", "");
        if (Lower)
            pattern = pattern.ToLower();
        return pattern;
    }
    public static string SearchString(string TextField)
    {


        string plower = TextValidation.StripQuotes(TextField);
        string pattern = plower;
        if (plower.Contains(" "))
        {
            string[] words = plower.Split(new char[] { ' ' });
             
           
            string sep = "";
            bool hasConnector = false;
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] == "and" || words[i] == "or" || words[i] == "near")
                    hasConnector = true;
            }
            for (int i = 0; i < words.Length; i++)
            {
                // Skip multiple spaces, single characters, and keywords
                if (words[i] == String.Empty || words[i].Length == 1 ) continue;
               

                pattern += sep + words[i];
                sep = hasConnector ? " " : " near ";
            }
        }
        if (pattern.Contains(","))
        {
            pattern = '"' + pattern + '"';
        }
        pattern = "'" + pattern + "'";
        return pattern;
    }
}
