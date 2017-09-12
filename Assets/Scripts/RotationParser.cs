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

            //parse the tokens
            foreach(var token in tokens)
            {
            }
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
