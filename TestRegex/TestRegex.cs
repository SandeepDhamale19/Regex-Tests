using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAutomation.Framework.Helpers.Regex;

namespace TestRegex
{
    [TestClass]
    public class TestRegex
    {
        Regex regex;
        string resultMatch;
        MatchCollection resultsMatch;
        string resultReplace = string.Empty; 
        string[] resultSplit;

        [TestMethod]
        public void MatchBetweenCharsIncluded()
        {
            string express = string.Format(RegexHelper.GetMatchBetweenCharsIncluded(), "[", "]");
            regex = new Regex(express);

            resultMatch = regex.Match("This is (yet) [another] (longer) test.").Value; 
            resultReplace = regex.Replace("This is (yet) [another] (longer) test.", "");
            resultReplace = regex.Replace("This is (yet) [another] (longer) test.", "($1)");
            resultSplit = regex.Split("This is (yet) [another] (longer) test.");
        }

        [TestMethod]
        public void GetFirstNChars()
        {            
            regex = new Regex(string.Format(RegexHelper.GetGetFirstNChars(), "{0,5}"));

            resultMatch = regex.Match("This is (yet) [another] (longer) test.").Value;
            
        }

        [TestMethod]
        public void MatchBetweenCharsExcluded()
        {
            string express = string.Format(RegexHelper.GetMatchBetweenCharsExcluded(), "[", "]");
            regex = new Regex(express);

            resultMatch = regex.Match("This is (yet) [another] (longer) test.").Value;
            resultReplace = regex.Replace("This is (yet) [another] (longer) test.", "");
            resultReplace = regex.Replace("This is (yet) [another] (longer) test.", "($1)");
            resultSplit = regex.Split("This is (yet) [another] (longer) test.");
        }

        [TestMethod]
        public void SplitByChars()
        {
            string express = string.Format(RegexHelper.GetSplitByAllChars(), "(y");
            regex = new Regex(express);
            resultSplit = regex.Split("This is (yet) [another] (longer) test.");
        }

        [TestMethod]
        public void StartsWith()
        {
            string express = string.Format(RegexHelper.GetStartsWith(), "T");
            regex = new Regex(express);
            resultsMatch = regex.Matches("This is yet another longer Test.");

            foreach (Match m in resultsMatch)
            {
                Console.WriteLine(m);
            }
        }

        [TestMethod]
        public void ReplaceWithExcludeCharBoundries()
        {
            string express = string.Format(RegexHelper.GetReplaceWithExcludeCharBoundries(), "[", "]", "/");
            regex = new Regex(express);
            resultReplace = regex.Replace("//div/span/div/input[@link='https://www.facebook.com/home']", ">");

            foreach (Match m in resultsMatch)
            {
                Console.WriteLine(m);
            }
        }

        [TestMethod]
        public void TestRegexMethod()
        {
            string result = RegexHelper.GenerateRegexString(@"[A-Z]|[0-9]|\b\d");

            // add word
            result = RegexHelper.GenerateRegexString(@"[A-Z]|[0-9]|\b\d\w");

            //remove spaces
            result = RegexHelper.GenerateRegexString(@"[A-Z]|[0-9]|\b\d\w\s");

            //add tab
            result = RegexHelper.GenerateRegexString(@"[A-Z]|[0-9]|\b\d\w\t");
        }

        [TestMethod]
        public void TestNthOccurence()
        {
            string input = "This is title1 from book title1 of library title1 of city title1";
            string output = "";

            //Replace string at Nth occurence
            int occurenceToReplace = 1;
            int noOfOccurenceToReplace = 2;
            string currentString = "title1";
            string newString = "title2";
            int matchIndex = Regex.Matches(input, currentString)[occurenceToReplace - 1].Index;
            Regex regex = new Regex(currentString);

            // Parameters: inputString, replaceString, noOfOccurences, startIndex
            output = regex.Replace(input, newString, noOfOccurenceToReplace, matchIndex);
		    //Console.WriteLine(output);
            		    
            output = Regex.Replace(input, @"(?<=title1.*)title1", "title2");		
		    //Console.WriteLine(output); //This is title1 from book title2 of library title2 of city title2

            int i = 1;
            output = Regex.Replace(input, @"title1", m => "title" + i++);
		    //Console.WriteLine(output); //This is title1 from book title2 of library title3 of city title4
        }

        [TestMethod]
        public void TestGetSubstringTillFirstOccurence()
        {
            string express = string.Format(RegexHelper.GetSubstringTillFirstOcuurence(), "title");
            regex = new Regex(express);
            resultMatch = regex.Match("This title is of a book title of library title of city title.").Value;
        }

        [TestMethod]
        public void TestGenerateStringWithSubstring()
        {
            RegexHelper.GenerateStringWithSubstring("-", -1,-1);
        }

        [TestMethod]
        public void MatchCollectionExample()
        {
            string express = string.Format(RegexHelper.GetDateMMMddFormat());
            regex = new Regex(express);

            MatchCollection matches = regex.Matches("June 24, August 9, Dec 12");

            // This will print the number of matches
            Console.WriteLine("{0} matches", matches.Count);

            // This will print each of the matches and the index in the input string
            // where the match was found:
            //   June 24 at index [0, 7)
            //   August 9 at index [9, 17)
            //   Dec 12 at index [19, 25)
            foreach (Match match in matches)
            {
                Console.WriteLine("Match: {0} at index [{1}, {2})",
                    match.Value,
                    match.Index,
                    match.Index + match.Length);
            }

            // For each match, we can extract the captured information by reading the 
            // captured groups.
            foreach (Match match in matches)
            {
                GroupCollection data = match.Groups;
                // This will print the number of captured groups in this match
                Console.WriteLine("{0} groups captured in {1}", data.Count, match.Value);

                // This will print the month and day of each match.  Remember that the
                // first group is always the whole matched text, so the month starts at
                // index 1 instead.
                Console.WriteLine("Month: " + data[1] + ", Day: " + data[2]);

                // Each Group in the collection also has an Index and Length member,
                // which stores where in the input string that the group was found.
                Console.WriteLine("Month found at[{0}, {1})",
                    data[1].Index,
                    data[1].Index + data[1].Length);
            }
        }
    }
}
