/*
 *  @Author: Adam Wyrembelski
 *  @Data: 12.10.2012.
 *  @Project: NaiveBayesClassifier 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Implementation
{
    public class InformationModel<TFeature>
    {
        /// <summary>
        /// Object category
        /// </summary>
        public string Lable { get; set; }
        
        /// <summary>
        /// List of object features 
        /// </summary>
        public List<TFeature> Features { get; set; }
    }
    public class DBSet
    {
        public string Major { get; set; }

        public string Skill { get; set; }

        public string SkillRate { get; set; }

        public string Interest { get; set; }

        public string InterestRate { get; set; }
    }
}
