//////////////////////////////////////////////////////////////////////////////////////
//
//  Project: Project 3 - DNAAnalysis
//  File Name: DetoxProfileDriver.cs
//  Description: Performs a detox analysis on a supplied DNA file 
//  Course: CSCI 3230-001 - Algorithms
//  Author: Seth Norton, nortonsp@etsu.edu
//  Created: May 19, 2020
//  Copyright: Seth Norton, 2020
//
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The name space for the program which runs a detox report on a DNA file
/// </summary>
namespace DetoxProfile
{
    /// <summary>
    /// The class which runs a detox report on a DNA file
    /// </summary>
    public class DetoxProfileDriver
    {

        /// <summary>
        /// A structure used for storing info on a particular rs number
        /// </summary>
        struct DNAInfo
        {
            /// <summary>
            /// The RSID (rs number) of a particular gene
            /// </summary>
            public string RSID { get; set; }
            /// <summary>
            /// The chromosome of a particular gene
            /// </summary>
            public int Chromosome { get; set; }
            /// <summary>
            /// The position of a gene
            /// </summary>
            public int Position { get; set; }
            /// <summary>
            /// The first allele for a gene
            /// </summary>
            public string AlleleOne { get; set; }
            /// <summary>
            /// The second allele for a gene
            /// </summary>
            public string AlleleTwo { get; set; }

            /// <summary>
            /// Tells whether or not the gene has any permutation. (-/- - no permutation, +/- - one permutation alelle one, -/+ - one permutation alelle two and +/+ - two mutations)
            /// </summary>
            public string Mutation { get; set; }


        }//end struct DNAInfo

        /// <summary>
        /// The driver for the main method of the program which will conduct a detox report on the DNA file given
        /// </summary>
        /// <param name="args">The arguments supplied to the program</param>
        public static void Main(string[] args)
        {

            List<DNAInfo> DNAInfoList = new List<DNAInfo>();    //The list which holds the info for a DNA file
            DNAInfo da = new DNAInfo();
            string DNAInfo = "";                                //a string used to grab DNA info
            int counter = 0;                                    //used to determine when the program should start grabbing rs numbers
            DNAInfo = Console.ReadLine();
            while (DNAInfo != null)
            {
                DNAInfo = Console.ReadLine();
                if (counter > 0 && DNAInfo != null)
                {
                    da.RSID = DNAInfo.Split('\t')[0];
                    da.Chromosome = Int32.Parse(DNAInfo.Split('\t')[1]);
                    da.Position = Int32.Parse(DNAInfo.Split('\t')[2]);
                    da.AlleleOne = DNAInfo.Split('\t')[3];
                    da.AlleleTwo = DNAInfo.Split('\t')[4];
                    DNAInfoList.Add(da);
                }//end if (counter > 0 && DNAInfo != null)
                if (counter == 0 && DNAInfo[0] != '#')
                    counter++;

            }//end  while (DNAInfo != null)
            //Add the associated allelles
            Dictionary<string, string> AlelleDictionary = new Dictionary<string, string>    //a dictionary which holds the dominant alleles for the rs values in question
            {
                {"rs1048943","T;T"},
                {"rs4986883","A;A"},
                {"rs1799814","C;C"},
                {"rs762551","A;A"},
                {"rs1056836","A;A"},
                {"rs1800440","T;T"},
                {"rs10012","G;G"},
                {"rs1801272","A;A"},
                {"rs28399444","AA;AA"},
                {"rs1799853","C;C"},
                {"rs12248560","C;C"},
                {"rs1135840","G;G"},
                {"rs1065852","G;G"},
                {"rs16947","G;G"},
                {"rs2070676","C;C"},
                {"rs55897648","G;G"},
                {"rs6413419","G;G"},
                {"rs2740574","T;T"},
                {"rs55785340","A;A"},
                {"rs4986910","A;A"},
                {"rs12721627","G;G"},
                {"rs1695","A;A"},
                {"rs1138272","C;C"},
                {"rs4880","A;A"},
                {"rs4986782","C;C"},
                {"rs1805158","C;C"},
                {"rs1801280","T;T"},
                {"rs1799930","G;G"},
                {"rs1799931","G;G"},
                {"rs1801279","G;G"},
                {"rs1208","A;A"},
                {"rs1057910","A;A"}

            };
            Dictionary<string, string> VariationDictionary = new Dictionary<string, string>()   //a dictionary which holds the Gene and Variation for the rs numbers in question
            {               
                    {"rs1048943","CYP1A1*2C A4889G"},
                    {"rs4986883","CYP1A1 m3 T3205C"},
                    {"rs1799814","CYP1A1 C2453A"},
                    {"rs762551","CYP1A2 164A>C"},
                    {"rs1056836","CYP1B1 L432V"},
                    {"rs1800440","CYP1B1 N453S"},
                    {"rs10012","CYP1B1 R48G	"},
                    {"rs1801272","CYP2A6*2 1799T>A"},
                    {"rs28399444","CYP2A6*20"},
                    {"rs1799853","CYP2C9*2 C430T"},
                    {"rs12248560","CYP2C19*17"},
                    {"rs1135840","CYP2D6 S486T"},
                    {"rs1065852","CYP2D6 100C>T"},
                    {"rs16947","CYP2D6 2850C>T"},
                    {"rs2070676","CYP2E1*1B 9896C>G"},
                    {"rs55897648","CYP2E1*1B 10023G>A"},
                    {"rs6413419","CYP2E1*4 4768G>A"},
                    {"rs2740574","CYP3A4*1B"},
                    {"rs55785340","CYP3A4*2 S222P"},
                    {"rs4986910","CYP3A4*3 M445T"},
                    {"rs12721627","CYP3A4*16 T185S"},
                    {"rs1695","GSTP1 I105V"},
                    {"rs1138272","GSTP1 A114V"},
                    {"rs4880","SOD2 A16V"},
                    {"rs4986782","R187Q"},
                    {"rs1805158","NAT1 R64W"},
                    {"rs1801280","NAT2 I114T"},
                    {"rs1799930","NAT2 R197Q"},
                    {"rs1799931","NAT2 G286E"},
                    {"rs1801279","NAT2 R64Q"},
                    {"rs1208","NAT2 K268R"},
                    {"rs1057910","CYP2C9*3 A1075C"}


            };
            string report = "Gene And Variation".PadRight(25).PadLeft(20) + "|".PadRight(5) + "rsID".PadRight(10) + "|".PadRight(5) + "Alleles".PadRight(10) + "|".PadRight(5) + "Result".PadRight(10) + "|".PadRight(5) + "Mutation".PadRight(10) + "|".PadRight(5) +  "Type of Mutation".PadRight(28) + "|".PadRight(5) + "\n";   //stores the report generated


            List<string> valuesUsed = new List<string>();   //the rs numbers used in the report
            //Scan through the list of RSID's and find if one matches the rsID in the Allele Dictionary
            for (int i = 0; i < DNAInfoList.Count; i++)
            {
                if (AlelleDictionary.ContainsKey(DNAInfoList[i].RSID))
                {

                    //find if the alleles in DNAInfoList[i] are mutated from the ones in the Alelle Dictionary
                    string AlleleOne = "";  //the first allele of the rs number
                    string AlleleTwo = "";  //the second allele of the rs number
                    string Mutation = "";   //holds the sign for the mutation
                    string MutationType = "";//translates what the sign for the mutation means
                    AlelleDictionary.TryGetValue(DNAInfoList[i].RSID, out AlleleOne);
                    AlleleTwo = AlleleOne.Split(';')[1];
                    AlleleOne = AlleleOne.Split(';')[0];
                    valuesUsed.Add(DNAInfoList[i].RSID);
                    if (DNAInfoList[i].AlleleOne == AlleleOne)
                    {
                        
                        if (DNAInfoList[i].AlleleTwo == AlleleTwo)
                        {
                            Mutation = "-/-";   //No mutation
                            MutationType = "No mutation";
                        }//end if(DNAInfoList[i].AlleleTwo == AlleleTwo)
                        else
                        {
                            Mutation = "-/+";   //one mutation allele two
                            MutationType = "Single Mutation: Allele Two";
                        }//end else (DNAInfoList[i].AlleleTwo == AlleleTwo)
                    }//end if (DNAInfoList[i].AlleleOne == AlleleOne)
                    else
                    {
                        if (DNAInfoList[i].AlleleTwo == AlleleTwo)
                        {
                            Mutation = "+/-"; //one mutation allele one
                            MutationType = "Single Mutation: Allele One";
                        }//end if(DNAInfoList[i].AlleleTwo == AlleleTwo)
                        else
                        {
                            Mutation = "+/+";   //a double mutation
                            MutationType = "Double Mutation";
                        }//end else(DNAInfoList[i].AlleleTwo == AlleleTwo)
                    }// end else(DNAInfoList[i].AlleleOne == AlleleOne)
                    string variation = "";  //a string to get the gene and variation of an rs number
                    VariationDictionary.TryGetValue(DNAInfoList[i].RSID, out variation);
                    report += variation.Trim().PadRight(25).PadLeft(20) + "|".PadRight(5) + DNAInfoList[i].RSID.PadRight(10) + "|".PadRight(5) + DNAInfoList[i].AlleleOne.PadRight(10) + "|".PadRight(5) + DNAInfoList[i].AlleleTwo.PadRight(10) + "|".PadRight(5) + Mutation.PadRight(10) + "|".PadRight(5) + MutationType.PadRight(28) + "|".PadRight(5) + "\n";
                }
            }
            string RSNumbersNotFound = "";  //a string to get the rs numbers not found in the DNA report for a detox profile
            foreach (KeyValuePair<string, string> entry in VariationDictionary)
            {
                if (!valuesUsed.Contains(entry.Key))
                {
                    RSNumbersNotFound += entry.Key + " (" + entry.Value.Trim() + ")" + ", ";
                }
            }
            Console.WriteLine(report);
            Console.WriteLine();
            Console.WriteLine("RS Numbers and Genes not found in DNA file: ");
            Console.WriteLine(RSNumbersNotFound);
            Console.ReadLine();
        }//end public static void Main(string[] args)
    }//end public class DetoxProfileDriver
}//end name space DetoxProfile
