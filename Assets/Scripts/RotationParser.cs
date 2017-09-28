using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class RotationParser
    {
        public Quaternion finalRotation;
        private ArrayThatWorks<string> tokens;
        static Dictionary<string, Quaternion> directions;
        public RotationParser(string strToParse)
        {
            //tokenize the string
            char[] dilimiters = { ' '};
            tokens = new ArrayThatWorks<string>(strToParse.Split(dilimiters));
            finalRotation = Quaternion.identity;
            //parse the tokens
            foreach (string tok in tokens)
            {
                switch (tok)
                {
                    case "+":
                        break;
                    default:
                        if(directions.ContainsKey(tok))
                        {
                            finalRotation *= directions[tok];
                        }
                        else if(tok.Contains("Rot"))
                        {
                            finalRotation *= ParseSubRotation(tok.Substring(3));
                        }
                        break;
                }
            }
            //if (strToParse != "")
            //{
            //    finalRotation = directions[strToParse];
            //}
        }

        private Quaternion ParseSubRotation(string str)
        {
            Vector3 ret = new Vector3();
            char[] dilimiters = { ' ' , '(', ',', ')'};
            tokens = new ArrayThatWorks<string>(str.Split(dilimiters));
            int i = 0;
            ret.x = float.Parse(tokens[1]);
            ret.y = float.Parse(tokens[2]);
            ret.z = float.Parse(tokens[3]);
            return Quaternion.Euler(ret);
        }

        static RotationParser()
        {
            directions = new Dictionary<string, Quaternion> { { "Identity", Quaternion.identity },
                                                              { "Right", Quaternion.Euler(0,90,0) },
                                                              { "Left", Quaternion.Euler(0,-90,0) },
                                                              { "Up", Quaternion.Euler(-90,0,0) },
                                                              { "Down", Quaternion.Euler(-90,0,0) },
                                                              { "RollRight", Quaternion.Euler(0,0,90) },
                                                              { "RollLeft", Quaternion.Euler(0,0,90) }};
        }
    }
}
